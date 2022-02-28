using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class PlayerController : AliveEntity
{
    [SerializeField] private Joystick _moveJoystick;
    [SerializeField] private Joystick _shootJoystick;
    private Rigidbody2D _rigidbody;
    [HideInInspector] public UnityEvent OnPickUpActionEvent;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Rotate();
        Move();
    }

    public void PickUpCollectableItem()
    {
        OnPickUpActionEvent?.Invoke();
    }

    protected override void Move()
    {
        if (!_rigidbody || !_moveJoystick) { return; }

        _rigidbody.velocity = new Vector3(_moveJoystick.X * Config.MoveSpeed, _moveJoystick.Y * Config.MoveSpeed);
    }

    protected override void Rotate()
    {
        if (!_moveJoystick) { return; }

        Vector3 rotation;

        if (_moveJoystick.IsPressed && !_shootJoystick.IsPressed)
        {
            rotation = new Vector3(0, (_moveJoystick.X < 0) ? -180 : 0, 0);
        }
        else
        {
            rotation = new Vector3(0, (_shootJoystick.X < 0) ? -180 : 0, 0);
        }

        transform.rotation = Quaternion.Euler(rotation);
    }
}
