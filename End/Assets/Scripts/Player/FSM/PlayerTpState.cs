using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTpState : PlayerBaseState
{
    private PlayerFacade _player;
    public PlayerTpState(PlayerFacade _player)

    {
        _player.rig.velocity = new Vector2(0, 0);
        this._player = _player;
    }

    public void Update()
    {

    }
    public void HandleInput()
    {
    }

    public void FixedUpdate()
    {

    }
}
