using UnityEngine;

public class PushObject : MonoBehaviour
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
        Vector2 targetVel = Vector2.zero;

        if (miPlayer != null)
        {
            float dist = Vector2.Distance(rb.position, miPlayer.GetComponent<Rigidbody2D>().position);
            Vector2 dirToPlayer = (miPlayer.GetComponent<Rigidbody2D>().position - rb.position).normalized;
            // 把点积算出来方便打印
            float dotResult = Vector2.Dot(miPlayer.currentInput, dirToPlayer);

            // 1. 每帧输出实时数据（如果觉得刷屏可以注释掉，主要看下面的成功输出）
            // Debug.Log($"Mi 探测中 -> 距离: {dist:F2}/{pullDistance} | 点积: {dotResult:F2}/-0.7");

            if (dist <= pullDistance && dotResult < -0.7f)
            {
                // 2. 只有判定完全通过时才进入
                Debug.Log($"<color=green>【Mi PUSH 判定通过】</color> 物体: {gameObject.name} | 正在推动！");
                rb.bodyType = RigidbodyType2D.Dynamic;
                rb.drag = 30f; // 确保没有额外阻力
                
            }
            else
            {
                if (rb.velocity == Vector2.zero)
                {
                    rb.bodyType = RigidbodyType2D.Kinematic;
                }
            }
        }

    }
}