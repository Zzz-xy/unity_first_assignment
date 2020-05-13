using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket : MonoBehaviour
{
    public GameObject explosion;
    private enemy enemies;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2);
    }
    void OnExplode()
    {
        Quaternion randRotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));
        Instantiate(explosion, transform.position, randRotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        enemies = collision.GetComponent<enemy>();
        if (collision.tag != "Player")
        {
            OnExplode();
            Destroy(gameObject);
        }
        if (collision.tag == "Enemy")
        {
            enemies.Hurt();
        }
        
    }
}
