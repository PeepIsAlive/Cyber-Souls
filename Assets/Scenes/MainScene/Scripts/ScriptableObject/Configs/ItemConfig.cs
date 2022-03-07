using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Config/Item", order = 51)]
public class ItemConfig : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private int _amount;
    [SerializeField] private GameObject _itemTemplate;

    public string Name => _name;
    public int Amount => _amount;
    public GameObject ItemTemplate => _itemTemplate;
}
