using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFacade
{
    PlayerBaseState _state;
    public Animator anim;
    public Rigidbody2D rig;
    public Transform trs;
    public InputQueue _inputQueue;

    public PlayerFacade(Animator at,Rigidbody2D rig,Transform trs)
    {
        this.trs = trs;
        this.rig = rig;
        _state = new PlayerIdleState(this);
        _inputQueue = new InputQueue();
        anim = at;
 
    }


    public void SetState(PlayerBaseState _state)
    {
        this._state = _state;
    }

    public void Update()
    {
        _state.HandleInput();
    }

    public void FixedUpdate()
    {
        _state.FixedUpdate();
    }
}
