using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

[RequireComponent(typeof(Rigidbody))]
public sealed class PlayerController : MonoBehaviour
{
    //Singleton
    static PlayerController instance = null;

    //Movement
    Transform _trans;
    Vector2 _movementInput;
    Vector3 _movement;
    Rigidbody _rb;
    PlayerInputActions _inputActions;
    [SerializeField] float _movementSpeed;

    //Stats
    public float maxHealth;
    public float health;
    bool damageable = true;

    //Attacks
    IceShot _iceShotAttack;
    bool _canAttack;

    //Singleton Blocks
    private PlayerController() { }
    public static PlayerController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PlayerController();
            }
            return instance;
        }
    }

    private void Awake()
    {
        _inputActions = new PlayerInputActions();
        _rb = gameObject.GetComponent<Rigidbody>();
        _inputActions.Player.Enable();
        _iceShotAttack = gameObject.GetComponent<IceShot>();
        _canAttack = true;
        _trans = gameObject.transform;
        instance = this;
    }

    private void Update()
    {
        if(Input.GetButton("Fire") && _canAttack)
        {
            StartCoroutine(AttackCooldown());
        }
    }

    IEnumerator AttackCooldown()
    {
        _canAttack = false;
        _iceShotAttack.PlayerAttack(_trans);
        yield return new WaitForSeconds(_iceShotAttack.GetCooldown());
        _canAttack = true;
    }

    private void FixedUpdate()
    {
        _movementInput = _inputActions.Player.Move.ReadValue<Vector2>();
        _movement = new Vector3(_movementInput.x, 0.0f, _movementInput.y);
        _rb.velocity = _movement * _movementSpeed;
    }

    public void TakeDamage(float amount)
    {
        if (damageable)
        {
            health -= amount;

            if (health <= 0)
            {
                Debug.Log("I'm dead!");
            }
            StartCoroutine(DamageCooldown());
        }
    }

    IEnumerator DamageCooldown()
    {
        damageable = false;
        yield return new WaitForSeconds(1);
        damageable = true;
    }
}
