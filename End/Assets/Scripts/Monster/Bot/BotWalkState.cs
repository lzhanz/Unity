using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotWalkState : MonsterBaseState
{
    MonsterFacade _monster;
    bool needBack = false;
    Vector2 p1;
    Vector2 p2;
    private float maxMove;
    private float ThinkTime;

    
    public BotWalkState(MonsterFacade _monster)
    {
        this._monster = _monster;
        p2=p1 = _monster.pos;
        p1.x -= _monster.trs.GetComponent<MonsterControll>().moveRange;
        p2.x += _monster.trs.GetComponent<MonsterControll>().moveRange;
        maxMove = _monster.trs.GetComponent<MonsterControll>().moveRange - 0.2f;
        ThinkTime = 0;
    }

    
    public void Update()
    {
        if(_monster.anim.GetInteger("botstate")==3)
        {
            return;
        }
        ThinkTime += Time.deltaTime;
        float dis = _monster.pos.x - _monster.trs.position.x;
        float playerdis = _monster.trs.position.x - _monster.playerTrs.position.x;
        float playerdisY= _monster.trs.position.y - _monster.playerTrs.position.y;

        if (ThinkTime > 2.8f)
        {
            if (Mathf.Abs(playerdis) < 4&&Mathf.Abs(playerdisY)<3)
            {
                if (playerdis < 0 && _monster.trs.rotation.y == 0)
                {
                    _monster.trs.Rotate(new Vector3(0, 180, 0));
                }
                else if (playerdis > 0 && _monster.trs.rotation.y != 0)
                {
                    _monster.trs.Rotate(new Vector3(0, -180, 0));
                }
                _monster.anim.SetInteger("botstate", 1);
                _monster.SetState(new BotAttackState(_monster));
            }
        }
        /*if (Mathf.Abs(dis)>5)
        {
            needBack = true;
        }
        if(needBack==true)
        {
            if(Mathf.Abs(dis)<=0.5)
            {
                needBack = false;
            }
            else
            {
                _monster.trs.Translate(Vector2.right * Time.deltaTime);
            }
        }*/
        if (Mathf.Abs(dis) >=maxMove)
        {
            _monster.trs.Rotate(_monster.trs.rotation.y==0?new Vector3(0, 180, 0): new Vector3(0, -180, 0));
        }

        
        // {
        if (_monster.trs.rotation.y == 0)
            {
            _monster.trs.position = Vector2.MoveTowards(_monster.trs.position, p1,Time.deltaTime);
            }
           else
            {
            _monster.trs.position = Vector2.MoveTowards(_monster.trs.position, p2, Time.deltaTime);
        }        

    }


    public void FixedUpdate()
    {

    }
}
