using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class move : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    public bool isRight = true;
    private Rigidbody2D rb;
    private bool nen;
    public AudioSource audio_coin;
    public Text txt_coin;
    int diemcoin = 0;// điểm coin
    public bool key = false;
    public GameObject panel, button, text, thoat, txt_chien_thang;


    public GameObject hoa;


    //panel
    public GameObject panel_dead;



    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        panel.SetActive(false);
        button.SetActive(false);
        text.SetActive(false);
        thoat.SetActive(true);
        txt_chien_thang.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            isRight = true;

            animator.SetBool("isRunning", true);
            transform.Translate(Time.deltaTime * 5, 0, 0);
            //rb.AddForce(new Vector2(0.f, 0));
            Vector2 scale = transform.localScale;
            scale.x *= scale.x > 0 ? -1 : 1;
            transform.localScale = scale;
        }

        // dan ban
        if (Input.GetKey(KeyCode.K))
        {
            var x = transform.position.x;
            var y = transform.position.y;
            var z = transform.position.z;
            Instantiate(Resources.Load<GameObject>("Assets/prefab/bullet"), new Vector3(x, y, z), Quaternion.identity);
        }


        else
        {
            animator.SetBool("isRunning", false);
        }

        if (nen)
        {

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow))
            {
                if (isRight)
                {
                    //transform.Translate(Time.deltaTime * 3, Time.deltaTime * 500 , 0);
                    rb.AddForce(new Vector2(100, 300));
                    Vector2 scale = transform.localScale;
                    scale.x *= scale.x > 0 ? -1 : 1;
                    transform.localScale = scale;
                }
                else
                {

                    //transform.Translate(-Time.deltaTime * 3, Time.deltaTime * 500 , 0);
                    rb.AddForce(new Vector2(-100, 300));
                    Vector2 scale = transform.localScale;
                    scale.x *= scale.x > 0 ? -1 : 1;
                    transform.localScale = scale;
                }
                nen = false;
            }
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            isRight = false;
            animator.SetBool("isRunning", true);
            transform.Translate(-Time.deltaTime * 5, 0, 0);
            //rb.AddForce(new Vector2(-2f, 0));
            Vector2 scale = transform.localScale;
            scale.x *= scale.x > 0 ? 1 : -1;
            transform.localScale = scale;
        }


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "nen_dat")
        {
            nen = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "coin")
        {
            // tăng số lượng đồng xu đã ăn
            // xóa đồng xu
            diemcoin++;
            Destroy(collision.gameObject);//xóa đồng xu
            txt_coin.text = "điểm coin : "+diemcoin.ToString();
            Debug.Log("điểm coin: " + diemcoin);
            // goi am thanh liên kết với ngoài giao diện 
            audio_coin.Play();
     // tạo đối tượng  instantiate (tạo ai, ở đâu , góc xoay thế nào)
        Instantiate(hoa, gameObject.transform.position, transform.rotation);
            StartCoroutine(TatHieuUng(hoa, 2f));
        } 
        if (collision.gameObject.tag == "big_coin")
        {
            // tăng số lượng đồng xu đã ăn
            // xóa đồng xu
            diemcoin+=2;
            Destroy(collision.gameObject);//xóa đồng xu
            txt_coin.text = "điểm coin : "+diemcoin.ToString();
            Debug.Log("điểm coin: " + diemcoin);
            // goi am thanh liên kết với ngoài giao diện 
            audio_coin.Play();
            Instantiate(hoa, gameObject.transform.position, transform.rotation);
            //StartCoroutine(TatHieuUng(hoa, 5f));
            // dua diem len servers
            playfab pf = new playfab();
            pf.SendServer(diemcoin);

        }

        if (collision.gameObject.tag=="tren")
        {
            // tiêu diệt boss theo tên trong hirachy khi mình va chạm , phân biệt với mấy boss khác
            var name = collision.attachedRigidbody.name;
            Destroy(GameObject.Find(name));
        }

        if (collision.gameObject.tag == "trai")
        {
            //hienthioption();
            Time.timeScale = 0;
            panel.SetActive(true);
            button.SetActive(true);
            text.SetActive(true);
            thoat.SetActive(false);
        } if (collision.gameObject.tag == "chong_gai")
        {
            //hienthioption();
            Time.timeScale = 0;
            panel.SetActive(true);
            button.SetActive(true);
            text.SetActive(true);
            thoat.SetActive(false);
        }
        
         

        // khai báo biến chứa chìa khóa.
        if (collision.gameObject.tag == "key")
        {
            key = true;//đã có chìa khóa
            DestroyObject(collision.gameObject);
            Debug.Log("key có");
            audio_coin.Play();
            //Instantiate(hoa, gameObject.transform.position, transform.rotation);

        } if (collision.gameObject.tag == "key_chien_thang")
        {
            key = true;//đã có chìa khóa
            DestroyObject(collision.gameObject);
            Debug.Log("key có");
            txt_chien_thang.SetActive(true);
            audio_coin.Play();
        }
        if (collision.gameObject.tag == "gate_1")
        {
            // nếu có chìa khóa thì vào màn sau. 
            if (key)
            {
                SceneManager.LoadScene("man_2");
            }
        } if (collision.gameObject.tag == "gate_2")
        {
            // nếu có chìa khóa thì vào màn sau. 
            if (key)
            {
                SceneManager.LoadScene("man_3");
            }
        }
   
    }


    //tắt particle system sau 1 thời gian
    IEnumerator TatHieuUng(GameObject hieuUng, float delay)
    {
        // Chờ đợi 5 giây
        yield return new WaitForSeconds(delay);

        // Tắt hiệu ứng Particle System
        DestroyImmediate(hieuUng, true);
    }




    //public void pause()
    //{
    //    Time.timeScale = 0;
    //}

    //public void resume()
    //{

    //    //timescale co the thay doi toc do tro choi
    //    Time.timeScale = 1.0f;
    //}

    //public void hienthioption()
    //{
    //    panel_dead.SetActive(true);
    //    pause();
    //}
    //public void exitpanel()
    //{
    //    panel_dead.SetActive(false);
    //    resume();
    //}
}
