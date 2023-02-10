using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackBase : MonoBehaviour
{
    [SerializeField] protected GameObject projectileType;
    protected Rigidbody _rb;
    protected float cooldown;
    protected float lifespan;
    protected float projectileVelocity;

    public virtual void PlayerAttack(Vector3 startingPos)
    {
        Vector3 targetedPos = Input.mousePosition;
        targetedPos.y = 0.0f;
        GameObject projectile = Instantiate(projectileType, startingPos, Quaternion.Euler(startingPos - targetedPos));
        projectile.GetComponent<Rigidbody>().velocity = (startingPos - targetedPos) * projectileVelocity;
        Destroy(projectile, lifespan);
    }

    public float GetCooldown()
    {
        return cooldown;
    }
}
