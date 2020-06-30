using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;

public class enemy : MonoBehaviour
{
    public float moveSpeed = 2f;
    public int HP = 2;
    public Sprite deadEnemy;
    public Sprite hurtedEnemy;
    public float deathSpinMin = -100f;
    public float deathSpinMax = 100f;
    public AudioClip[] deathClips;
    public AudioMixer mixer;
    public GameObject hundredPointsUI;

    private Transform frontCheck;
    private AudioSource audio;
    private SpriteRenderer ren;
    private Rigidbody2D enemyBody;
    private bool bDeath = false;

    // Start is called before the first frame update
    void Start()
    {
        frontCheck = transform.Find("frontCheck");
        ren = transform.Find("body").GetComponent<SpriteRenderer>();
        enemyBody = GetComponent<Rigidbody2D>();
    }

    void flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void Hurt()
    {
        HP--;
    }

    private void FixedUpdate()
    {
        int nLayer = 1 << LayerMask.NameToLayer("Wall");
        Collider2D[] frontHits = Physics2D.OverlapPointAll(frontCheck.position, nLayer);
        foreach(Collider2D c in frontHits)
        {
            if (c.tag == "wall")
            {
                flip();
                break;
            }
              
        }
        enemyBody.velocity = new Vector2(moveSpeed * transform.localScale.x, enemyBody.velocity.y);
 
        if (HP == 0 && !bDeath)
        {
            Death();
        }
        if (HP == 1&&hurtedEnemy!=null)
        {
            ren.sprite = hurtedEnemy;
        }

       
    }

    void Death()
    {
        Score scorefollow = GameObject.FindGameObjectWithTag("Score").GetComponent<Score>();
        scorefollow.addscore();
        Text text = GameObject.FindGameObjectWithTag("ShowScore").GetComponent<Text>();
        text.text = scorefollow.score.ToString();

        SpriteRenderer[] renders = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer s in renders) s.enabled = false;

        ren.enabled = true;
        if (deadEnemy != null) ren.sprite = deadEnemy;

        enemyBody.AddTorque(Random.Range(deathSpinMin, deathSpinMax));

        Collider2D[] colliders = GetComponents<Collider2D>();
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].isTrigger = true;
        }

        if (audio != null)
        {
            if (!audio.isPlaying)
            {
                int j = Random.RandomRange(0, deathClips.Length);
                audio.clip = deathClips[j];
                audio.Play();
                mixer.SetFloat("ProbsMusic", 0);
            }
        }

        if (hundredPointsUI!=null)
        {
            Vector3 scorePos;
            scorePos = transform.position;
            scorePos.y += 1.5f;
            Instantiate(hundredPointsUI, scorePos, Quaternion.identity);
        }
        bDeath = true;
    }
}
