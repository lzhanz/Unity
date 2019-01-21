using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFacade
{
    MonsterBaseState _state;
    public Transform playerTrs;
    public Animator anim;
    //public Rigidbody2D rig;
    public Transform trs;
    public Vector2 pos;

    public MonsterFacade(Animator at, Transform trs,int dex,Vector2 pos, Transform playerTrs)
    {
        this.trs = trs;
        anim = at;
        this.pos = pos;
        this.playerTrs = playerTrs;
        //this.rig = rig;
        ChooseMonster(dex);
       
    }


    public void SetState(MonsterBaseState _state)
    {
        this._state = _state;
    }

    public void Update()
    {
        _state.Update();
    }

    public void FixedUpdate()
    {
        _state.FixedUpdate();
    }

    private void ChooseMonster(int index)
    {
        switch(index)
        {
            case 0:
                _state = new BotWalkState(this);
                break;
            case 1:
                _state = new m1IdleState(this);
                break;

        }
    }
}
