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
    AttackController _attackController;

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
        _rb = GetComponent<Rigidbody>();
        _attackController = GetComponent<AttackController>();
        _inputActions.Player.Enable();
        _trans = gameObject.transform;
        instance = this;
    }

    private void Update()
    {
        if (_inputActions.FindAction("Fire").WasPerformedThisFrame()) _attackController.AttackAction("Ice Burst");
        if (_inputActions.FindAction("SecondaryFire").WasPerformedThisFrame()) _attackController.AttackAction("Ice Shot");
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
                GameObject.Find("GameManager").GetComponent<Score>().SaveHighScore();
                GameObject.Find("GameManager").GetComponent<SceneLoader>().Restart();
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
