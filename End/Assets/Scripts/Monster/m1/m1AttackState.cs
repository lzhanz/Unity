using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class m1AttackState :MonsterBaseState
{
    private MonsterFacade _monster;
    private AnimatorStateInfo _stateInfo;
    private RaycastHit2D hit;

    public m1AttackState(MonsterFacade _monster)
    {
        this._monster = _monster;
        float playerdis= _monster.trs.position.x - _monster.playerTrs.position.x;
 
            if (playerdis < 0 && _monster.trs.rotation.y == 0)
            {
                _monster.trs.Rotate(new Vector3(0, 180, 0));
            }
            else if (playerdis > 0 && _monster.trs.rotation.y != 0)
            {
                _monster.trs.Rotate(new Vector3(0, -180, 0));
            }
        if (_monster.anim.GetFloat("Num") == 1)
        {
            GameObject gb = GameObject.Instantiate(Resources.Load<GameObject>("Bullet/Empty"));
            gb.transform.parent = _monster.trs;
            gb.tag = _monster.trs.rotation.y == 0 ? "leftgb" : "rightgb";
            gb.transform.localPosition = (new Vector2(-0.1f, 0.25f));
            gb.transform.parent = null;
        }

    }

	public void Update()
    {
        _stateInfo = _monster.anim.GetCurrentAnimatorStateInfo(0);
        if (_stateInfo.IsName("m1Attack") && _stateInfo.normalizedTime >1.0f)
        {
            _monster.anim.SetInteger("state",0);
            _monster.SetState(new m1IdleState(_monster));

        }
    }


    public void FixedUpdate()
    {
        if(_monster.anim.GetFloat("Num")==-1)
        {
            Vector2 pos = _monster.trs.position;
            pos.x += _monster.trs.rotation.y == 0 ? -0.5f : 0.5f;
            pos.y += 1.0f;
            hit = Physics2D.Raycast(pos, _monster.trs.rotation.y == 0 ? Vector2.left : Vector2.right, 0.8f, 1 << LayerMask.NameToLayer("Player"));
            
            if(hit.collider!=null&& hit.collider.tag.CompareTo("Player") == 0)
            {
                NewPlayerControll tp = hit.collider.gameObject.GetComponent<NewPlayerControll>();
                tp.CURHP -= 15;
                tp.HanleHp(tp.CURHP, tp.MAXHP);
                if (tp.CURHP < 0)
                {
                    tp.HandleDead();
                }
                tp.HandleHpColor();
            }
        }
    }


}
