using System.Threading.Tasks;

// 뒤끝 SDK namespace 추가
using BackEnd;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackendManager : MonoBehaviour
{
    public bool isLoginSuccess = false;

    public void Init()
    {
        var bro = Backend.Initialize(true); // 뒤끝 초기화

        // 뒤끝 초기화에 대한 응답값
        if (bro.IsSuccess())
        {
            Debug.Log("초기화 성공 : " + bro); // 성공일 경우 statusCode 204 Success
        }
        else
        {
            Debug.LogError("초기화 실패 : " + bro); // 실패일 경우 statusCode 400대 에러 발생 
        }
    }

    // 동기 함수를 비동기에서 호출하게 해주는 함수(유니티 UI 접근 불가)
    //async void Test()
    //{
    //    await Task.Run(() => {
    //        BackendLogin.Instance.CustomLogin("user1", "1234"); // 뒤끝 로그인

    //        BackendGameData.Instance.GameDataGet(); // 데이터 삽입 함수

    //        // [추가] 서버에 불러온 데이터가 존재하지 않을 경우, 데이터를 새로 생성하여 삽입
    //        if (BackendGameData.userData == null)
    //        {
    //            BackendGameData.Instance.GameDataInsert();
    //        }

    //        BackendGameData.Instance.LevelUp(); // [추가] 로컬에 저장된 데이터를 변경

    //        BackendGameData.Instance.GameDataUpdate(); //[추가] 서버에 저장된 데이터를 덮어쓰기(변경된 부분만)

    //        Debug.Log("테스트를 종료합니다.");
    //    });
    //}

    private void Update()
    {
        if (isLoginSuccess)
        {
            if ((Define.Scene)SceneManager.GetActiveScene().buildIndex == Define.Scene.Title)
            {
                FindObjectOfType<LoginPannel>().SuccessGoogleLogin();
            }
        }
    }

    public void StartGoogleLogin()
    {
        TheBackend.ToolKit.GoogleLogin.Android.GoogleLogin(GoogleLoginCallback);
    }
    private void GoogleLoginCallback(bool isSuccess, string errorMessage, string token)
    {
        if (isSuccess == false)
        {
            Debug.LogError(errorMessage);
            return;
        }

        var bro = Backend.BMember.AuthorizeFederation(token, FederationType.Google);
        Debug.Log("페데레이션 로그인 결과 : " + bro);

        isLoginSuccess = bro.IsSuccess();
    }

    public void SignOutGoogleLogin()
    {
        TheBackend.ToolKit.GoogleLogin.Android.GoogleSignOut(GoogleSignOutCallback);
    }
    private void GoogleSignOutCallback(bool isSuccess, string error)
    {
        if (isSuccess == false)
        {
            Debug.Log("구글 로그아웃 에러 응답 발생 : " + error);
        }
        else
        {
            Debug.Log("로그아웃 성공");
        }
    }
}