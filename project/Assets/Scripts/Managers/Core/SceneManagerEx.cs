using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    BaseScene _script;

    Define.Scene _currentScene = Define.Scene.None;

    public Define.Scene GetScene()
    {
        return _currentScene;
    }
    public Define.Scene SetScene(Define.Scene scene, BaseScene script)
    {
        _currentScene = scene;
        _script = script;

        return _currentScene;
    }

    // 게임 씬
    public void GameSceneStart(int _stage)
    {
        LoadScene(Define.Scene.GameScene);

        _currentStage = _stage;
    }
    int _currentStage = 1;
    public int CurrentStage { get { return _currentStage; } }

    public void SceneRestart()
    {
        LoadScene(_currentScene);
    }
    public void LoadScene(Define.Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
        Managers.Clear();
    }

    string GetSceneName(Define.Scene type)
    {
        return System.Enum.GetName(typeof(Define.Scene), type);
    }

    public void Clear()
    {
        Time.timeScale = 1f;
        _script.Clear();
    }
}