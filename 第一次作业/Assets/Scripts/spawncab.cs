using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawncab : MonoBehaviour
{
	public float spawnTime = 5f;        
	public float spawnDelay = 3f;       
	public GameObject cab;        

	void Start()
	{
		InvokeRepeating("Spawn", spawnDelay, spawnTime);
	}

	void Spawn()
	{
		Instantiate(cab, transform.position, transform.rotation);

		foreach (ParticleSystem p in GetComponentsInChildren<ParticleSystem>())
		{
			p.Play();
		}
	}
}
