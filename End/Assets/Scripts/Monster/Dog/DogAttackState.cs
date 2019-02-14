using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DogAttackState : MonsterBaseState

{
    private bool isFinish = false;
    private MonsterFacade _monster;
    private AnimatorStateInfo _stateInfo;

    public DogAttackState(MonsterFacade _monster)
    {
        this._monster = _monster;
        isFinish = false;
    }

    public void Update()
    {
      
        _stateInfo = _monster.anim.GetCurrentAnimatorStateInfo(0);
        if (_stateInfo.IsName("Attack") && _stateInfo.normalizedTime > 1.0f)
        {
            _monster.anim.SetInteger("state", 0);
            _monster.SetState(new DogWalkState(_monster));
            return;
        }
        if (isFinish == false)
        {
            float a = Random.Range(0.0f, 12.0f);
            if (a >= 8.0f)
            {

                _monster.trs.DOMoveZ(0.000001f,0.25f).OnComplete(LoadBullet);
                _monster.trs.DOMoveZ(0.00001f, 1.6f).OnComplete(LoadBullet);
                isFinish = true;
            }
            else
            {
                _monster.trs.DOMoveZ(0.000001f, 0.25f).OnComplete(LoadBullet);
                isFinish = true;
            }
        }
        


        if (_monster.anim.GetInteger("state") == 3)
        {
            return;
        }

    }
        public void FixedUpdate()
    {

    }


    private void LoadBullet()
    {
        GameObject g1 = GameObject.Instantiate(Resources.Load<GameObject>("Bullet/DogBullet"));
        if (_monster.trs.rotation.y != 0)
        {
            g1.transform.Rotate(new Vector3(0, 180, 0));
        }
        g1.transform.parent = _monster.trs;
        g1.transform.localPosition = new Vector3(0.8f, 0.01f, 0);
        g1.transform.parent = null;
    }

  

}
