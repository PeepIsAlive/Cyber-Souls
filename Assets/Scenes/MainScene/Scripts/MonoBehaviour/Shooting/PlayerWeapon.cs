using UnityEngine;

public class PlayerWeapon : Weapon
{
    [SerializeField] private Joystick _joystick;
    private Transform _shootPoint;
    private SpriteRenderer _spriteRenderer;

    private float _delayAfterShot = 10f;

    private void OnEnable()
    {
        _shootPoint = GetComponentInChildren<Transform>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        OnStart();
    }

    private void FixedUpdate()
    {
        Rotate();
        Shoot();
    }

    private void Shoot()
    {
        if (_joystick.IsPressed)
        {
            if (_delayAfterShot >= MaxDelayAfterShot)
            {
                Instantiate(BulletPrefab, _shootPoint.position, Quaternion.Euler(0, 0, RotateZ));

                _delayAfterShot = 0;
            }
        }

        _delayAfterShot += Time.fixedDeltaTime;
    }

    protected virtual void Rotate()
    {
        transform.rotation = base.Rotate(_spriteRenderer, _joystick.X, _joystick.Y);
    }
}
