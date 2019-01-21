using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{

    private PlayerFacade _player;
    private float horizontal;
    private float speedX;
    RaycastHit2D hit;
    RaycastHit2D hitDown;
    private int index;
   
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
       
        if (HandleKey(_player._inputQueue.GetIndex()) !=-1)
        {
            return;
        }
      

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


        if (isClimb == true && Input.GetKeyDown(KeyCode.UpArrow) && hit.collider != null)
        {
            if (_player.trs.parent == null)
            {
                _player.trs.parent = hit.collider.transform;
                _player.trs.localPosition = new Vector3(-0.358f, _player.trs.localPosition.y, _player.trs.localPosition.z);
            }
            _player.SetState(new PlayerClimbState(_player));
            _player.anim.SetInteger("state", 3);
        }
        if (isClimb == true && Input.GetKeyDown(KeyCode.DownArrow) && hitDown.collider != null)
        {
            if (_player.trs.parent == null)
            {
                _player.trs.parent = hitDown.collider.transform;
                _player.rig.velocity = new Vector2(0, 0);
                _player.trs.localPosition = new Vector3(-0.358f, 2.62f, _player.trs.localPosition.z);
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
        hitDown = Physics2D.Raycast(pos, Vector2.down, 5.0f, 1 << LayerMask.NameToLayer("stair"));
        if (hit.collider != null || hitDown.collider != null)
        {
            isClimb = true;
        }

        else
        {
            isClimb = false;
        }
    }


    private int HandleKey(int index)
    {
        switch (index)
        {
            
            case 0:
                if (NewPlayerControll.isLight==false)
                {
                    NewPlayerControll.isLight = true;
                    _player.anim.SetFloat("skillnum", 0);
                }
                return index;
            case 1:
            case 2:
                _player.anim.SetFloat("skillnum", 1);
                _player.anim.SetInteger("state", 5);
                _player.SetState(new PlayerSkillState(_player));
                return index;
            default:
                return -1;
        }
    }


  
}
