using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnbus : MonoBehaviour
{
	public float spawnTime = 10f;
	public float spawnDelay = 5f;
	public GameObject bus;

	void Start()
	{
		InvokeRepeating("Spawn", spawnDelay, spawnTime);
	}

	void Spawn()
	{
		Instantiate(bus, transform.position, transform.rotation);

		foreach (ParticleSystem p in GetComponentsInChildren<ParticleSystem>())
		{
			p.Play();
		}
	}
}
