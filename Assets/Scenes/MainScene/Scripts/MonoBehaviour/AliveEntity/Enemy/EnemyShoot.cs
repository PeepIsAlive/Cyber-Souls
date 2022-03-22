using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    private EnemyMovement _movement;
    private EnemyWeapon _weapon;

    private void OnEnable()
    {
        _movement = GetComponent<EnemyMovement>();
        _weapon = transform.GetChild(0).GetChild(0).gameObject.GetComponent<EnemyWeapon>();
    }

    private void FixedUpdate()
    {
        Shoot();
    }

    private void Shoot()
    {
        if (_movement.TargetInVisibilityZone)
        {
            _weapon.Rotate(_movement.TargetPosition.x, _movement.TargetPosition.y);
        }

        // to do
    }
}
