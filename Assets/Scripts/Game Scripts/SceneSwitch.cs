using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    public void onClick(string newScene)
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName == "Game")
        {
            SaveManager.Instance.SaveCurrentSceneObjects();
        }
        SceneManager.LoadScene(newScene);
    }
}
