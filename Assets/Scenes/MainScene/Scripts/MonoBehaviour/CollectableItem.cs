using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class CollectableItem : MonoBehaviour
{
    [SerializeField] private PotionConfig _potion;
    [SerializeField] private ItemConfig _item;
    [SerializeField] private WeaponConfig _weapon;

    private PlayerController _player;
    private Inventory _inventory;
    private bool _playerInTrigger = false;

    private void OnDisable() { if (_player) _player.OnPickUpActionEvent.RemoveListener(PickUp); }

    private void PickUp()
    {
        if (!_inventory) { return; }
        if (!_item && !_potion && !_weapon) return;

        if (_playerInTrigger && _player)
        {
            if (_item) { _inventory.AddItem(_item); }
            if (_potion) { PlayerController.Instance.Health.RestoreHealth(_potion.RestoreCount); }
            if (_weapon) { _inventory.AddWeapon(_weapon); }

            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!_playerInTrigger && collision.gameObject.CompareTag("Player"))
        {
            _inventory = PlayerController.Instance.Inventory;

            if (!_player)
            {
                _player = PlayerController.Instance;
                _player.OnPickUpActionEvent.AddListener(PickUp);
            }

            if (!_playerInTrigger) _playerInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _playerInTrigger = false;
        _inventory = null;
    }
}
