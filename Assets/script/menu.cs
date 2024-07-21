using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menu : MonoBehaviour
{
    public GameObject option;
    public Slider amthanh;
    public Text text_thua;

    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
    public  void pause()
        {
            // dung man choi
            Time.timeScale = 0;

        }
    public  void chay_tiep()
    {
        // dung man choi
        Time.timeScale = 1f;

    }

    public void an_option()
    {
        // ẩn pannel 
        option.SetActive(false);
    }
    public void hien_thi_option()
    {
        // hiện  pannel 
        option.SetActive(true);
    }

    public void chinh_am_thanh()
    {
        AudioListener.volume = amthanh.value;

    }
}
