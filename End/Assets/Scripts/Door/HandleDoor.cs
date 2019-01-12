using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HandleDoor : MonoBehaviour {

    Tweener t;

    private void Start()
    {
        
        transform.tag = "path";
        t = transform.DORotate(new Vector3(0, 0, -90), 6.0f);
        t.SetUpdate(true); ;
    }



    private void LateUpdate()
    {
        
        if (transform.eulerAngles.z<=270)
        {       
            GameObject.Find("idle_0").GetComponent<PlayerCameraMove>().STOP = false;
            GameObject gb=GameObject.Instantiate(Resources.Load<GameObject>("laser-a-3"), GameObject.Find("laser").transform);
            GameObject gb1 = GameObject.Instantiate(Resources.Load<GameObject>("laser-b-1"), GameObject.Find("laser1").transform);
            gb.transform.localPosition = new Vector3(0.05f, -0.09f, 1);
            gb1.transform.localPosition = new Vector3(0, 0.01f, 1);
            Time.timeScale = 1;
            Destroy(this);
        }
    }




}
