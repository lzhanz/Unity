﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DogWalkState : MonsterBaseState
{
    MonsterFacade _monster;
    private bool needBack = false;
    Vector2 p1;
    Vector2 p2;
    private float maxMove;
    private float ThinkTime;

    public DogWalkState(MonsterFacade _monster)
    {
        this._monster = _monster;
        p2 = p1 = _monster.pos;
        p1.x -= _monster.trs.GetComponent<MonsterControll>().moveRange;
        p2.x += _monster.trs.GetComponent<MonsterControll>().moveRange;
        maxMove = _monster.trs.GetComponent<MonsterControll>().moveRange - 0.6f;
        ThinkTime = 0;
    }

    public void Update()
    {
        if (_monster.anim.GetInteger("state") == 3)
        {
            return;
        }
        ThinkTime += Time.deltaTime;
        float dis = _monster.pos.x - _monster.trs.position.x;
        float playerdis = _monster.trs.position.x - _monster.playerTrs.position.x;
        float playerdisY = _monster.trs.position.y - _monster.playerTrs.position.y;

        if (ThinkTime > 3.0f)
        {
            if (Mathf.Abs(playerdis) < 5 && Mathf.Abs(playerdisY) < 2)
            {
                if (playerdis < 0 && _monster.trs.rotation.y != 0)
                {
                    _monster.trs.Rotate(new Vector3(0, 180, 0));
                }
                else if (playerdis > 0 && _monster.trs.rotation.y == 0)
                {
                    _monster.trs.Rotate(new Vector3(0, -180, 0));
                }
                _monster.anim.SetInteger("state", 1);
                _monster.SetState(new DogAttackState(_monster));
                return;
            }
        }
        if (Mathf.Abs(dis) >= maxMove)
        {
            _monster.trs.Rotate(_monster.trs.rotation.y != 0 ? new Vector3(0, 180, 0) : new Vector3(0, -180, 0));
           
        }


        // {
        if (_monster.trs.rotation.y != 0)
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
