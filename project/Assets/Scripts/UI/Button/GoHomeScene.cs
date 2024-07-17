using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoHomeScene : MonoBehaviour
{
    public void GoHOmeScene()
    {
        Debug.Log("테스트");
        SceneManager.LoadScene("MainScene");
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}