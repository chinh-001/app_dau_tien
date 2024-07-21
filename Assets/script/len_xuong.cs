using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class len_xuong : MonoBehaviour
{
    // Start is called before the first frame update
    public float up_max, down_min;
    private bool is_up;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var gach_y = transform.position.y;

        if (gach_y > up_max)
        {

            is_up =false;

        }if (gach_y <down_min)
        {

            is_up = true;

        }if (is_up)
        {

            transform.Translate(new Vector3(0, Time.deltaTime*3, 0));

        }
        else {
            transform.Translate(new Vector3(0, -Time.deltaTime*3, 0));
        }

    }
}
