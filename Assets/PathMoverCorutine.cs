using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMoverCorutine : MonoBehaviour
{
    public float smoothing = 7f;

    public List<Vector3> Targets
    {
        get => targets;
        set
        {
            if (value.Count > 0)
            {
                targets = value;
                current = 0;
                StopCoroutine("Movement");
                StartCoroutine("Movement", targets);
            }
        }
    }
    [SerializeField]
    private List<Vector3> targets;
    [SerializeField]
    private int current = 0;

    public bool moving = false;

    IEnumerator Movement(List<Vector3> targets)
    {

        while (current != targets.Count)
        {
            while (Vector3.Distance(transform.position, targets[current]) > 0.05f)
            {
                transform.position = Vector3.Lerp(transform.position, targets[current], smoothing * Time.deltaTime);

                yield return null;
            }

            current++;
            yield return null;
        }

        moving = false;
    }
}
