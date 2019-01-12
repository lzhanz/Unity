using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimbState : PlayerBaseState
{
    private PlayerFacade _player;
    private float horizontal;
    private float vertical;
    RaycastHit2D hit;

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
            _player.rig.velocity = new Vector2(0, 0);
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
        Vector2 pos = _player.trs.position;
        pos.x += 0.2f;
        pos.y += 0.5f;
        hit = Physics2D.Raycast(pos, Vector2.up, 0.5f, 1 << LayerMask.NameToLayer("stairup"));
        if (hit.collider != null)
        {
            _player.trs.parent = hit.collider.transform;
            _player.rig.velocity=new Vector2(0, 0);
            _player.SetState(new PlayerIdleState(_player));
            _player.anim.SetInteger("state", 0);
            _player.anim.speed = 1;
            _player.trs.localPosition = new Vector3(0,-0.06f,_player.trs.localPosition.z);
            _player.trs.parent = null;
        }
    }
   public void HandleInput()
    {
        Update();

    }
}
