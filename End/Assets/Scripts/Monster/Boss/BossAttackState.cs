using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BossAttackState : MonsterBaseState
{
    private MonsterFacade _monster;
    private int model;
    private Rigidbody2D rig;
    private AnimatorStateInfo _stateInfo;
    private bool isEnd = false;
    private RaycastHit2D hit;

    public BossAttackState(MonsterFacade _monster, int model)
    {
        this._monster = _monster;
        this.model = model;
        rig = _monster.trs.GetComponent<Rigidbody2D>();
        float playerdis = _monster.trs.position.x - _monster.playerTrs.position.x;
        isEnd = false;
        if (playerdis < 0)
        {
            if (_monster.trs.rotation.y == 0)
            {
                _monster.trs.Rotate(new Vector3(0, 180, 0));
            }
        }
        else if (playerdis > 0)
        {
            if (_monster.trs.rotation.y != 0)
            {
                _monster.trs.Rotate(new Vector3(0, -180, 0));
            }
        }
    }


	public void Update()
    {
        _stateInfo = _monster.anim.GetCurrentAnimatorStateInfo(0);  
        if (_stateInfo.IsName("Attack") && _stateInfo.normalizedTime > 1.0f)
        {
            _monster.anim.SetInteger("state", 0);
            chooseThinkTime(model);
        }
        if (model == 1 && _stateInfo.IsName("Attack") && _stateInfo.normalizedTime > 0.3f)
        {
            BossRaycast(30,1.4f);
        }
        if (model == 2 && _stateInfo.IsName("Attack") && _stateInfo.normalizedTime > 0.45f&& _stateInfo.normalizedTime <0.85f)
        {
            BossRaycast(15,2.2f);
        }
        if (isEnd == false)
        {
            chooseSkill(model);
        }

    }

    public void FixedUpdate()
    {

    }


    private void chooseSkill(int index)
    {
        switch(index)
        {
            case 0:
                _monster.trs.DOMoveZ(0.000001f, 0.5f).OnComplete(LoadBossBulletX);
                    isEnd = true;
                break;
            case 1:
                _monster.trs.DOMoveZ(0.000001f, 0.3f).OnComplete(
                    () => { rig.velocity = new Vector2(_monster.trs.rotation.y == 0 ? -14 : 14, 0); });
                isEnd = true;
                break;
            case 2:
                isEnd = true;
                break;
            case 3:
                _monster.trs.DOMoveZ(0.000001f, 0.5f).OnComplete(LoadBossBulletY);
                isEnd = true;
                break;
        }
    }

    private void chooseThinkTime(int time)
    {
        float randomtime;
        switch (time)
        {
            case 0:
                randomtime = Random.Range(1.2f, 2.0f);
                _monster.SetState(new BossIdleState(_monster, randomtime));
                break;
            case 1:
                randomtime = Random.Range(1.4f, 2f);
                _monster.SetState(new BossIdleState(_monster, randomtime));
                break;
            case 2:
                _monster.SetState(new BossIdleState(_monster, 0.45f));
                break;
            case 3:
                randomtime = Random.Range(1.2f, 2.0f);
                _monster.SetState(new BossIdleState(_monster, randomtime));
                break;
        }
        
    }


    private void LoadBossBulletX()
    {
        GameObject gb = GameObject.Instantiate(Resources.Load<GameObject>("Bullet/BossBullet"));
        if (_monster.trs.rotation.y != 0)
        {
            gb.transform.Rotate(new Vector3(0, 180, 0));
        }
        gb.GetComponent<BossBullet>().isY = false;
        gb.transform.parent = _monster.trs;
        gb.transform.localPosition = (new Vector3(-0.62f, 0, 0));
        gb.transform.parent = null;
    }
    private void LoadBossBulletY()
    {
        GameObject gb = GameObject.Instantiate(Resources.Load<GameObject>("Bullet/BossBullet"));
        
        float a = Random.Range(0.0f, 2.0f);
        if(a<=1.0f)
        {
            gb.transform.Rotate(new Vector3(0, 0, 90));
        }
        else
        {
            gb.transform.Rotate(new Vector3(0, 0, -90));
        }
        gb.GetComponent<BossBullet>().isY = true;
        gb.transform.parent = _monster.playerTrs;
        gb.transform.localPosition = (gb.transform.rotation.z>0?new Vector3(0.15f, 2.95f, 0):new Vector3(0.15f, -2.95f, 0));

        gb.transform.parent = null;
    }


    private void BossRaycast(int hurnum,float distance)
    {
        
        Vector2 pos = _monster.trs.position;
        pos.x += _monster.trs.rotation.y == 0 ? 0.7f : -0.7f;
        pos.y -= 0.3f;
        hit = Physics2D.Raycast(pos, _monster.trs.rotation.y == 0 ? Vector2.left : Vector2.right, distance, 1 << 8);
        Debug.DrawRay(pos, _monster.trs.rotation.y == 0 ? Vector2.left : Vector2.right, Color.red);
        if (hit.collider!=null && hit.collider.tag.CompareTo("Player") == 0)
        {
            NewPlayerControll tp = hit.collider.gameObject.GetComponent<NewPlayerControll>();
            tp.CURHP -= hurnum;
            tp.HanleHp(tp.CURHP, tp.MAXHP);
            if (tp.CURHP < 0)
            {
                tp.HandleDead();
            }
            tp.HandleHpColor();
        }
    }
}
