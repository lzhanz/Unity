using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Back : MonoBehaviour
{

    Button btn;

    private void Start()
    {
        btn = this.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }

 
    private void OnClick()
    {
        PlayerDeadState.isDead = false;
        SceneManager.LoadScene("StartMenu");
    }

}
