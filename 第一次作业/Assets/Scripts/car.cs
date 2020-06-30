using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class car : MonoBehaviour
{
    public float moveSpeed;
    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(-transform.localScale.x * moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
    }
}
