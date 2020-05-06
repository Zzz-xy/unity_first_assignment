﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetParticalSortingLayer : MonoBehaviour
{
    public string sortingLayerName;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = sortingLayerName;
    }
}
