using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItemSlot
{
    #region ::Init field::
    private ItemConfig _item;
    private int _amount = 0;
    #endregion

    #region ::Properties::
    public ItemConfig Item => _item;
    public int Amount { get => _amount; set { if (value > 0) _amount = value; } }
    #endregion

    public InventoryItemSlot(ItemConfig item)
    {
        _item = item;
        _amount += item.Amount;
    }
}

[System.Serializable]
public class InventoryWeaponSlot
{
    #region ::Init field::
    public WeaponConfig _weapon;
    #endregion

    #region ::Properties::
    public WeaponConfig Weapon { get => _weapon; set { _weapon = value; } }
    #endregion

    public InventoryWeaponSlot(WeaponConfig weapon)
    {
        _weapon = weapon;
    }
}

public class Inventory : MonoBehaviour
{
    #region ::Init field::
    [SerializeField] private List<InventoryItemSlot> _inventoryItems = new List<InventoryItemSlot>();
    [SerializeField] private List<InventoryWeaponSlot> _inventoryWeapons = new List<InventoryWeaponSlot>();
    [SerializeField] private Transform _dropPointTransform;

    private const int _sizeInventoryItems = 10;
    private const int _sizeInventoryWeapons = 3;
    #endregion

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

        if (_inventoryItems.Count == _sizeInventoryWeapons)
        {
            previousWeapon = _inventoryWeapons[2].Weapon;
            _inventoryWeapons[2].Weapon = weapon;
        }

        if (previousWeapon != null)
        {
            DropWeapon(ref previousWeapon);
            return;
        }

        if (_inventoryWeapons.Count < _sizeInventoryWeapons) _inventoryWeapons.Add(new InventoryWeaponSlot(weapon));
    }

    public void DropWeapon(ref WeaponConfig weapon)
    {
        Instantiate(weapon.WeaponPrefab, _dropPointTransform.position, Quaternion.identity);
        weapon = null;
    }
}
