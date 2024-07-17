using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 뒤끝 SDK namespace 추가
using BackEnd;
using UnityEngine.SceneManagement;

public class BackendLogin
{
    //private static BackendLogin _instance = null;

    //public static BackendLogin Instance
    //{
    //    get
    //    {
    //        if (_instance == null)
    //        {
    //            _instance = new BackendLogin();
    //        }

    //        return _instance;
    //    }
    //}

    public void GestLogin()
    {
        BackendReturnObject bro = Backend.BMember.GuestLogin("게스트 로그인으로 로그인함");

        if (bro.IsSuccess())
        {
            Debug.Log("게스트 로그인에 성공했습니다");

            //Param param = new Param()
            //{
            //    { "GOLD", 0 }
            //};

            //Managers.Data.GameDataInsert("USER_DATA", param);

            // 서버 데이터를 세이브 파일에 덮어쓰기
            Managers.Data.LoadServerDataToSaveFile();

            // Main Scene으로 이동
            SceneManager.LoadScene("MainScene");
           Managers.Data.GetSaveFile();
            //Managers.Data.PushData();
        }
    }

    public void CustomSignUp(string id, string pw)
    {
        Debug.Log("회원가입을 요청합니다.");

        var bro = Backend.BMember.CustomSignUp(id, pw);

        if (bro.IsSuccess())
        {
            Debug.Log("회원가입에 성공했습니다. : " + bro);
        }
        else
        {
            Debug.LogError("회원가입에 실패했습니다. : " + bro);
        }
    }

    public void CustomLogin(string id, string pw)
    {
        Debug.Log("로그인을 요청합니다.");

        var bro = Backend.BMember.CustomLogin(id, pw);

        if (bro.IsSuccess())
        {
            Debug.Log("로그인이 성공했습니다. : " + bro);
        }
        else
        {
            Debug.LogError("로그인이 실패했습니다. : " + bro);
        }
    }

    //public void UpdateNickname(string nickname)
    //{
    //    Debug.Log("닉네임 변경을 요청합니다.");

    //    var bro = Backend.BMember.UpdateNickname(nickname);

    //    if (bro.IsSuccess())
    //    {
    //        Debug.Log("닉네임 변경에 성공했습니다 : " + bro);
    //    }
    //    else
    //    {
    //        Debug.LogError("닉네임 변경에 실패했습니다 : " + bro);
    //    }
    //}
}