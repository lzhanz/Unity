using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLoadScene : MonoBehaviour
{

    public void LClick()
    {
        PlayerPrefs.SetInt("JumpTime", 12);
        PlayerPrefs.SetInt("nextScene", 1);

        SceneManager.LoadScene("Loading");
    }

    public void EClick()
    {
        Application.Quit();
    }
}
