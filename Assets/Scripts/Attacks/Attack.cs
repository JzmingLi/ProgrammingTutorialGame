using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack
{
    public GameObject projectileType;
    public float cooldown;
    public float lifespan;
    public float projectileVelocity;
    public int projectileCount = 1;
    public float projectileDelay = 0;
    public string attackName;

    public Attack(GameObject pT, string name, float cd, float lS, float pV, int pC, float pD)
    {
        projectileType = pT;
        attackName = name;
        cooldown = cd;
        lifespan = lS;
        projectileVelocity = pV;
        projectileCount = pC;
        projectileDelay = pD;
    }

    public Attack(GameObject pT, string name, float cd, float lS, int pV)
    {
        projectileType = pT;
        attackName = name;
        cooldown = cd;
        lifespan = lS;
        projectileVelocity = pV;
        projectileCount = 1;
        projectileDelay = 0;
    }
}
