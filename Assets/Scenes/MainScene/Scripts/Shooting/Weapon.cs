using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Joystick _joystick;
    [SerializeField] private GameObject _bulletPrefab;
    private Transform _shootPoint;
    private SpriteRenderer _spriteRenderer;

    private float _delayAfterShot = 1f;
    private float _maxDelayAfterShot = 0.5f;
    private float _weaponRotateZ;

    private void OnEnable()
    {
        _shootPoint = GetComponentInChildren<Transform>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        WeaponRotate();
        Shoot();
    }

    private void WeaponRotate()
    {
        if (_spriteRenderer) { _spriteRenderer.flipX = (_joystick.X < 0) ? true : false; }

        _weaponRotateZ = Mathf.Atan2(_joystick.Y, _joystick.X) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, _weaponRotateZ);
    }

    private void Shoot()
    {
        if (_joystick.IsPressed)
        {
            if (_delayAfterShot >= _maxDelayAfterShot)
            {
                Instantiate(_bulletPrefab, _shootPoint.position, Quaternion.Euler(0, 0, _weaponRotateZ));

                _delayAfterShot = 0;
            }
        }

        _delayAfterShot += Time.fixedDeltaTime;
    }
}
