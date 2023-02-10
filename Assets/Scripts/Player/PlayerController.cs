using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    Transform _trans;
    Vector2 _movementInput;
    Vector3 _movement;
    Rigidbody _rb;
    PlayerInputActions _inputActions;
    [SerializeField] float _movementSpeed;
    IceShot _iceShotAttack;
    bool _canAttack;

    private void Awake()
    {
        _inputActions = new PlayerInputActions();
        _rb = gameObject.GetComponent<Rigidbody>();
        _inputActions.Player.Enable();
        _iceShotAttack = gameObject.GetComponent<IceShot>();
        _canAttack = true;
        _trans = gameObject.transform;
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
        _iceShotAttack.PlayerAttack(_trans.position);
        yield return new WaitForSeconds(_iceShotAttack.GetCooldown());
        _canAttack = true;
    }

    private void FixedUpdate()
    {
        _movementInput = _inputActions.Player.Move.ReadValue<Vector2>();
        _movement = new Vector3(_movementInput.x, 0.0f, _movementInput.y);
        _rb.velocity = _movement * _movementSpeed;
    }
}
