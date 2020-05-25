using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float xDistance = 2f;
    public float yDistance = 2f;
    public float xSmooth = 5f;
    public float ySmooth = 5f;
    public Vector2 maxXAndY;
    public Vector2 minXAndY;

    public Transform player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    bool CheckXDistance()
    {
       return Mathf.Abs(transform.position.x - player.position.x) > xDistance;
    }
    bool CheckYDistance()
    {
       return Mathf.Abs(transform.position.y - player.position.y) > yDistance;
    }
    
    void TrackPlayer()
    {
        float fTargetX = transform.position.x;
        float fTargetY = transform.position.y;

        if (CheckXDistance())
        {
            fTargetX = Mathf.Lerp(transform.position.x, player.transform.position.x, Time.deltaTime * xSmooth);
            fTargetX = Mathf.Clamp(fTargetX, minXAndY.x, maxXAndY.x);
        }
        if (CheckYDistance())
        {
            fTargetY = Mathf.Lerp(transform.position.y, player.transform.position.y, Time.deltaTime * ySmooth);
            fTargetY = Mathf.Clamp(fTargetY, minXAndY.y, maxXAndY.y);
        }

        transform.position = new Vector3(fTargetX, fTargetY, transform.position.z);
    }

    private void FixedUpdate()
    {
        TrackPlayer();
        Debug.Log(Time.deltaTime);
    }
}
