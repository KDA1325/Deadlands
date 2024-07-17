using BackEnd;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginPannel : MonoBehaviour
{
    [SerializeField] GameObject NickNamePannel;
    [SerializeField] GameObject nickNameWarning_Pannel;
    [SerializeField] string nickName = "";

    public void GestLogin()
    {
        Managers.BLogin.GestLogin();
    }

    public void GoogleLogin()
    {
        Managers.BackEnd.StartGoogleLogin();
    }

    public void SuccessGoogleLogin()
    {
        Managers.Data.token = Define.googleToken;

        // 유저 닉네임이 있는지 확인
        if (string.IsNullOrEmpty(Backend.UserNickName))
        {
            // 닉네임이 없다면 닉네임 생성창 열기
            NickNamePannel.SetActive(true);
        }
        else
        {
            // 닉네임이 있다면 Main Scene으로 가기
            SceneManager.LoadScene("MainScene");
        }
    }
    public void SetNickName(string _nickName)
    {
        nickName = _nickName;
    }
    public void CreateNickName()
    {
        var bro = Backend.BMember.UpdateNickname($"{nickName}");

        if (bro.IsSuccess() == false)
        {
            BackendReturnObject DuplicationCheck = Backend.BMember.CheckNicknameDuplication($"{nickName}");

            // 닉네임이 적절하지 않을 경우
            if (DuplicationCheck.IsSuccess() == false)
            {
                nickNameWarning_Pannel.SetActive(true);
                nickNameWarning_Pannel.GetComponentInChildren<TextMeshProUGUI>().text = "Wrong NickName";
                return;
            }
        }
        else if (bro.IsSuccess())
        {
            UserInit();
        }
    }

    void UserInit()
    {
        // 랭킹에 필요한 초기 데이터 삽입
        //BackendRank.Instance.RankInsert(0);
        //BackendGameData.Instance.GameDataInsert(Define.clearMapCount, 0);
        //BackendGameData.Instance.GameDataInsert(Define.totalClearTime, 0);
        
        // 로그인 종류에 따른 데이터 저장 
        Managers.Data.WriteLoginData();

        // Main Scene으로 이동
        SceneManager.LoadScene("MainScene");
    }
}
