using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BombPickup : MonoBehaviour
{
	public AudioClip pickupClip;
	public AudioMixer mixer;

	private Animator anim;              
	private bool landed = false;
	private LayBombs layBombs;


	void Awake()
	{
		anim = transform.root.GetComponent<Animator>();
		layBombs = GameObject.FindGameObjectWithTag("Player").GetComponent<LayBombs>();
	}


	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			AudioSource.PlayClipAtPoint(pickupClip, transform.position);
			mixer.SetFloat("ProbsMusic", 0);
			other.GetComponent<LayBombs>().bombCount++;

			Destroy(transform.root.gameObject);
		}
		else if (other.tag == "ground" && !landed)
		{
			anim.SetTrigger("Land");
			transform.parent = null;
			gameObject.AddComponent<Rigidbody2D>();
			landed = true;
		}
	}
}
