using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillState : PlayerBaseState
{
    private PlayerFacade _player;
    RaycastHit2D hit;
    private AnimatorStateInfo _stateInfo;
    int index;

    public PlayerSkillState(PlayerFacade _player)
    {
        this._player = _player;
        index =(int) _player.anim.GetFloat("skillnum");
       
    }

    public void Update()
    {
        _stateInfo = _player.anim.GetCurrentAnimatorStateInfo(0);
        if(_stateInfo.IsName("Skill") && _stateInfo.normalizedTime > 1.0f)
        {
            _player.anim.SetInteger("state", 0);
            _player.SetState(new PlayerIdleState(_player));
            return;
        }
        ChooseSkill(index);
    }



    public void HandleInput()
    {
        Update();
    }

    public void FixedUpdate()
    {

    }


    private void ChooseSkill(int dex)
    {
        switch(dex)
        {
            case 1:
                AttackRayCast();
                break;
        }
    }

    void AttackRayCast()
    {
        Vector2 pos = _player.trs.position;
        pos.x += 0.2f;
        pos.y += 0.5f;
        hit = Physics2D.Raycast(pos, _player.anim.GetFloat("Speed") == 1 ? Vector2.right : Vector2.left, 0.75f, (1 << 9) | 1 << 13);
        if (hit.collider != null)
        {
            if (hit.collider.tag.CompareTo("monster") == 0)
            {

                hit.collider.gameObject.GetComponent<MonsterControll>().HandleColor(2);
            }
            if (hit.collider.tag.CompareTo("boss") == 0)
            {
                hit.collider.gameObject.GetComponent<MonsterControll>().HandleBossColor(2);
            }
        }
    }
}
