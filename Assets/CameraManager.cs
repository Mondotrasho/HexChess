using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public ILocator[] target;
    public float poslerp = 0.5f;
    public float rotlerp = 0.5f;
    private float speed = 1;
    public int current_cam = 0;

    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            current_cam++;
            if (current_cam == target.Length)
            {
                current_cam = 0;
            }
        }
    }

    private void LateUpdate()
    {
        target[current_cam].ForceUpdate();

        Vector3 new_desired_pos = target[current_cam].Position;
        Quaternion new_desired_rot = target[current_cam].Orientation;

        var lerped_pos = Vector3.Lerp(transform.position, new_desired_pos, poslerp);
        transform.position = lerped_pos;

        var lerped_orientation = Quaternion.Slerp(transform.rotation, new_desired_rot, rotlerp);
        transform.rotation = lerped_orientation;
    }
}
