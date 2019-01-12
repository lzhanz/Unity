﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState {

    private PlayerFacade _player;
    private AnimatorStateInfo _stateInfo;


    public PlayerAttackState(PlayerFacade _player)
    {
        this._player = _player;
        _player.anim.SetInteger("Attack", 1);
    }

    public void Update()
    {
        
        _stateInfo = _player.anim.GetCurrentAnimatorStateInfo(0);
        if(_stateInfo.IsName("Jump")&&_player.anim.GetInteger("state")==2)
        {
            _player.SetState(new PlayerIdleState(_player));
            _player.anim.SetInteger("state", 0);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            HandleAttack(_stateInfo);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            _player.SetState(new PlayerJumpState(_player));
            _player.anim.SetInteger("state", 1);
        }
        if (_stateInfo.IsName("Attack1")|| _stateInfo.IsName("Attack2")|| _stateInfo.IsName("Attack3")) 
        {
            if (_stateInfo.normalizedTime > 1.0f)
            {
                _player.anim.SetInteger("state", 0);
                _player.SetState(new PlayerIdleState(_player));
                return;
            }
        }
       

    }
    public void HandleInput()
    {
        Update();
    }


    public void FixedUpdate()
    { }

    private void HandleAttack(AnimatorStateInfo stateInfo)
    {
        if (_stateInfo.IsName("Attack1")&& _stateInfo.normalizedTime<0.8f)
        {
            _player.anim.SetInteger("Attack", 2);

        }
       else if (_stateInfo.IsName("Attack2")&& _stateInfo.normalizedTime < 0.8f)
        {
            _player.anim.SetInteger("Attack", 3);
        }
    }
}