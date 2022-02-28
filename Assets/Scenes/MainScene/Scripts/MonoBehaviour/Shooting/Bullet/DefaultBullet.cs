using UnityEngine;

public class DefaultBullet : Bullet
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private LayerMask _layerMaskTarget;

    private void OnEnable()
    {
        Move();
    }

    protected override void Move()
    {
        if (!_rigidbody || !Config) { return; }

        _rigidbody.velocity = transform.right * Config.FlightSpeed;
    }

    protected override void Destroy()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((_layerMaskTarget.value & (1 << collision.gameObject.layer)) != 0)
        {
            if (collision.gameObject.TryGetComponent<Health>(out Health health))
            {
                health.TakeHit(DamageCalc.WeaponDamageCalc(Config.Damage, Config.CritChance));
            }

            if (collision.gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D rigidbody))
            {
                rigidbody.AddForce(_rigidbody.velocity * Config.Force, ForceMode2D.Impulse);
            }

            Destroy();
        }
    }
}
