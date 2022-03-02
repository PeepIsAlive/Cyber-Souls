using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponConfig _config;

    public WeaponConfig Config => _config;
    public float RotateZ { get; private set; }

    protected virtual Quaternion Rotate(float x, float y)
    {
        RotateZ = Mathf.Atan2(y, x) * Mathf.Rad2Deg;

        return Quaternion.Euler((RotateZ > 90 || RotateZ < -90) ? 180 : 0, 0, (RotateZ > 90 || RotateZ < -90) ? -RotateZ : RotateZ);
    }
}
