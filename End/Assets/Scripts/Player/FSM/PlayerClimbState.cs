using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimbState : PlayerBaseState
{
    private PlayerFacade _player;
    private float horizontal;
    private float vertical;

    public PlayerClimbState(PlayerFacade _player)
    {
        this._player = _player;
        _player.rig.gravityScale = 0;
        
    }


    public void Update()
    {
        horizontal = Input.GetAxis("Horizontal");

        if (horizontal != 0)
        {
            _player.anim.SetFloat("Speed", horizontal > 0 ? 1 : -1);
        }

        if (horizontal!=0&&Input.GetKeyDown(KeyCode.C))
        {
            _player.anim.speed = 1;
            _player.SetState(new PlayerJumpState(_player));
            _player.anim.SetInteger("state", 1);
        }
        if (_player.trs.localPosition.x!=0.358f)
        {
            _player.trs.localPosition = new Vector3(-0.358f, _player.trs.localPosition.y, _player.trs.localPosition.z);
        }
        if (Input.GetKeyUp(KeyCode.UpArrow)&& !Input.GetKey(KeyCode.DownArrow))
        {
            _player.anim.speed = 0;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow))
        {
            _player.anim.speed = 0;
        }
        if (Input.GetKey(KeyCode.UpArrow)||Input.GetKey(KeyCode.DownArrow))
        {
            if (_player.anim.speed != 1)
            {
                _player.anim.speed = 1;
            }
        }
        vertical = Input.GetAxis("Vertical");
        if (vertical != 0)
        {
            _player.rig.velocity = new Vector2(0, vertical * 1.5f);
        }

    }

    public void FixedUpdate()
    {

    }
   public void HandleInput()
    {
        Update();

    }
}
