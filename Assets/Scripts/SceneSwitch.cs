using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwifch : MonoBehaviour
{
    public void onClick(String scene) {
        SceneManager.LoadScene(scene); 
        if (scene == "Game") {
            SaveLoadManager.Instance.LoadData();
        } else {
            SaveLoadManager.Instance.SaveData();
        }
    }
}
