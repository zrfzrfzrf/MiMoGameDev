using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SyncedObject : MonoBehaviour
{
    [Header("Linked Object")]
    public SyncedObject linkedObject;

    [Header("Sync Settings")]
    public bool syncPosition = true;
    public bool syncRotation = true;
    public bool syncVelocity = true;

    private Rigidbody2D _rb;
    private bool _isSyncing = false;
    private Vector2 _previousPosition;
    private float _previousRotation;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _previousPosition = _rb.position;
        _previousRotation = _rb.rotation;
    }

    private void LateUpdate()
    {
        if (_isSyncing) return;
        if (linkedObject == null) return;

        Vector2 deltaPosition = _rb.position - _previousPosition;
        float deltaRotation = _rb.rotation - _previousRotation;

        _previousPosition = _rb.position;
        _previousRotation = _rb.rotation;

        // Only sync if something actually moved
        if (deltaPosition == Vector2.zero && deltaRotation == 0f && !syncVelocity) return;

        linkedObject.ApplySync(
            deltaPosition,
            deltaRotation,
            _rb.velocity,
            _rb.angularVelocity
        );
    }

    public void ApplySync(Vector2 deltaPosition, float deltaRotation, Vector2 velocity, float angularVelocity)
    {
        _isSyncing = true;

        if (syncPosition)
        {
            _rb.MovePosition(_rb.position + deltaPosition);
        }

        if (syncRotation)
        {
            _rb.MoveRotation(_rb.rotation + deltaRotation);
        }

        if (syncVelocity)
        {
            _rb.velocity = velocity;
            _rb.angularVelocity = angularVelocity;
        }

        _isSyncing = false;
    }
}