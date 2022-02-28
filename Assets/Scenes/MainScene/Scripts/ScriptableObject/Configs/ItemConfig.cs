using UnityEngine;

[CreateAssetMenu(fileName = "New item config", menuName = "Config/ItemConfig", order = 51)]
public class ItemConfig : ScriptableObject
{
    #region ::Init field::
    [SerializeField] private string _name;
    [SerializeField] private int _amount;
    [SerializeField] private GameObject _itemTemplate;
    #endregion

    #region ::Properties::
    public string Name => _name;
    public int Amount => _amount;
    public GameObject ItemTemplate => _itemTemplate;
    #endregion
}
