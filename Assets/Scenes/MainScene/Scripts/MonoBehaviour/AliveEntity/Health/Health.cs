using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int _healthCount = 100;
    [HideInInspector] public UnityEvent<int> OnTakeHitEvent;

    [Header("VFX:")]
    [SerializeField] private ParticleSystem _destroyEffect;

    public void TakeHit(int damage)
    {
        if (_healthCount > 0) { _healthCount -= damage; }
        if (_healthCount < 0) { _healthCount = 0; }
        if (_healthCount == 0) { Die(); }

        OnTakeHitEvent?.Invoke(damage);
    }

    private void Die()
    {
        if (CompareTag("Enemy")) { Destroy(gameObject); }

        if (_destroyEffect) { _destroyEffect.Play(); }
    }
}