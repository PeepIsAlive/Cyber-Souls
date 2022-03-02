using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Config/Weapon", order = 52)]
public class WeaponConfig : ScriptableObject
{
    [SerializeField] private int _id;
    [SerializeField] private string _name;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] [Range(0, 3)] private float _maxDelayAfterShot;

    public int Id => _id;
    public string Name => _name;
    public Sprite Sprite => _sprite;
    public GameObject Prefab => _prefab;
    public GameObject BulletPrefab => _bulletPrefab;
    public float MaxDelayAfterShot => _maxDelayAfterShot;
}
