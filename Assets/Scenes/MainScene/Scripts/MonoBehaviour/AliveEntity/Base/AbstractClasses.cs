using UnityEngine;

public abstract class AliveEntity : MonoBehaviour
{
    [SerializeField] private AliveEntityConfig _config;

    public float MoveSpeed { get; private set; }

    protected void OnStart()
    {
        MoveSpeed = _config.MoveSpeed;
    }

    protected virtual void Move() { }
    protected virtual void Rotate() { }
}

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponConfig _config;

    public float RotateZ { get; private set; }
    public float MaxDelayAfterShot { get; private set; }
    public GameObject BulletPrefab { get; private set; }

    protected void OnStart()
    {
        MaxDelayAfterShot = _config.MaxDelayAfterShot;
        BulletPrefab = _config.BulletPrefab;
    }

    protected virtual Quaternion Rotate(SpriteRenderer renderer, float x, float y)
    {
        if (renderer) { renderer.flipX = (x < 0) ? true : false; }
        RotateZ = Mathf.Atan2(y, x) * Mathf.Rad2Deg;

        return Quaternion.Euler(0, 0, RotateZ);
    }
}