using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Audio;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;
    public float damageInterval = 0.35f;
    public float damageAmout = 10f;
    public float hurtForce = 100f;
    public AudioClip[] ouchClips;
    public AudioMixer mixer;

    private AudioSource audio;
    private SpriteRenderer healthBar;
    private float lastHurtTime;
    private Vector3 healthScale;
    private PlayerControl playerControl;
    private Rigidbody2D heroBody;
    private Animator anim;

    private void Awake()
    {
        healthBar = GameObject.Find("HealthBar").GetComponent<SpriteRenderer>();
        heroBody = GetComponent<Rigidbody2D>();
        playerControl = GetComponent<PlayerControl>();
        healthScale = healthBar.transform.localScale;
        audio = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    public void UpdateHealthBar()
    {
        healthBar.material.color = Color.Lerp(Color.green, Color.red, 1 - health * 0.01f);
        healthBar.transform.localScale = new Vector3(healthScale.x * health * 0.01f, 1, 1);
    }

    void TakeDamage(Transform EnemyTran)
    {
        playerControl.jump = false;
        Vector3 hurtVector3 = transform.position - EnemyTran.position + Vector3.up * 5f;
        heroBody.AddForce(hurtForce * hurtVector3);
        health -= damageAmout;
        if (health < 0) health = 0;

        UpdateHealthBar();

        if (audio != null)
        {
            if (!audio.isPlaying)
            {
                int i = Random.RandomRange(0, ouchClips.Length);
                audio.clip = ouchClips[i];
                audio.Play();
                mixer.SetFloat("HeroMusic", 0);
            }
        }
    }

    void Death()
    {
        

        Collider2D[] colliders = GetComponents<Collider2D>();
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].isTrigger = true;
        }
        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i].sortingLayerName = "UI";
        }
        playerControl.enabled = false;
        GetComponentInChildren<Gun>().enabled = false;
        anim.SetTrigger("die");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (Time.time > lastHurtTime + damageInterval)
            {
                if (health > 0)
                {
                    TakeDamage(collision.gameObject.transform);
                    lastHurtTime = Time.time;
                    if (health <= 0) Death();           
                }
                else
                {
                    Death();
                }
            }
        }
    }
}
