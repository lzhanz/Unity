using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState {

    PlayerFacade _player;
    private float speedY;
    private RaycastHit2D[] hit=new RaycastHit2D[4];
    private float speedX;
    private float horizontal;
    private int numjump;
    private int cTime;
    private bool isJump;
    private float gravity;
    private bool firstEnter;



    public PlayerJumpState(PlayerFacade _player)
    {
        this._player = _player;
        numjump = 1;
        cTime = 0;
        gravity = 0.54f;
        isJump = true;
        speedY = 1.0f;
        _player.rig.velocity = new Vector2(speedX, speedY);

    }

    public void HandleInput()
    {
        Update();
    }

    public void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.X) && isJump == true )
        {
            AttackRayCast();
            if (_player.anim.GetInteger("JumpAt") == 2)
            {
                _player.anim.SetInteger("JumpAt", 3);
            }
            else
            {
                _player.anim.SetInteger("JumpAt", 1);
            }
        }

        horizontal = Input.GetAxis("Horizontal");
        _player.rig.gravityScale = 0;

        if (horizontal != 0)
        {
            _player.anim.SetFloat("Speed", horizontal > 0 ? 1 : -1);
        }
        speedX = horizontal * 3.0f;

        _player.anim.SetFloat("Direction", horizontal);

    

        if (Input.GetKeyUp(KeyCode.C) && numjump != 3)
        {
            ++numjump;
            cTime = 0;
        }

        if (Input.GetKeyDown(KeyCode.C) && numjump != 3)
        {
            if (numjump == 2)
            {
                _player.anim.SetInteger("JumpAt", 2);
            }
            speedY = 1.0f;
            _player.rig.velocity = new Vector2(speedX, speedY);
        }

        if (Input.GetKey(KeyCode.C) && isJump == true && numjump != 3)
        {
            if (cTime < 12)
            {
                cTime += 1;
                speedY += 1.0f;
            }
            speedY = Mathf.Min(speedY, 10);
        }
    }

    public void FixedUpdate()
    {
       

        if (isJump == true)
        {
         
            speedY -= gravity;
            speedY = Mathf.Max(speedY, -10);
            _player.rig.velocity = new Vector2(speedX, speedY);
        }
        if (_player.rig.velocity.y >= 0 && _player.rig.gravityScale == 0)
        {
            _player.anim.SetFloat("JumpDirection", 0);
        }
        else if (_player.rig.velocity.y < 0 && _player.rig.gravityScale == 0)
        {
            _player.anim.SetFloat("JumpDirection", 1);
        }
    }

    void AttackRayCast()
    {
        Vector2 pos = _player.trs.position;
        pos.x += 0.2f;
        pos.y += 0.5f;
        hit[0] = Physics2D.Raycast(pos,Vector2.right, 1.2f, 1 << LayerMask.NameToLayer("monster"));
        hit[1] = Physics2D.Raycast(pos, Vector2.left, 1.2f, 1 << LayerMask.NameToLayer("monster"));
        hit[2] = Physics2D.Raycast(pos, Vector2.up, 1.2f, 1 << LayerMask.NameToLayer("monster"));
        hit[3] = Physics2D.Raycast(pos, Vector2.down, 1.2f, 1 << LayerMask.NameToLayer("monster"));

        foreach(RaycastHit2D h in hit)
        {
            if (h.collider != null)
            {
                if (h.collider.tag.CompareTo("monster") == 0)
                {
                    h.collider.gameObject.GetComponent<MonsterControll>().HandleColor(1);
                }
            }
        }
        
    }


}
