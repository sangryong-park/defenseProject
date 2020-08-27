using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeCircle : MonoBehaviour
{
    static RangeCircle instance;
    public static RangeCircle Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<RangeCircle>();
            }

            return instance;
        }

    }

    public int segmets;
    public float xradius;
    public float yradius;
    LineRenderer line;

    // Start is called before the first frame update
    void Start()
    {
        segmets = 30;
        line = gameObject.GetComponent<LineRenderer>();
        line.positionCount = segmets + 1;
        line.useWorldSpace = false;
   
    }

    // Update is called once per frame
    void Update()
    {
  
    }

    public void CreatePoints(float range , Transform pos)
    {
        float x;
        float y;
        float z = 0f;

        transform.position = pos.transform.position + new Vector3(0,0.2f,0);
        transform.SetParent(pos);

        float angle = 20f;

        for (int i = 0; i < (segmets+1); i++)
        {
            x = Mathf.Cos(Mathf.Deg2Rad * angle) * range;
            y = Mathf.Sin(Mathf.Deg2Rad * angle) * range;

            line.SetPosition(i, new Vector3(x, z, y));
            angle += (360f / segmets);

        }
    }



}
