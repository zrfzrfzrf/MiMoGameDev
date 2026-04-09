using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class PullObject : MonoBehaviour
{
    [Header("Detection Settings")]
    public float pullDistance = 10f;   // 感应距离
    public float allowedAngle = 0.4f;    // 拉取角度判定 (点积 > 0.4)

    private Rigidbody2D rb;
    private Collider2D _myCollider;      // 箱子自己的碰撞体
    private Collider2D _moCollider;      // Mo 的碰撞体
    private PlayerMovement moPlayer;
    private PlayerMovement miPlayer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _myCollider = GetComponent<Collider2D>();

        // 基础物理初始化
        rb.gravityScale = 0f;
        rb.bodyType = RigidbodyType2D.Kinematic; // 初始锁死
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        
        // 自动寻找角色并获取 Mo 的 Collider
        PlayerMovement[] players = FindObjectsOfType<PlayerMovement>();
        foreach (var p in players)
        {
            if (p.role == PlayerMovement.PlayerRole.Mi) 
                miPlayer = p;
            
            if (p.role == PlayerMovement.PlayerRole.Mo)
            {
                moPlayer = p;
                _moCollider = p.GetComponent<Collider2D>();
            }
        }
    }

    private void FixedUpdate()
    {
        bool isBeingInteracted = false;
        Vector2 targetVel = Vector2.zero;

        // --- 1. Mo 的拉取逻辑 ---
        if (moPlayer != null)
        {
            // 计算距离和方向
            float dist = Vector2.Distance(rb.position, moPlayer.GetComponent<Rigidbody2D>().position);
            Vector2 dirToPlayer = (moPlayer.GetComponent<Rigidbody2D>().position - rb.position).normalized;

            if (moPlayer.isPulling)
            {
                Debug.Log($"Dist: {dist:F2}, Dot: {Vector2.Dot(moPlayer.currentInput, dirToPlayer):F2}");
            }
            
            
            // 判定条件：按住右Alt + 距离足够 + 方向是对的(拉)
            if (moPlayer.isPulling && dist <= pullDistance && Vector2.Dot(moPlayer.currentInput, dirToPlayer) > allowedAngle)
            {
                
                // 只有在拉动且没有发生身体碰撞时，才赋予速度
                // 如果你想撞到了还能拉，就只保留 Log；如果你想撞到了拉不动，就在这里加条件
                if (_moCollider != null && _myCollider.IsTouching(_moCollider))
                {
                    Debug.Log("<color=yellow>! Mo 正在紧贴箱子 (可能是撞到了) </color>");
                }

                // 同步 Mo 的速度
                targetVel = moPlayer.GetComponent<Rigidbody2D>().velocity;
                isBeingInteracted = true;
            }
        }

        // --- 2. 物理状态执行 ---
        if (isBeingInteracted)
        {
            // 激活物理：允许碰撞和移动
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.drag = 0f; // 确保没有额外阻力
            rb.velocity = targetVel;
        }
        else
        {
            // 锁死物理：变成一堵墙，推不动也拉不动
            rb.velocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
    }

}