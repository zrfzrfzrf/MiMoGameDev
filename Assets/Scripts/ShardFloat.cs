using UnityEngine;

public class CrystalFloat : MonoBehaviour
{
    [Header("Float Settings")]
    public float amplitude = 0.5f;      // How far up/down it bobs
    public float frequency = 1f;        // How fast it bobs
    public float rotationSpeed = 30f;   // Degrees per second (0 to disable)

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float newY = startPosition.y + Mathf.Abs(Mathf.Sin(Time.time * frequency) * amplitude);
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);

        // Optional gentle rotation
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}