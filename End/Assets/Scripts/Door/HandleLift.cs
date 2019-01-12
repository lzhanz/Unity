using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HandleLift : MonoBehaviour {

	// Use this for initialization
	void Start () {

        transform.parent.gameObject.transform.DOMoveY(
            transform.parent.gameObject.transform.position.y - 10, 6.0f).OnComplete(() => {
                transform.tag = "backlift";
                GameObject.Find("idle_0").transform.parent = null;
                transform.GetComponent<Collider2D>().enabled = true;
                Destroy(this);
            });
    }
	

}
