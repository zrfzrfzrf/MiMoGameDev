using UnityEngine;

public class PullObject : MonoBehaviour
{
    [Header("Settings")]
    public float pullDistance = 1.5f; 
    // 技巧：pullSpeed 设为 -1 表示完全同步玩家速度
    public float pullSpeed = -1f; 
    public float allowedAngle = 0.4f; 

    private Rigidbody2D rb;
    private PlayerMovement moPlayer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        // 关键：防止拉动时箱子乱转
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        
        // 找到 Mo
        PlayerMovement[] players = FindObjectsOfType<PlayerMovement>();
        foreach(var p in players)
        {
            if(p.role == PlayerMovement.PlayerRole.Mo) {
                moPlayer = p;
                break;
            }
        }
    }

    private void FixedUpdate() // 处理物理位移建议用 FixedUpdate
    {
        if (moPlayer == null) return;

        float dist = Vector2.Distance(transform.position, moPlayer.transform.position);
        Vector2 directionToPlayer = ((Vector2)moPlayer.transform.position - (Vector2)transform.position).normalized;
        Vector2 input = moPlayer.currentInput;

        // 拉取判定
        if (dist <= pullDistance && moPlayer.isPulling && input.magnitude > 0.1f)
        {
            if (Vector2.Dot(input, directionToPlayer) > allowedAngle)
            {
                // --- 解决速度慢的核心逻辑 ---
                
                // 1. 获取 Mo 当前的实际速度（包含冲刺倍率后的速度）
                Rigidbody2D moRb = moPlayer.GetComponent<Rigidbody2D>();
                Vector2 targetVelocity = moRb.velocity;

                // 2. 距离补偿：如果箱子掉队了（距离变大），额外加一个“追赶力”
                Vector2 catchUp = Vector2.zero;
                if (dist > 1.1f) // 假设 1.0 是贴紧距离
                {
                    catchUp = directionToPlayer * (dist * 2f); 
                }

                rb.velocity = targetVelocity + catchUp;
                return;
            }
        }

        // 停止拉取时，箱子立刻静止（或者你可以靠 Rigidbody 的 Drag）
        rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, Time.fixedDeltaTime * 10f);
    }
}