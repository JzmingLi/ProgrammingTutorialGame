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
    protected Camera cam;
    public virtual void PlayerAttack(Vector3 startingPos)
    {
        //Doesn't work as intended
        Vector3 targetedPos = cam.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log(targetedPos);
        Debug.Log(startingPos);
        GameObject projectile = Instantiate(projectileType, startingPos, Quaternion.Euler((targetedPos - startingPos).normalized));
        projectile.GetComponent<Rigidbody>().velocity = (targetedPos - startingPos).normalized * projectileVelocity;
        Destroy(projectile, lifespan);
    }

    public float GetCooldown()
    {
        return cooldown;
    }
}
