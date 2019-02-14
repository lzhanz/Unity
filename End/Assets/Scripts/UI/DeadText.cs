using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class DeadText : MonoBehaviour
{

    private Text deadText;


    private void Awake()
    {
        deadText = GetComponent<Text>();
    }
    // Use this for initialization
    void Start()
    {
        DOTween.To(() => deadText.fontSize, x => deadText.fontSize = x, 150, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
