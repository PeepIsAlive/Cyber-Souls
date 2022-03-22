using UnityEngine;

public class EnemyMovement : AliveEntity
{
    [SerializeField] private LayerMask _targetsLM;

    private Transform _targetTransform;
    private bool _targetInVisibilityZone;

    public Vector3 TargetPosition => _targetTransform.position;
    public bool TargetInVisibilityZone => _targetInVisibilityZone;

    private void FixedUpdate()
    {
        Rotate();
    }

    protected override void Move()
    {
        // to do
    }

    protected override void Rotate()
    {
        if (_targetInVisibilityZone && _targetTransform != null)
        {
            float xPosTarget = TargetPosition.x;
            float xPosEnemy = transform.position.x;

            transform.rotation = Quaternion.Euler(transform.rotation.x, (xPosTarget > xPosEnemy) ? 0 : 180, transform.rotation.z);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((_targetsLM.value & (1 << collision.gameObject.layer)) != 0)
        {
            _targetInVisibilityZone = true;
            
            if (_targetTransform == null)
            {
                _targetTransform = collision.transform;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((_targetsLM.value & (1 << collision.gameObject.layer)) != 0)
        {
            if (_targetTransform == collision.transform)
            {
                _targetInVisibilityZone = false;
                _targetTransform = null;
            }
        }
    }
}
