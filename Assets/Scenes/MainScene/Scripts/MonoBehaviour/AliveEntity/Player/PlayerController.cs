using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class PlayerController : AliveEntity
{
    [SerializeField] private Joystick _moveJoystick;
    [SerializeField] private Joystick _shootJoystick;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private Inventory _inventory;
    [SerializeField] private SpriteRenderer _weaponRenderer;
    [HideInInspector] public UnityEvent OnPickUpActionEvent;

    public static PlayerController Instance { get; private set; }
    public Joystick MoveJoystick => _moveJoystick;
    public Joystick ShootJoystick => _shootJoystick;

    private void Start()
    {
        if (!Instance) { Instance = this; }
        else if(Instance == this) { Destroy(gameObject); }

        DontDestroyOnLoad(gameObject);

        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _inventory = GetComponent<Inventory>();
        _weaponRenderer = transform.GetChild(0).GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        _inventory.OnEquipWeaponEvent.AddListener(ChangeWeaponRenderer);
    }

    private void FixedUpdate()
    {
        Rotate();
        Move();
    }

    public void PickUpCollectableItem()
    {
        OnPickUpActionEvent?.Invoke();
    }

    private void ChangeWeaponRenderer(SpriteRenderer renderer)
    {
        _weaponRenderer = renderer;
    }

    protected override void Move()
    {
        if (!_rigidbody || !_moveJoystick) { return; }

        _rigidbody.velocity = new Vector3(_moveJoystick.X * Config.MoveSpeed, _moveJoystick.Y * Config.MoveSpeed);

        if (_animator)
        {
            _animator.SetFloat("Speed", Mathf.Abs(_rigidbody.velocity.x + _rigidbody.velocity.y));
        }
    }

    protected override void Rotate()
    {
        if (!_moveJoystick) { return; }

        Vector3 rotation;

        if (_moveJoystick.IsPressed && !_shootJoystick.IsPressed)
        {
            rotation = new Vector3(0, (_moveJoystick.X < 0) ? -180 : 0, 0);
        }
        else
        {
            rotation = new Vector3(0, (_shootJoystick.X < 0) ? -180 : 0, 0);
        }

        if (!_shootJoystick.IsPressed)
        {
            if (_weaponRenderer) { _weaponRenderer.flipX = (_moveJoystick.X < 0) ? true : false; }
        }

        transform.rotation = Quaternion.Euler(rotation);
    }
}
