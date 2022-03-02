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
    [SerializeField] private List<InventoryItemSlot> _inventoryItems = new List<InventoryItemSlot>();
    [SerializeField] private List<InventoryWeaponSlot> _inventoryWeapons = new List<InventoryWeaponSlot>();
    private Transform _weaponPosition;
    private Transform _dropPointTransform;

    private const int _sizeInventoryItems = 3;
    private const int _sizeInventoryWeapons = 1;
    [HideInInspector] public UnityEvent<SpriteRenderer> OnEquipWeaponEvent;

    public int CurrentIndexWeapon { get; private set; }

    public Inventory(List<InventoryWeaponSlot> weapons) => _inventoryWeapons = weapons;
    public GameObject this[int index]
    {
        get => _inventoryWeapons[index].Weapon.Prefab;
    }

    private void OnEnable()
    {
        _weaponPosition = transform.GetChild(0);
        _dropPointTransform = transform.GetChild(1);
        CurrentIndexWeapon = 0;
    }

    public void AddItem(ItemConfig item)
    {
        foreach (InventoryItemSlot slot in _inventoryItems)
        {
            if (slot.Item.name == item.Name)
            {
                slot.Amount += item.Amount;
                return;
            }
        }

        if (_inventoryItems.Count < _sizeInventoryItems) _inventoryItems.Add(new InventoryItemSlot(item));
    }

    public void AddWeapon(WeaponConfig weapon)
    {
        WeaponConfig previousWeapon = null;

        if (_inventoryWeapons.Count == _sizeInventoryWeapons)
        {
            previousWeapon = _inventoryWeapons[CurrentIndexWeapon].Weapon;
            _inventoryWeapons[CurrentIndexWeapon].Weapon = weapon;

            EquipWeapon(weapon);
        }

        if (previousWeapon != null)
        {
            DropWeapon(ref previousWeapon);
            return;
        }

        if (_inventoryWeapons.Count < _sizeInventoryWeapons) _inventoryWeapons.Add(new InventoryWeaponSlot(weapon));
    }

    private void EquipWeapon(WeaponConfig weapon)
    {
        GameObject weaponObj = Instantiate(weapon.Prefab, _weaponPosition);
        weaponObj.GetComponent<PlayerWeapon>().Joystick = PlayerController.Instance.ShootJoystick;

        OnEquipWeaponEvent?.Invoke(weaponObj.GetComponent<SpriteRenderer>());
    }

    private void DropWeapon(ref WeaponConfig weapon)
    {
        Destroy(_weaponPosition.GetChild(0).gameObject);
        Instantiate(weapon.Prefab, _dropPointTransform.position, Quaternion.identity);
        weapon = null;
    }
}
