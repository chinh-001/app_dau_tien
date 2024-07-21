using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_script : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public float start, end;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //vi tri nguoi choi 
        var player_x = player.transform.position.x;
        // vi tri camera
        var cam_x = transform.position.x;
        var cam_y = transform.position.y;
        var cam_z = transform.position.z;
        if (player_x > start && player_x < end)
        {

            cam_x = player_x;
        }
        else
        {
            if (player_x < start)
            {
                cam_x = start;
            }
            if (player_x > end)
            {
                cam_x = end;
            }
        }
        transform.position = new Vector3(cam_x, cam_y,cam_z);


    }
}
