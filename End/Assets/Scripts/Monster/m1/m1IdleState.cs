using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class m1IdleState : MonsterBaseState
{
    public static float waterCd=0;
    public static bool isCd = false;
    MonsterFacade _monster;
    private float ThinkTime;
    private float maxMove;

    public m1IdleState(MonsterFacade _monster)
    {
        this._monster = _monster;
        ThinkTime = 0;
        
    }


    public void Update()
    {
        if(isCd==true)
        {
            waterCd += Time.deltaTime;
            if (waterCd > 10.0f)
            {
                isCd = false;
            }
        }
      
        ThinkTime += Time.deltaTime;

        float playerdis = _monster.trs.position.x - _monster.playerTrs.position.x;
        float playerdisY = _monster.trs.position.y - _monster.playerTrs.position.y;

        if (ThinkTime > 1.1f)
        {
           
            if (Mathf.Abs(playerdis) <=0.5f && Mathf.Abs(playerdisY) < 3)
            {
                _monster.anim.SetFloat("Num", -1);
                _monster.anim.SetInteger("state", 2);
                _monster.SetState(new m1AttackState(_monster));
                return;
            }
            else if (Mathf.Abs(playerdis) <= 4 && Mathf.Abs(playerdisY) < 3)
            {
                float a = Random.Range(0f, 6f);
                if (a >= 4.0f&&isCd==false)
                {
                    isCd = true;
                    waterCd = 0;
 
                    _monster.anim.SetFloat("Num", 1);
                    _monster.anim.SetInteger("state", 2);
                    _monster.SetState(new m1AttackState(_monster));
                    return;
                }
                else
                {
                    _monster.anim.SetInteger("state", 1);
                    _monster.SetState(new m1RunState(_monster));
                    return;
                }
            }
            else
            {
                _monster.anim.SetInteger("state", 1);
                _monster.SetState(new m1RunState(_monster));
                return;
            }
        }
    }


    public void FixedUpdate()
    {

    }
}
