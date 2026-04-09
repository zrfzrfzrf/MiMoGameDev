using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerMovement : MonoBehaviour
{
    public enum PlayerRole { Mi, Mo }

    [Header("Movement")]
    public PlayerRole role = PlayerRole.Mi;
    public float moveSpeed = 5f;
    public float sprintMultiplier = 2f; // 冲刺速度倍数

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Vector2 moveInput;
    private bool isSprinting;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rb.gravityScale = 0f; // top-down 不要重力
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        moveInput = ReadInput();
        isSprinting = IsSprinting();

        bool isMoving = moveInput != Vector2.zero;
        animator.SetBool("isMoving", isMoving);
        animator.SetFloat("moveX", moveInput.x);
        animator.SetFloat("moveY", moveInput.y);
        animator.SetBool("isSprinting", isSprinting);

        // 根据左右输入决定朝向
        if (moveInput.x > 0.1f)
        {
            spriteRenderer.flipX = false;   // 朝右
        }
        else if (moveInput.x < -0.1f)
        {
            spriteRenderer.flipX = true;    // 朝左
        }
    }

    private void FixedUpdate()
    {
        float currentSpeed = isSprinting ? moveSpeed * sprintMultiplier : moveSpeed;
        rb.velocity = moveInput * currentSpeed;
    }

    private Vector2 ReadInput()
    {
        return role == PlayerRole.Mi ? GetMiInput() : GetMoInput();
    }

    private Vector2 GetMiInput()
    {
        float x = (Input.GetKey(KeyCode.D) ? 1f : 0f) - (Input.GetKey(KeyCode.A) ? 1f : 0f);
        float y = (Input.GetKey(KeyCode.W) ? 1f : 0f) - (Input.GetKey(KeyCode.S) ? 1f : 0f);
        return new Vector2(x, y).normalized;
    }

    private Vector2 GetMoInput()
    {
        float x = (Input.GetKey(KeyCode.L) ? 1f : 0f) - (Input.GetKey(KeyCode.J) ? 1f : 0f);
        float y = (Input.GetKey(KeyCode.I) ? 1f : 0f) - (Input.GetKey(KeyCode.K) ? 1f : 0f);
        return new Vector2(x, y).normalized;
    }

    private bool IsSprinting()
    {
        return role == PlayerRole.Mi ? Input.GetKey(KeyCode.LeftShift) : Input.GetKey(KeyCode.RightShift);
    }
}