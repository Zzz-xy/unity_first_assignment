using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    public float spawnTime = 3f;
    public float spawnDelay = 4f;
    public float dropRangeLeft;
    public float dropRangeRight;
    public GameObject[] enemies;
    void Start()
    {
        InvokeRepeating("Spawn", spawnDelay, spawnTime);
    }

    void Spawn()
    {
        float dropPosX = Random.Range(dropRangeLeft, dropRangeRight);

        Vector3 dropPos = new Vector3(dropPosX, 15f, 1f);

        int enemyIndex = Random.Range(0, enemies.Length);
        Instantiate(enemies[enemyIndex], dropPos, Quaternion.identity);

        foreach (ParticleSystem p in GetComponentsInChildren<ParticleSystem>())
        {
            p.Play();
        }
    }

}
