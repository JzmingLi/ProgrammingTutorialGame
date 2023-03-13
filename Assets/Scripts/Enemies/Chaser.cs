using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : MonoBehaviour
{
    PlayerController _player;
    Rigidbody _rb;
    Transform _trans;

    [SerializeField] float _movementSpeed;
    [SerializeField] float _damage;

    void Start()
    {
        _player = PlayerController.Instance;
        _rb = gameObject.GetComponent<Rigidbody>();
        _trans = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        _rb.velocity = (_player.gameObject.transform.position - _trans.position) * _movementSpeed;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _player.TakeDamage(_damage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerBullet")) Destroy(gameObject);
    }
}

