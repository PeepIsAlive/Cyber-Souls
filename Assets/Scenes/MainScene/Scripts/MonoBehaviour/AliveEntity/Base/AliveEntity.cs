using UnityEngine;

public abstract class AliveEntity : MonoBehaviour
{
    [SerializeField] private AliveEntityConfig _config;

    public AliveEntityConfig Config => _config;

    protected abstract void Move();
    protected abstract void Rotate();
}