using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager<T> where T : Attack
{
    private List<T> attacks = new List<T>();

    public void AddAttack(T Attack)
    {
        attacks.Add(Attack);
    }

    //Use a given attack
    public Attack GetAttack(string name)
    {
        foreach (T Attack in attacks)
        {
            if (Attack.attackName == name)
            {
                return Attack;
            }
        }
        return null;
    }
}
