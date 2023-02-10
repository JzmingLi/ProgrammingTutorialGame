using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceShot : AttackBase
{
    private void Start()
    {
        _rb = projectileType.GetComponent<Rigidbody>();
        cooldown = 1.5f;
        lifespan = 3.0f;
        projectileVelocity = 5.0f;
        cam = Camera.main;
    }

    public override void PlayerAttack(Vector3 startingPos)
    {
        StartCoroutine(BurstDelay(startingPos));
    }

    IEnumerator BurstDelay(Vector3 startingPos)
    {
        for (int i = 0; i < 3; i++)
        {
            base.PlayerAttack(startingPos);
            yield return new WaitForSeconds(0.2f);
        }
    }
}
