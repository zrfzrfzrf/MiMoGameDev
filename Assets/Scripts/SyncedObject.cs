using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SyncedObject : MonoBehaviour
{
    [Header("Linked Object")]
    public SyncedObject linkedObject;

    private Rigidbody2D _rb;
    private Vector2 _relativeOffset; // 初始相对位置偏移
    private bool _isMaster = false;   // 标记我是否是当前被操控的主体

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (linkedObject != null)
        {
            // 计算初始时对方相对于我的位置
            // 比如我在(0,0)，对方在(10,0)，偏移量就是(10,0)
            _relativeOffset = (Vector2)linkedObject.transform.position - (Vector2)transform.position;
        }
    }

    private void FixedUpdate()
    {
        if (linkedObject == null) return;

        // 【核心逻辑】检查我是否正在被推或拉（BodyType 变为 Dynamic 说明玩家在动我）
        if (_rb.bodyType == RigidbodyType2D.Dynamic)
        {
            _isMaster = true;
            // 告诉对方：我动了，你快“瞬移”到对应的相对位置去
            linkedObject.ReceiveMagicSync((Vector2)transform.position + _relativeOffset);
        }
        else
        {
            _isMaster = false;
        }
    }

    // “魔法同步”函数：无视物理，直接定位
    public void ReceiveMagicSync(Vector2 targetPosition)
    {
        // 如果我此时不是 Master（说明我没被玩家动），我就作为跟随者
        if (!_isMaster)
        {
            // 1. 确保跟随者是 Kinematic，这样它才能穿墙、无视障碍物
            if (_rb.bodyType != RigidbodyType2D.Kinematic)
            {
                _rb.velocity = Vector2.zero;
                _rb.bodyType = RigidbodyType2D.Kinematic;
            }

            // 2. 强制同步位置（MovePosition 会保证平滑但无视阻碍）
            _rb.MovePosition(targetPosition);
        }
    }
}