using UnityEngine;

[CreateAssetMenu(fileName = "Potion", menuName = "Config/Potion", order = 52)]
public class PotionConfig : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private string _restoreType;
    [SerializeField] private int _restoreCount;

    public string Name => _name;
    public string RestoreType => _restoreType;
    public int RestoreCount => _restoreCount;
}
