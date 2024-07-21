using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playfab : MonoBehaviour
{

    public InputField pass;
    public InputField user;
    public InputField email;
    public Text messageText;
    public GameObject rowprefab;
    public Transform tbody;
    public int diem;
    // lưu điểm 
    //b1 : thiết kế giao diện :    1. có bảng   chứa bảng : tiêu đề , nội dung : các dòng chứa điểm 
    //  đưa dòng vào trong prefab để gọi lại '
    // b2 : tạo hàm chuyển đểm lên server( playfab), hàm hiển thị điểm dưới client
    // b3 : liên kết giao diện :



    public void btn_dang_nhap()
    {
        var req = new LoginWithPlayFabRequest
        {
            Username = user.text,
            Password = pass.text
        };
        PlayFabClientAPI.LoginWithPlayFab(req, dang_nhap_thanh_cong, loi_dang_nhap);
    }
    void dang_nhap_thanh_cong(LoginResult loginResult)
    {
        messageText.text = "đăng nhập thành công ";
        SceneManager.LoadScene("man_1");
        //lưu điểm vào tài khoản đăng nhập
        //gửi điểm lên server
        //SendServer(0); // hàm gửi điểm lên server
    }
    void loi_dang_nhap(PlayFabError error)
    {
        messageText.text = error.ErrorMessage;
        Debug.Log(error.ErrorMessage);
    }


    public  void SendServer(int d)
    {
        var req = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate> { new StatisticUpdate()
            {
            StatisticName="bangdiem102",// ten bang dim tao tren playdfab
            Value =d
            }

            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(req, update_thanh_cong, loi_dang_nhap);
    }
    public  void update_thanh_cong(UpdatePlayerStatisticsResult result) {
        Debug.Log("update Sucess");
    }

    public void btn_dang_ki()
    {
        var req = new RegisterPlayFabUserRequest
        {
            Username = user.text,
            Email = email.text,
            Password = pass.text,
            DisplayName = user.text,
            RequireBothUsernameAndEmail = false
         };
        PlayFabClientAPI.RegisterPlayFabUser(req,dang_ki_thanh_cong,loi_dang_ki);

    }

    void dang_ki_thanh_cong(RegisterPlayFabUserResult result)
    {
        messageText.text = "đăng kí thành công ";

    }
    void loi_dang_ki(PlayFabError error)
    {
        messageText.text = error.ErrorMessage;
        Debug.Log(error.ErrorMessage);

    }


    public void getBD()
    {
        var req = new GetLeaderboardRequest
        {
            StatisticName = "bangdiem102", //  "Bangdiem",
            StartPosition = 0,
            MaxResultsCount = 5
        };
        PlayFabClientAPI.GetLeaderboard(req, onLeaderboardGet, loi_dang_nhap);
    }
    void onLeaderboardGet(GetLeaderboardResult result)
    {
        foreach (Transform item in tbody)
        {
            Destroy(item.gameObject);
        }
        foreach (var item in result.Leaderboard)
        {
            GameObject newGo = Instantiate(rowprefab, tbody);
            Text[] texts = newGo.GetComponentsInChildren<Text>();
            texts[0].text = (item.Position + 1).ToString();
            texts[1].text = item.DisplayName.ToString();
            texts[2].text = item.StatValue.ToString();
            Debug.Log(item.Position + " " + item.PlayFabId + item.StatValue);
        }
    }

}
