using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Enemy _enemyType;
    [SerializeField] float _spawnFrequency;
    [SerializeField] GameObject[] _enemyObjects;

    bool readyToSpawn;
    Dictionary<Enemy, GameObject> _enemies;
    private void Awake()
    {
        readyToSpawn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (readyToSpawn)
        {
            switch (_enemyType)
            {
                case Enemy.chaser:
                    Debug.Log("Spawn Attempted");
                    GameObject enemy = Instantiate(_enemyObjects[0], gameObject.transform.position, Quaternion.identity);
                    break;
                default:
                    Debug.Log("Invalid Enemy Spawn");
                    break;
            }
            StartCoroutine(SpawnerCooldown());
        }
    }

    IEnumerator SpawnerCooldown()
    {
        readyToSpawn = false;
        yield return new WaitForSeconds(_spawnFrequency);
        readyToSpawn = true;
    }
}

public enum Enemy
{
    chaser
}
