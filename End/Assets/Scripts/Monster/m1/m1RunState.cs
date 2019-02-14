using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class m1RunState :MonsterBaseState
{
    MonsterFacade _monster;
    Vector2 p1;
    Vector2 p2;
    private float maxMove;
    public m1RunState(MonsterFacade _monster)
    {

        this._monster = _monster;
        p2 = p1 = _monster.pos;
        p1.x -= _monster.trs.GetComponent<MonsterControll>().moveRange;
        p2.x += _monster.trs.GetComponent<MonsterControll>().moveRange;
        maxMove = _monster.trs.GetComponent<MonsterControll>().moveRange - 0.2f;
    }

    public void Update()
    {
       
        if (m1IdleState.isCd == true)
        {
            m1IdleState.waterCd += Time.deltaTime;
            if (m1IdleState.waterCd > 35.0f)
            {
                m1IdleState.isCd = false;
            }
        }
        float playerdis = _monster.trs.position.x - _monster.playerTrs.position.x;
        float dis = _monster.pos.x - _monster.trs.position.x;
        float playerdisY = _monster.trs.position.y - _monster.playerTrs.position.y;

        if (Mathf.Abs(playerdis) <= 1.5f&&Mathf.Abs(playerdisY)<=1.0f)
        {
            _monster.anim.SetFloat("Num", -1);
            _monster.anim.SetInteger("state", 2);
            _monster.SetState(new m1AttackState(_monster));
            return;
        }
        else if (Mathf.Abs(playerdis) <= 4 && Mathf.Abs(playerdisY) < 1.5f)
        {
            float a = Random.Range(0f, 8f);
            if (a >= 6.0f&& m1IdleState.isCd==false)
            {
                m1IdleState.isCd = true;
                m1IdleState.waterCd = 0;
                
                _monster.anim.SetFloat("Num", 1);
                _monster.anim.SetInteger("state", 2);
                _monster.SetState(new m1AttackState(_monster));
                return;
            }
        }

       
        if (Mathf.Abs(dis) >= maxMove)
        {
            _monster.trs.Rotate(_monster.trs.rotation.y == 0 ? new Vector3(0, 180, 0) : new Vector3(0, -180, 0));
           
        }
        /* else if(Mathf.Abs(playerdis) <=4.5f&& Mathf.Abs(dis) <=maxMove-1)

         {
             /*if (playerdis < 0 && _monster.trs.rotation.y == 0)
             {
                 _monster.trs.Rotate(new Vector3(0, 180, 0));
             }
             else if (playerdis > 0 && _monster.trs.rotation.y != 0)
             {
                 _monster.trs.Rotate(new Vector3(0, -180, 0));
             }*/


        //}
        if (_monster.trs.rotation.y == 0)
        {
            _monster.trs.position = Vector2.MoveTowards(_monster.trs.position, p1, Time.deltaTime * 1.4f);
        }
        else
        {
            _monster.trs.position = Vector2.MoveTowards(_monster.trs.position, p2, Time.deltaTime * 1.4f);
        }



    }


    public void FixedUpdate()
    {

    }
}