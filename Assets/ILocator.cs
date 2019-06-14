using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ILocator : MonoBehaviour
{
    public Vector3 Position { get; set; }
    public Quaternion Orientation { get; set; }

    public abstract void ForceUpdate();
}
