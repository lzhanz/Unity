using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class m1IdleState : MonsterBaseState
{

    MonsterFacade _monster;
    private float ThinkTime;

    public m1IdleState(MonsterFacade _monster)
    {
        this._monster = _monster;
        ThinkTime = 0;
    }


    public void Update()
    {
        ThinkTime += Time.deltaTime;

        float playerdis = _monster.trs.position.x - _monster.playerTrs.position.x;
        float playerdisY = _monster.trs.position.y - _monster.playerTrs.position.y;

        if (ThinkTime > 2.0f)
        {
            if (Mathf.Abs(playerdis) < 4 && Mathf.Abs(playerdisY) < 3)
            {
                if (playerdis < 0 && _monster.trs.rotation.y == 0)
                {
                    _monster.trs.Rotate(new Vector3(0, 180, 0));
                }
                else if (playerdis > 0 && _monster.trs.rotation.y != 0)
                {
                    _monster.trs.Rotate(new Vector3(0, -180, 0));
                }
                
            }
            _monster.anim.SetInteger("m1state", 1);
            _monster.SetState(new m1RunState(_monster));
        }
    }


    public void FixedUpdate()
    {

    }
}
