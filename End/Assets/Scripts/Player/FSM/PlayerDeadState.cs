using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerDeadState : PlayerBaseState

{

    private PlayerFacade _player;
    public static bool isDead = false;
    private GameObject canvas = GameObject.Find("Canvas");

    public PlayerDeadState(PlayerFacade _player)
    {
        this._player = _player;
        
        if (isDead == false)
        {
            isDead = true;
            AudioManager.Instance.PlaySound(10);
            GameObject.Instantiate(Resources.Load<GameObject>("Bubble"), _player.trs.position, Quaternion.identity);
            Vector3 pos = canvas.transform.position;
            pos.y += 20;
            GameObject gb = GameObject.Instantiate(Resources.Load<GameObject>("DeadText"), pos, Quaternion.identity);
            gb.transform.parent = canvas.transform;
            pos.y -= 100;
            GameObject gt = GameObject.Instantiate(Resources.Load<GameObject>("Return"), pos, Quaternion.identity);
            gt.transform.parent = canvas.transform;
            DOTween.To(() => DeadPac.Instance.darkPercent, x => DeadPac.Instance.darkPercent = x, 0.7f, 4).OnComplete(() => { _player.trs.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0); });
        }
    }
    // Update is called once per frame
    public void Update () {
		
	}

    public void HandleInput()
    {
        if (_player.rig.velocity.x != 0 || _player.rig.velocity.y != 0)
        {
            _player.rig.velocity = new Vector2(0, 0);
        }

        if (_player.anim.GetInteger("state") != 6)
        {
            _player.anim.SetInteger("state", 6);
        }
    }

    public void FixedUpdate()
    {

    }
}
