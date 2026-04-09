using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public enum PlayerRole { Mi, Mo }

    [Header("Movement")]
    public PlayerRole role = PlayerRole.Mi;
    public float moveSpeed = 3f;
    public float sprintMultiplier = 2f;

    [HideInInspector] public Vector2 currentInput;
    [HideInInspector] public bool isPulling;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }

    private void Update()
    {
        currentInput = ReadInput();
        isPulling = role == PlayerRole.Mo && Input.GetKey(KeyCode.RightAlt);
    }

    private void FixedUpdate()
    {
        float currentSpeed = moveSpeed;
        if (role == PlayerRole.Mi && Input.GetKey(KeyCode.LeftShift))
            currentSpeed *= sprintMultiplier;
        else if (role == PlayerRole.Mo && Input.GetKey(KeyCode.RightShift))
            currentSpeed *= sprintMultiplier;

        rb.velocity = currentInput * currentSpeed;
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
}