using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] GameObject _projectileObject;
    bool _canAttack;
    AttackManager<Attack> _attackManager;

    void Start()
    {
        _attackManager = new AttackManager<Attack>();

        //Default Attacks
        Attack primaryAttack = new Attack(_projectileObject, "Ice Burst", 1.5f, 3, 10, 3, 0.2f);
        Attack secondaryAttack = new Attack(_projectileObject, "Ice Shot", 1.5f, 5, 20);
        _attackManager.AddAttack(primaryAttack);
        _attackManager.AddAttack(secondaryAttack);

        _canAttack = true;
    }

    public void AttackAction(string name)
    {
        if (_canAttack)
        {
            Attack attack = _attackManager.GetAttack(name);

            GameObject projectileType = attack.projectileType;
            float cooldown = attack.cooldown;
            float lifespan = attack.lifespan;
            float projectileVelocity = attack.projectileVelocity;
            int projectileCount = attack.projectileCount;
            float projectileDelay = attack.projectileDelay;

            if (projectileCount > 1) StartCoroutine(MultiAttackWithDelay(projectileType, lifespan, projectileVelocity, projectileCount, projectileDelay));
            else ExecuteAttack(projectileType, lifespan, projectileVelocity);
            StartCoroutine(AttackCooldown(cooldown));
        }
    }

    IEnumerator MultiAttackWithDelay(GameObject pT, float lS, float pV, int pC, float pD)
    {
        for (int i = 0; i < pC; i++)
        {
            ExecuteAttack(pT, lS, pV);
            yield return new WaitForSeconds(pD);
        }
    }

    void ExecuteAttack(GameObject pT, float lS, float pV)
    {
        Transform startingPos = PlayerController.Instance.gameObject.transform;
        Vector3 targetPos;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            targetPos = hit.point;
            targetPos.y = startingPos.position.y;

            GameObject projectile = Instantiate(pT, startingPos.position, Quaternion.LookRotation(targetPos - startingPos.position));
            projectile.GetComponent<Rigidbody>().velocity = (targetPos - startingPos.position).normalized * pV;
            Destroy(projectile, lS);
        }
        else
        {
            Debug.Log("Mouse Raycast Failed");
        }
    }

    IEnumerator AttackCooldown(float cd)
    {
        _canAttack = false;
        yield return new WaitForSeconds(cd);
        _canAttack = true;
    }
}
