using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CountChildren : MonoBehaviour
{
    public float count = 0;

    // Update is called once per frame
    void Update()
    {
        count = 0;
        foreach (Transform child in transform)
        {
            count++;
        }
    }
}
