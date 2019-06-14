using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class drawmap : MonoBehaviour
{
    public GameObject hexobj;
    private Layout layout;

    private GameObject clone;

    public List<GameObject> neighbourObjects = new List<GameObject>(6);
    // Start is called before the first frame update
    void Start()
    {
        layout = new Layout(Layout.flat, new Point(1.1, 1.1), new Point(0, 0));

        float UPPER = 10;

        for (int r = 0; r < UPPER; r++)
        {
            for (int q = 0; q < UPPER; q++)
            {
                Point a = layout.HexToPixel(new Hex(q, r));
                Vector3 pos = new Vector3((float) a.x, 0, (float) a.y);
                Instantiate(hexobj, pos, Quaternion.identity);
            }
        }
    }

    private void FixedUpdate()
    {
        Hex hex;
        Vector3 vec3;

        if (Hex.HexRaycast(out hex, out vec3, layout))
        {
            hexobj.transform.position = vec3 + Vector3.up;
            for (int i = 0; i < 6; i++)
            {
                neighbourObjects[i].transform.position = Hex.HexToVec3(hex.Neighbor(i), layout, 2);
            }
        }
    }

 
}
