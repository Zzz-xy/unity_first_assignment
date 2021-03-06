﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Rigidbody2D rocket;
    public float speed = 20f;

    public AudioClip clip;

    private PlayerControl playerCtrl;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        playerCtrl = transform.root.GetComponent<PlayerControl>();
        anim = transform.root.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetButtonDown("Fire1"))
        { 
            anim.SetTrigger("shoot");
            GetComponent<AudioSource>().clip = clip;
            GetComponent<AudioSource>().Play();

            if (playerCtrl.faceRight)
            {
                Rigidbody2D bullet = Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                bullet.velocity = new Vector2(speed, 0);
            }
            else
            {
                Rigidbody2D bullet = Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0, 0, 180)));
                bullet.velocity = new Vector2(-speed, 0);
            }
        }
    }
}
