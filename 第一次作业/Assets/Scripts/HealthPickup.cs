using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
	public float healthBonus;               
	//public AudioClip collect;               

	private PickupSpawner pickupSpawner;    
	private Animator anim;                  
	private bool landed=false;                  


	void Awake()
	{
		pickupSpawner = GameObject.Find("pickupManager").GetComponent<PickupSpawner>();
		anim = transform.root.GetComponent<Animator>();
	}


	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
			
			playerHealth.health += healthBonus;
			playerHealth.health = Mathf.Clamp(playerHealth.health, 0f, 100f);

			playerHealth.UpdateHealthBar();

	
			pickupSpawner.StartCoroutine(pickupSpawner.DeliverPickup());

			//AudioSource.PlayClipAtPoint(collect, transform.position);

			Destroy(transform.root.gameObject);
		}
		else if (other.tag == "ground" && !landed)
		{
			anim.SetTrigger("land");

			transform.parent = null;
			gameObject.AddComponent<Rigidbody2D>();
			landed = true;
		}
	}
}
