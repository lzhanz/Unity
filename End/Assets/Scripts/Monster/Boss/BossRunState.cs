using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRunState : MonsterBaseState
{

    private MonsterFacade _monster;
    private AnimatorStateInfo _stateInfo;
    private Rigidbody2D rig;
    private RaycastHit2D hit;
    Vector2 p1;
    int model;


    public BossRunState(MonsterFacade _monster,int model)
    {
        this._monster=_monster;
        this.model = model;
        rig = _monster.trs.GetComponent<Rigidbody2D>();
        float playerdis = _monster.trs.position.x - _monster.playerTrs.position.x;
        if(playerdis<0)
        {
            if(_monster.trs.rotation.y==0)
            {
                _monster.trs.Rotate(new Vector3(0, 180, 0));
            }
        }
        else if(playerdis>0)
        {
            if (_monster.trs.rotation.y != 0)
            {
                _monster.trs.Rotate(new Vector3(0, -180, 0));
            }
        }
        if (model == 0)
        {
            rig.velocity = new Vector2(_monster.trs.rotation.y == 0 ? -7 : 7, 0);
        }
        else if(model==1)
        {
           p1 = _monster.trs.position;
           
            p1.x = playerdis > 0 ? _monster.playerTrs.position.x+1.0f : _monster.playerTrs.position.x -0.5f;
          
        }
    }

	// Update is called once per frame
	public void Update () {
       
        _stateInfo = _monster.anim.GetCurrentAnimatorStateInfo(0);
        if(_stateInfo.IsName("BossRun")&&_stateInfo.normalizedTime>1.0f)
        {
            _monster.anim.SetInteger("state", 0);
            _monster.SetState(new BossIdleState(_monster, 1.0f));
        }
        
        if(model==1)
        {
            _monster.trs.position = Vector2.MoveTowards(_monster.trs.position, p1, Time.deltaTime * 11.0f);
        }
    }


    public void FixedUpdate()
    {
        moveRaycast();
    }





    private void moveRaycast()
    {
       
        Vector2 pos = _monster.trs.position;
        hit = Physics2D.Raycast(pos, _monster.trs.rotation.y == 0 ? Vector2.left : Vector2.right, 1.5f, 1 << 5);
        if(hit.collider!=null)
        {
            rig.velocity = new Vector2(0, 0);
            _monster.trs.Rotate(_monster.trs.rotation.y == 0 ? new Vector3(0, 180, 0) : new Vector3(0, -180, 0));
            return;
        }
    }
}
