using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket : MonoBehaviour
{
    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2);
    }
    void OnExplode()
    {
        Quaternion randRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
        Instantiate(explosion, transform.position, randRotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
        {
            OnExplode();
            Destroy(gameObject);
        }
        if (collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<enemy>().Hurt();
            OnExplode();
            Destroy(gameObject);
        }
        
    }
}
