using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bob : MonoBehaviour
{
    public float originalY = 3;

    public float floatStrength = .1f;

    public float speed = 1;

    public float timeOffset = 0;

    public 

    void Update()
    {
        transform.position = new Vector3(transform.position.x,
            originalY + ((float)Math.Sin(timeOffset + Time.time * speed) * floatStrength),
            transform.position.z);
    }
}
