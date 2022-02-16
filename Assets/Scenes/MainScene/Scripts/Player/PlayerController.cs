using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Joystick _joystick;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    [Space(10)]
    [SerializeField][Range(0, 10)] private float _moveSpeed;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        Rotate();
        Move();
    }

    private void Move()
    {
        if (_rigidbody) { _rigidbody.velocity = new Vector3(_joystick.X * _moveSpeed, _joystick.Y * _moveSpeed); }
    }

    private void Rotate()
    {
        if (_joystick.IsPressed)
        {
            if (_spriteRenderer) { _spriteRenderer.flipX = (_joystick.X < 0) ? true : false; }
        }
    }
}
