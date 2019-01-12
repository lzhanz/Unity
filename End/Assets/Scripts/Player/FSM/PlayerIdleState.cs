using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState {

    private PlayerFacade _player;
    private float horizontal;
    private float speedX;
    RaycastHit2D hit;
    bool isClimb = false;

    public PlayerIdleState(PlayerFacade facade)
    {
        _player = facade;
        isClimb = false;
    }

    public void HandleInput()
    {
        Update();
    }

    public void Update()
    {
        
       
        if (Input.GetKeyDown(KeyCode.C))
        {
            _player.SetState(new PlayerJumpState(_player));
            _player.anim.SetInteger("state", 1);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            _player.SetState(new PlayerAttackState(_player));
            _player.anim.SetInteger("state", 2);
           
        }

        
        if (isClimb==true&&Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (_player.trs.parent == null)
            {
                _player.trs.parent = hit.collider.transform;
                _player.trs.localPosition = new Vector3(-0.358f, _player.trs.localPosition.y, _player.trs.localPosition.z);
            }
            _player.SetState(new PlayerClimbState(_player));
            _player.anim.SetInteger("state", 3);
        }
        horizontal = Input.GetAxis("Horizontal");
        if (horizontal != 0)
        {
            _player.anim.SetFloat("Speed", horizontal > 0 ? 1 : -1);
        }
        speedX = horizontal * 3.0f;

        _player.anim.SetFloat("Direction", horizontal);
        _player.rig.velocity = new Vector2(speedX, _player.rig.velocity.y);    
    }

    public void FixedUpdate()
    {
        Vector2 pos = _player.trs.position;
        pos.x += 0.2f;
        pos.y += 0.5f;
        hit = Physics2D.Raycast(pos, Vector2.up, 2.5f, 1 << LayerMask.NameToLayer("stair"));
        if(hit.collider!=null)
        {
            isClimb = true;
        }

        else
        {
            isClimb = false;
        }
    }



   
}
