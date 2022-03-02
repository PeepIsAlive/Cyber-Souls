using System;
using UnityEngine;

public class PlayerWeapon : Weapon
{
    public Joystick Joystick;
    [SerializeField] private Transform _shootPoint;
    private SpriteRenderer _renderer;

    private float _delayAfterShot = 10f;

    private void OnEnable()
    {
        _renderer = GetComponent<SpriteRenderer>();
        // shootPoint
    }

    private void FixedUpdate()
    {
        Rotate();
        Shoot();
    }

    private void Shoot()
    {
        if (!_shootPoint || !Joystick) { return; }

        if (Joystick.IsPressed)
        {
            if (_delayAfterShot >= Config.MaxDelayAfterShot)
            {
                Instantiate(Config.BulletPrefab, _shootPoint.position, Quaternion.Euler(0, 0, RotateZ));

                _delayAfterShot = 0;
            }
        }

        try
        {
            _delayAfterShot = checked(_delayAfterShot + Time.fixedDeltaTime);
        }
        catch (OverflowException) { _delayAfterShot = Config.MaxDelayAfterShot; }
        
    }

    protected virtual void Rotate()
    {
        if (!Joystick || !_renderer) { return; }

        if (Joystick.IsPressed)
        {
            if (_renderer.flipX == true) { _renderer.flipX = false; }
        }

        transform.rotation = base.Rotate(Joystick.X, Joystick.Y);
    }
}
