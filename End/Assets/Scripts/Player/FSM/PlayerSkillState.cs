using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillState : PlayerBaseState
{
    private PlayerFacade _player;
    int index;

    public PlayerSkillState(PlayerFacade _player)
    {
        this._player = _player;
        index =(int) _player.anim.GetFloat("skillnum");
       
    }

    public void Update()
    {
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
                break;
    }
    }
}
