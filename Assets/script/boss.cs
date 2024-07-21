using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss : MonoBehaviour
{
    // Start is called before the first frame update
    public float left, right;
    private bool is_right;

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
         var boss_x = transform.position.x;
        if (boss_x < left)
        {
            is_right = true;

        }
        if (boss_x >right)
        {
            is_right = false;

        } if (is_right)
        {
            transform.Translate(new Vector3(Time.deltaTime * 3, 0, 0));
          Vector2 scale = transform.localScale;
            scale.x *= scale.x > 0 ? -1 : 1;
            transform.localScale = scale;
        }
        else
        {
        transform.Translate(new Vector3(-Time.deltaTime * 3, 0, 0));
            

  Vector2 scale = transform.localScale;
            scale.x *= scale.x > 0 ? 1 : -1;
            transform.localScale = scale;

        }
    }
}
