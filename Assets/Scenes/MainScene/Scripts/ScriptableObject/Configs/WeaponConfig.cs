using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Config/Weapon", order = 52)]
public class WeaponConfig : ScriptableObject
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField][Range(0, 3)] private float _maxDelayAfterShot;

    public GameObject BulletPrefab => _bulletPrefab;
    public float MaxDelayAfterShot => _maxDelayAfterShot;
}
