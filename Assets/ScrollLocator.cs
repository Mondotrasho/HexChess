using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollLocator : ILocator
{
    //the game object that sits on the head position
    public GameObject headmark;
    private Vector3 headpos;

    //speed/multi for mouse input
    public float mouseMulti = 5;
    private float MouseWheel = 50;

    //the Radius of the orbit of the camera
    [Range(-40, 40)]
    public float rad = -25f;

    [Range(-180, 180)]
    public float DownAngleMax = -10f;
    [Range(-180, 180)]
    public float UpAngleMax = 90f;
    // Start is called before the first frame update
    void Start()
    {
        //store for repeated access
        headpos = headmark.transform.position;

        //where to first lerp
        Position = headpos + new Vector3(Mathf.Cos(-MouseWheel * Time.fixedDeltaTime) * rad, Mathf.Sin(-MouseWheel * Time.fixedDeltaTime) * rad, 0);
        Orientation = Quaternion.Euler(50, 90, 0);
    }

    public override void ForceUpdate()
    {
        MouseWheel += Input.mouseScrollDelta.y * mouseMulti;

        //the euler angle is used to lock rotation between a ranch 60 from the rotation
        var rotR = headmark.transform.rotation.eulerAngles.x + UpAngleMax;
        var rotL = headmark.transform.rotation.eulerAngles.x - DownAngleMax;

        MouseWheel = Mathf.Clamp(MouseWheel, rotL, rotR);

        headpos = headmark.transform.position;

        Position = headpos + new Vector3(Mathf.Cos(-MouseWheel * Time.fixedDeltaTime) * rad, Mathf.Sin(-MouseWheel * Time.fixedDeltaTime) * rad, 0);
        Orientation = Quaternion.Euler(MouseWheel, 90, 0);
    }
}
