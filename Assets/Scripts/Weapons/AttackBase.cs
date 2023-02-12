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
    public virtual void PlayerAttack(Transform startingPos)
    {
        Vector3 targetPos;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            targetPos = hit.point;
            targetPos.y = startingPos.position.y;

            GameObject projectile = Instantiate(projectileType, startingPos.position, Quaternion.LookRotation(targetPos-startingPos.position));
            projectile.GetComponent<Rigidbody>().velocity = (targetPos - startingPos.position) * projectileVelocity;
            Destroy(projectile, lifespan);
        }
        else
        {
            Debug.Log("Mouse Raycast Failed");
        }
    }

    public float GetCooldown()
    {
        return cooldown;
    }
}
