using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TestWin : MonoBehaviour {

    GameObject boss;
    GameObject player;
    bool isFinish;
    private GameObject canvas;
    private void Awake()
    {
        canvas = GameObject.Find("Canvas");
}
    // Use this for initialization
    void Start () {
        boss = GameObject.Find("boss_10");
        player = GameObject.Find("idle_0");

        isFinish = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(boss==null&&isFinish==false)
        {
            isFinish = true;
            Vector3 pos = canvas.transform.position;
            pos.y += 20;
            GameObject gb = GameObject.Instantiate(Resources.Load<GameObject>("WinText"), pos, Quaternion.identity);
            gb.transform.parent = canvas.transform;
            pos.y -= 100;
            GameObject gt = GameObject.Instantiate(Resources.Load<GameObject>("Return"), pos, Quaternion.identity);
            gt.transform.parent = canvas.transform;
            DOTween.To(() => DeadPac.Instance.darkPercent, x => DeadPac.Instance.darkPercent = x, 0.7f, 4).OnComplete(() => { player.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0); });
           
        }
    }
   
}
