using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotAttackState : MonsterBaseState {

    MonsterFacade _monster;
    bool go = false;
    bool finishAdd = false;
    float t1 = 0;

    public BotAttackState(MonsterFacade _monster)
    {
        this._monster = _monster;
        t1 = 0;
        finishAdd = false;
        go = false;
    
    }

    // Update is called once per frame
   public  void Update () {
        t1 += Time.deltaTime;
        if(t1>0.70f&&finishAdd==false)
        {
            go = true;
        }
        if(go==true)
        {
            GameObject g1 = GameObject.Instantiate(Resources.Load<GameObject>("Bullet/BotBullet"));
            if (_monster.trs.rotation.y == 0)
            {
                g1.transform.Rotate(new Vector3(0, 180, 0));
            }
            g1.transform.parent = _monster.trs;
            g1.transform.localPosition = new Vector3(-0.33f, 0, 0);
            g1.transform.parent = null;
            go = false;
            finishAdd = true;
        }
        if (_monster.anim.GetInteger("botstate") == 3)
        {
            return;
        }
        if (_monster.anim.GetInteger("botstate") == 0)
        {
            _monster.SetState(new BotWalkState(_monster));
        }
    }
    public void FixedUpdate()
    {

    }


 
}
