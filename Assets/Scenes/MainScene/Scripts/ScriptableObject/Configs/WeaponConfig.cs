using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Config/Weapon", order = 52)]
public class WeaponConfig : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private GameObject _weaponPrefab;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] [Range(0, 3)] private float _maxDelayAfterShot;

    public string Name => _name;
    public GameObject WeaponPrefab => _weaponPrefab;
    public GameObject BulletPrefab => _bulletPrefab;
    public float MaxDelayAfterShot => _maxDelayAfterShot;
}
