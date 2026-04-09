using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PushPullObject : MonoBehaviour
{
    public float pushSpeed = 3f;
    public float pullSpeed = 3f;
    public float allowedAngle = 0.7f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        PlayerMovement player = collision.collider.GetComponent<PlayerMovement>();
        if (player == null) return;

        Vector2 directionToPlayer = ((Vector2)player.transform.position - (Vector2)transform.position).normalized;
        Vector2 input = player.currentInput;

        if (player.role == PlayerMovement.PlayerRole.Mi)
        {
            if (Vector2.Dot(input, directionToPlayer) > allowedAngle)
            {
                rb.velocity = input * pushSpeed;
                return;
            }
        }
        else if (player.role == PlayerMovement.PlayerRole.Mo)
        {
            Debug.Log("碰撞者角色: " + player.role + " | 是否拉取: " + player.isPulling);
            if (player.isPulling && Vector2.Dot(input, directionToPlayer) > allowedAngle)
            {
                rb.velocity = input * pullSpeed;
                Debug.Log("箱子应该在动了！速度设为: " + rb.velocity);
                return;
            }
        }

        rb.velocity = Vector2.zero;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        rb.velocity = Vector2.zero;
    }
}