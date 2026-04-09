using UnityEngine;

public class PushPullObject : MonoBehaviour
{
    public float pullDistance = 1.3f;
    public float allowedAngle = 0.4f;

    private Rigidbody2D rb;
    private PlayerMovement moPlayer;
    private PlayerMovement miPlayer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        // 初始设为运动学，绝对不动
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        
        // 建议在 Inspector 里手动指定，或者这样寻找：
        foreach (var p in FindObjectsOfType<PlayerMovement>())
        {
            if (p.role == PlayerMovement.PlayerRole.Mi) miPlayer = p;
            if (p.role == PlayerMovement.PlayerRole.Mo) moPlayer = p;
        }
    }

    private void FixedUpdate()
    {
        bool isBeingInteracted = false;
        Vector2 targetVel = Vector2.zero;

        // --- Mo 的拉取逻辑 ---
        if (moPlayer != null)
        {
            float dist = Vector2.Distance(rb.position, moPlayer.GetComponent<Rigidbody2D>().position);
            Vector2 dirToPlayer = (moPlayer.GetComponent<Rigidbody2D>().position - rb.position).normalized;

            if (moPlayer.isPulling && dist <= pullDistance && Vector2.Dot(moPlayer.currentInput, dirToPlayer) > allowedAngle)
            {
                // 核心修复：直接取玩家速度，不要加额外的加速力，防止飞出去
                targetVel = moPlayer.GetComponent<Rigidbody2D>().velocity;
                isBeingInteracted = true;
            }
        }

        // --- Mi 的推送逻辑 ---
        if (!isBeingInteracted && miPlayer != null)
        {
            float dist = Vector2.Distance(rb.position, miPlayer.GetComponent<Rigidbody2D>().position);
            Vector2 dirToPlayer = (miPlayer.GetComponent<Rigidbody2D>().position - rb.position).normalized;

            if (dist <= pullDistance && Vector2.Dot(miPlayer.currentInput, dirToPlayer) < -0.7f)
            {
                targetVel = miPlayer.GetComponent<Rigidbody2D>().velocity;
                isBeingInteracted = true;
            }
        }

        // --- 物理状态切换 ---
        if (isBeingInteracted)
        {
            // 变成 Dynamic 才能和墙壁产生正常的 Collision 交互
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.velocity = targetVel;
            // 增加阻尼，防止松手后滑太远
            rb.drag = 0f; 
        }
        else
        {
            // 没人动时，变回 Kinematic 锁死
            rb.velocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
    }
}