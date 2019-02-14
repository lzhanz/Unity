using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HandleLoading : MonoBehaviour
{

    private Image myImage;
    private AsyncOperation operation;
    private float targetValue;
    private Text myText;
    public GameObject gb;
    public GameObject cir;
    private int num;


    private void Awake()
    {
        myImage = GameObject.Find("ProgressCircle").GetComponent<Image>();
        myText = GameObject.Find("jindu").transform.GetComponent<Text>();
        myImage.fillAmount = 0.0f;
      
    }


    // Use this for initialization
    void Start()
    {
        num = PlayerPrefs.GetInt("nextScene");
        if (SceneManager.GetActiveScene().name == "Loading")
        {
            //启动异步加载协程
            StartCoroutine(AsyncLoading());
        }
    }


    IEnumerator AsyncLoading()
    {
        if(num==1)
        {
            operation = SceneManager.LoadSceneAsync("Game");
        }
        else if(num == 2)
        {
            operation = SceneManager.LoadSceneAsync("Boss");
        }
        
        //阻止加载完成时立马切换
        operation.allowSceneActivation = false;

        yield return operation;
    }
    // Update is called once per frame
    void Update()
    {
        targetValue = operation.progress;

        if (operation.progress >= 0.9f)
        {
            targetValue = 1.0f;
        }
        if (targetValue != myImage.fillAmount)
        {
            myImage.fillAmount = Mathf.Lerp(myImage.fillAmount, targetValue, Time.deltaTime);

            if (Mathf.Abs(myImage.fillAmount - targetValue) < 0.01f)
            {
                myImage.fillAmount = targetValue;
            }
        }

        myText.text = ((int)(myImage.fillAmount * 100)).ToString() + "%";
        if ((int)(myImage.fillAmount * 100) == 100)
        {
            if (gb.activeInHierarchy == false)
            {
                gb.SetActive(true);
                cir.SetActive(false);
            }      //operation.allowSceneActivation = true;
        }
        if (gb.activeInHierarchy == true)
        {
            if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
            {
                operation.allowSceneActivation = true;
            }
        }
    }
}
