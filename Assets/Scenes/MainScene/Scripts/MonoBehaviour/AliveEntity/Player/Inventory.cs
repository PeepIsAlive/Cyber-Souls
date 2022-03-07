using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InventoryItemSlot
{
    private ItemConfig _item;
    private int _amount = 0;

    public ItemConfig Item => _item;
    public int Amount { get => _amount; set { if (value > 0) _amount = value; } }

    public InventoryItemSlot(ItemConfig item)
    {
        _item = item;
        _amount += item.Amount;
    }
}

[System.Serializable]
public class InventoryWeaponSlot
{
    public WeaponConfig _weapon;

    public WeaponConfig Weapon { get => _weapon; set { _weapon = value; } }

    public InventoryWeaponSlot(WeaponConfig weapon)
    {
        _weapon = weapon;
    }
}

public class Inventory : MonoBehaviour
{
    [SerializeField] private Image _imageWeapon;
    [SerializeField] private List<InventoryItemSlot> _items = new List<InventoryItemSlot>();
    [SerializeField] private List<InventoryWeaponSlot> _weapons = new List<InventoryWeaponSlot>();
    [SerializeField] private GameObject[] _weaponsObj = new GameObject[3];
    [SerializeField] private SpriteRenderer[] _weaponsRendr = new SpriteRenderer[3];
    private Transform _weaponPosition;
    private Transform _dropPointTransform;

    private const int _sizeInventoryItems = 3;
    private const int _sizeInventoryWeapons = 3;
    [HideInInspector] public UnityEvent<SpriteRenderer> OnEquipWeaponEvent;

    public int CurrentIndexWeapon { get; private set; }

    public Inventory(List<InventoryWeaponSlot> weapons) => _weapons = weapons;
    public GameObject this[int index]
    {
        get => _weapons[index].Weapon.Prefab;
    }

    private void OnEnable()
    {
        _weaponPosition = transform.GetChild(0);
        _dropPointTransform = transform.GetChild(1);
        CurrentIndexWeapon = 0;

        _imageWeapon.sprite = _weapons[CurrentIndexWeapon].Weapon.Sprite;
    }

    public void AddItem(ItemConfig item)
    {
        foreach (InventoryItemSlot slot in _items)
        {
            if (slot.Item.name == item.Name)
            {
                slot.Amount += item.Amount;
                return;
            }
        }

        if (_items.Count < _sizeInventoryItems) _items.Add(new InventoryItemSlot(item));
    }

    public void AddWeapon(WeaponConfig weapon)
    {
        WeaponConfig previousWeapon = null;

        if (_weapons.Count == _sizeInventoryWeapons)
        {
            previousWeapon = _weapons[CurrentIndexWeapon].Weapon;
            _weapons[CurrentIndexWeapon].Weapon = weapon;

            DropWeapon(ref previousWeapon);
            EquipWeapon(weapon, true);
        }

        if (_weapons.Count < _sizeInventoryWeapons)
        {
            _weapons.Add(new InventoryWeaponSlot(weapon));
            EquipWeapon(weapon);
        }
    }

    public void ChangeWeapon()
    {
        if (_weapons.Count > 1)
        {
            _weaponsObj[CurrentIndexWeapon].SetActive(false);

            CurrentIndexWeapon = (CurrentIndexWeapon + 1 > _sizeInventoryWeapons - 1) ? 0 : CurrentIndexWeapon + 1;

            _weaponsObj[CurrentIndexWeapon].SetActive(true);
            OnEquipWeaponEvent?.Invoke(_weaponsRendr[CurrentIndexWeapon]);
            _imageWeapon.sprite = _weapons[CurrentIndexWeapon].Weapon.Sprite;
        }
    }

    private void EquipWeapon(WeaponConfig weapon, bool isReplace = false)
    {
        GameObject weaponObj = Instantiate(weapon.Prefab, _weaponPosition);
        SpriteRenderer renderer = weaponObj.GetComponent<SpriteRenderer>();

        weaponObj.GetComponent<PlayerWeapon>().Joystick = PlayerController.Instance.ShootJoystick;

        if (!isReplace)
        {
            weaponObj.SetActive(false);

            _weaponsObj[_weapons.Count - 1] = weaponObj;
            _weaponsRendr[_weapons.Count - 1] = renderer;
        }
        else
        {
            _weaponsObj[CurrentIndexWeapon] = weaponObj;
            _weaponsRendr[CurrentIndexWeapon] = renderer;

            OnEquipWeaponEvent?.Invoke(renderer);
            _imageWeapon.sprite = weapon.Sprite;
        }
    }

    private void DropWeapon(ref WeaponConfig weapon)
    {
        Destroy(_weaponsObj[CurrentIndexWeapon]);
        Instantiate(weapon.Prefab, _dropPointTransform.position, Quaternion.identity);

        weapon = null;
    }
}
