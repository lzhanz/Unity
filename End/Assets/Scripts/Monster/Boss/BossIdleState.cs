using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleState : MonsterBaseState
{

    private MonsterFacade _monster;
    private float ThinkTime;
    private float nowTime;
    private int index;
    private RaycastHit2D[] hit=new RaycastHit2D[2];
    float playerdis;
    bool isInWall = false;

    public BossIdleState(MonsterFacade _monster, float time)
    {
        this._monster = _monster;
        nowTime = 0;
        isInWall = false;
        this.ThinkTime = time;
    }


    public void Update()
    {
        playerdis = _monster.trs.position.x - _monster.playerTrs.position.x;
        nowTime += Time.deltaTime;
        if (nowTime > ThinkTime)
        {
            float a = Random.Range(0.0f, 12.0f);
            if(a<=0.8f)
            {
                nowTime = 0;
                ThinkTime = 1.0f;
                return;
            }
            else 
            {
                if (isInWall==true)
                {
                    float k = Random.Range(0.0f, 4.0f);
                    if (k <= 3.5f)
                    {
                        index = 6;
                   }
                }
                else
                {
                    if (Mathf.Abs(playerdis) <= 3.5f)
                    {
                        float b = Random.Range(0.0f, 7.0f);
                        if (b < 0.8f)
                        {
                            index = 0;
                        }
                        else if (b < 1.8f)
                        {
                            index = 1;
                        }
                        else if (b < 2.8f)
                        {
                            index = 4;
                        }
                        else if (b < 5.0f)
                        {
                            index = 5;
                        }
                    }
                    else
                    {
                        float c = Random.Range(0.0f, 11.0f);
                        if (c <= 3.5f)
                        {
                            index = 2;
                        }
                        else if (c <= 7.0f)
                        {
                            index = 3;
                        }
                        else
                        {
                            index = 4;
                        }
                    }
                }
            }



            Choose(index);
        }
    }


    public void FixedUpdate()
    {
        isPlayerInWall();
    }



    private void Choose(int dex)
    {
        switch(dex)
        {
            case 0:
                nowTime = 0;
                break;
            case 1:
                _monster.anim.SetInteger("state", 1);
                _monster.SetState(new BossRunState(_monster,0));
                break;
            case 2:
                _monster.anim.SetInteger("state", 1);
                _monster.SetState(new BossRunState(_monster,1));
                break;
            case 3:
                _monster.anim.SetInteger("state", 2);
                _monster.anim.SetFloat("Num", 1);
                _monster.SetState(new BossAttackState(_monster,0));
                break;
            case 4:
                _monster.anim.SetInteger("state", 2);
                _monster.anim.SetFloat("Num", 2);
                _monster.SetState(new BossAttackState(_monster, 1));
                break;
            case 5:
                _monster.anim.SetInteger("state", 2);
                _monster.anim.SetFloat("Num", 3);
                _monster.SetState(new BossAttackState(_monster, 2));
                break;
            case 6:
                _monster.anim.SetInteger("state", 2);
                _monster.anim.SetFloat("Num", 1);
                _monster.SetState(new BossAttackState(_monster, 3));
                break;
        }
    }


    private void isPlayerInWall()
    {
       if(_monster.playerTrs.position.y>=-11.50f)
        {
            isInWall = true;
        }
       else
        {
            isInWall = false;
        }
    }
}
