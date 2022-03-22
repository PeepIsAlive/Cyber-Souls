using UnityEngine;

public class EnemyWeapon : Weapon
{
    private SpriteRenderer _renderer;

    private void OnEnable()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    public void Shoot()
    {
        // to do
    }

    public new void Rotate(float xPosTarget, float yPosTarget)
    {
        transform.rotation = base.Rotate(xPosTarget, yPosTarget);

        if (_renderer != null)
        {
            _renderer.flipX = (RotateZ < 0) ? true : false;
        }
    }
}
