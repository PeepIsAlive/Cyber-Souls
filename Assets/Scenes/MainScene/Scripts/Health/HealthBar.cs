using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Image _healthBar;

    private void OnEnable()
    {
        if (_health) { _health.OnTakeHitEvent.AddListener(ChangeHealtBar); }
    }

    private void ChangeHealtBar(int value)
    {
        if (_healthBar.fillAmount > 0f) { _healthBar.fillAmount -= (float)value / 100.0f; }
    }
}
