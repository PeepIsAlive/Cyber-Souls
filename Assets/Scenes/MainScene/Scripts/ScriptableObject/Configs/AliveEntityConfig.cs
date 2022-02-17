using UnityEngine;

[CreateAssetMenu(fileName = "Alive Entity", menuName = "Config/AliveEntity", order = 52)]
public class AliveEntityConfig : ScriptableObject
{
    [SerializeField] private float _moveSpeed;

    public float MoveSpeed => _moveSpeed;
}
