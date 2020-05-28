using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerControl : MonoBehaviour
{
    public float moveForce = 400f;
    public float maxSpeed = 5f;
    public float jumpForce = 100;
    [HideInInspector]
    public bool jump = false;
    [HideInInspector]
    public bool faceRight = true;
    public AudioClip[] jumpClips;
    public AudioMixer mixer;
    public AudioClip[] taunts;
    public float tauntProbability = 50f;   
    public float tauntDelay = 1f;

    private AudioSource audio;
    private int tauntIndex;
    private bool grounded = false;
    private Transform groundCheck;
    private Rigidbody2D heroBody;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    { 
        heroBody = GetComponent<Rigidbody2D>();
        groundCheck = transform.Find("GroundCheck");
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        //获取水平方向输入
        float h = Input.GetAxis("Horizontal");

        if (h * heroBody.velocity.x < maxSpeed)
            heroBody.AddForce(Vector2.right * h * moveForce);

        if (Mathf.Abs(heroBody.velocity.x) > maxSpeed)
            heroBody.velocity = new Vector2(Mathf.Sign(heroBody.velocity.x) * maxSpeed, heroBody.velocity.y);

        anim.SetFloat("speed", Mathf.Abs(h));

        if (h > 0 && !faceRight)
            flip();
        if (h < 0 && faceRight)
            flip();
        if (jump)
        {
            anim.SetTrigger("jump");
            heroBody.AddForce(new Vector2(0, jumpForce));
            jump = false;

            if (audio != null)
            {
                if (!audio.isPlaying)
                {
                    int i = Random.RandomRange(0, jumpClips.Length);
                    audio.clip = jumpClips[i];
                    audio.Play();
                    mixer.SetFloat("HeroMusic", 0);
                }
            }
        }

    }
    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        if (Input.GetButtonDown("Jump") && grounded)
            jump = true;
    }

    void flip()
    {
        faceRight = !faceRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public IEnumerator Taunt()
    {
        // Check the random chance of taunting.
        float tauntChance = Random.Range(0f, 100f);
        if (tauntChance > tauntProbability)
        {
            // Wait for tauntDelay number of seconds.
            yield return new WaitForSeconds(tauntDelay);

            // If there is no clip currently playing.
            if (!GetComponent<AudioSource>().isPlaying)
            {
                // Choose a random, but different taunt.
                tauntIndex = TauntRandom();

                // Play the new taunt.
                GetComponent<AudioSource>().clip = taunts[tauntIndex];
                GetComponent<AudioSource>().Play();
            }
        }
    }


    int TauntRandom()
    {
        // Choose a random index of the taunts array.
        int i = Random.Range(0, taunts.Length);

        // If it's the same as the previous taunt...
        if (i == tauntIndex)
            // ... try another random taunt.
            return TauntRandom();
        else
            // Otherwise return this index.
            return i;
    }
}
