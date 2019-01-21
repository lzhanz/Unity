using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class m1RunState :MonsterBaseState
{
    MonsterFacade _monster;
    Vector2 p1;
    Vector2 p2;
    public m1RunState(MonsterFacade _monster)
    {

        this._monster = _monster;
        p2 = p1 = _monster.pos;
        p1.x -= _monster.trs.GetComponent<MonsterControll>().moveRange;
        p2.x += _monster.trs.GetComponent<MonsterControll>().moveRange;
    }

    public void Update()
    {
        float playerdis = _monster.trs.position.x - _monster.playerTrs.position.x;

        if (playerdis < 0 && _monster.trs.rotation.y == 0)
        {
            _monster.trs.Rotate(new Vector3(0, 180, 0));
        }
        else if (playerdis > 0 && _monster.trs.rotation.y != 0)
        {
            _monster.trs.Rotate(new Vector3(0, -180, 0));
        }
        if (_monster.trs.rotation.y == 0)
        {
            _monster.trs.position = Vector2.MoveTowards(_monster.trs.position, p1, Time.deltaTime);
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