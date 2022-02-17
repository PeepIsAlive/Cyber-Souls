using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class Player : AliveEntity
{
    [SerializeField] private Joystick _joystick;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _renderer;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();

        OnStart();
    }

    private void FixedUpdate()
    {
        Rotate();
        Move();
    }

    protected override void Move()
    {
        if (_rigidbody) { _rigidbody.velocity = new Vector3(_joystick.X * MoveSpeed, _joystick.Y * MoveSpeed); }
    }

    protected override void Rotate()
    {
        if (_joystick.IsPressed)
        {
            if (_renderer) { _renderer.flipX = (_joystick.X < 0) ? true : false; }
        }
    }
}
