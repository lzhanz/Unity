using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MonsterControll : MonoBehaviour {

    private GameObject playerGb;
    private int HurtCount;
    public int hp;
    MonsterFacade _monster;
    private Animator anim;
    private Transform trs;
    private SpriteRenderer spR;
    public int index;
 
    public float moveRange;
    

    // Use this for initialization
    void Start () {
        anim = transform.GetComponent<Animator>();
        trs = transform.GetComponent<Transform>();
        spR = transform.GetComponent<SpriteRenderer>();
        playerGb = GameObject.Find("idle_0") as GameObject;
        _monster = new MonsterFacade(anim, trs,index,trs.position,playerGb.GetComponent<Transform>());
	}
	
	// Update is called once per frame
	void Update () {
        if(HurtCount>hp)
        {
          
            anim.speed = 0;
            anim.SetInteger("state", 3);
            transform.GetComponent<SpriteRenderer>().DOFade(0, 2.0f).OnComplete(
                () => { Destroy(this.gameObject); }) ;
        }
        _monster.Update();
        
	}

    private void FixedUpdate()
    {
        _monster.FixedUpdate();
    }


    void setBotState(int num)
    {
        anim.SetInteger("state", num);
    }


    public void HandleBossColor(int n)
    {
        HurtCount += n;
        transform.tag = "hurtBoss";
        spR.DOColor(new Color32(255, 150, 150, 255), 0.45f)
            .OnComplete(() =>
            {
                spR.DOColor(new Color32(255, 255, 255, 255), 0.68f)
    .OnComplete(() =>
    {
        transform.tag = "boss";
    });
            });

    }



    public void HandleColor(int n)
    {
        HurtCount+=n;
     
            anim.SetInteger("state", 3);
 
        anim.speed = 0;

        transform.GetComponent<BoxCollider2D>().enabled = false;
        spR.DOColor(new Color32(255, 150, 150, 255), 0.3f)
            .OnComplete(() => {
            spR.DOColor(new Color32(255, 255, 255, 255), 0.2f)
.OnComplete(() =>
{
    transform.GetComponent<BoxCollider2D>().enabled = true;
    anim.speed = 1;
    anim.SetInteger("state", 0);
    if(transform.parent.name.CompareTo("m1")==0)
    {
        _monster.SetState(new m1IdleState(_monster));
    }
    else if(transform.parent.name.CompareTo("Dog") == 0)
    {
        _monster.SetState(new DogWalkState(_monster));
    }
});
            });

    }
}
