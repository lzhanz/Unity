using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BotBullet : MonoBehaviour {

    Tweener t1;
    float endX;
	// Use this for initialization
	void Start () {
        endX = transform.rotation.y == 0 ? 5.0f : -5.0f;

        t1 = transform.DOMoveX(transform.position.x + endX, 1.2f).OnComplete(
            () =>
            {
                transform.GetComponent<SpriteRenderer>().DOFade(0, 0.1f).OnComplete
                (() =>
                {
                    Destroy(this.gameObject);
                });
            }
            );
            
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.CompareTo("Player")==0)
        {
            NewPlayerControll tp = collision.gameObject.GetComponent<NewPlayerControll>();
            tp.CURHP -= 20;
            tp.HanleHp(tp.CURHP, tp.MAXHP);
            if (tp.CURHP < 0)
            {
                tp.HandleDead();
            }
            tp.HandleHpColor();
        }
    }
}
