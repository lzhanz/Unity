using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BossBullet : MonoBehaviour {


    private Tweener t1;
    float endX;
    float endY;
    public bool isY;

    // Use this for initialization
    void Start () {
        if (isY == false)
        {
            endX = transform.rotation.y == 0 ? -8.0f : 8.0f;

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
        else
        {
            endY = transform.rotation.z > 0 ? -18.0f :18.0f;

            t1 = transform.DOMoveY(transform.position.y + endY, 1.8f).OnComplete(
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

    }

    // Update is called once per frame
    void Update () {
		
	}


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.CompareTo("Player") == 0)
        {
            NewPlayerControll tp = collision.gameObject.GetComponent<NewPlayerControll>();
            tp.CURHP -= 36;
            tp.HanleHp(tp.CURHP, tp.MAXHP);
            if(tp.CURHP<0)
            {
                tp.HandleDead();
            }
            tp.HandleHpColor();
        }
    }
}
