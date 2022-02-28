using System;
using UnityEngine;

public class PlayerWeapon : Weapon
{
    [SerializeField] private Joystick _joystick;
    private Transform _shootPoint;

    private float _delayAfterShot = 10f;

    private void OnEnable()
    {
        _shootPoint = GetComponentInChildren<Transform>();
    }

    private void FixedUpdate()
    {
        Rotate();
        Shoot();
    }

    private void Shoot()
    {
        if (!_shootPoint || !_joystick) { return; }

        if (_joystick.IsPressed)
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
        if (!_joystick) { return; }

        transform.rotation = base.Rotate(_joystick.X, _joystick.Y);
    }
}
