using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
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
    private SpriteRenderer ren;
    private Rigidbody2D enemyBody;
    private bool bDeath = false;
    private Score score;

    // Start is called before the first frame update
    void Start()
    {
        frontCheck = transform.Find("frontCheck");
        ren = transform.Find("alienShip").GetComponent<SpriteRenderer>();
        enemyBody = GetComponent<Rigidbody2D>();
        score = GameObject.Find("Score").GetComponent<Score>();
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

        if (HP == 1&&hurtedEnemy!=null)
        {
            ren.sprite = hurtedEnemy;
        }

        if (HP == 0 && !bDeath)
        {
            Death();
        }
    }

    void Death()
    {
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

        int j = Random.Range(0, deathClips.Length);
        AudioSource.PlayClipAtPoint(deathClips[j], transform.position);
        mixer.SetFloat("ProbsMusic", 0);
        
        score.score += 100;
        Vector3 scorePos;
        scorePos = transform.position;
        scorePos.y += 1.5f;

        Instantiate(hundredPointsUI, scorePos, Quaternion.identity);
    }
}
