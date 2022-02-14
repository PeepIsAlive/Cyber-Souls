using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class PlayerController : MonoBehaviour
{
    #region ::Init field::
    [SerializeField] private FixedJoystick _moveJoystick;
    [SerializeField] private FixedJoystick _weaponJoystick;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Transform _weaponTransform;

    [SerializeField] private float _moveSpeed;
    #endregion

    private void FixedUpdate()
    {
        WeaponRotate();
        Rotate();
        Move();
    }

    private void Move()
    {
        if (_rigidbody) { _rigidbody.velocity = new Vector3(_moveJoystick.Horizontal * _moveSpeed, _moveJoystick.Vertical * _moveSpeed); }
    }

    private void Rotate()
    {
        if (_moveJoystick.Horizontal != 0 && _moveJoystick.Vertical != 0) {
            if (_spriteRenderer) { _spriteRenderer.flipX = (_moveJoystick.Horizontal < 0) ? true : false; }
        }
    }

    private void WeaponRotate()
    {
        float rotateZ = Mathf.Atan2(_weaponJoystick.Vertical, _weaponJoystick.Horizontal) * Mathf.Rad2Deg;

        if (_weaponTransform) { _weaponTransform.rotation = Quaternion.Euler(0, 0, rotateZ); }
    }
}
