using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleLaser : MonoBehaviour {

    ParticleSystem ps;
    GameObject gb;
    RaycastHit2D hit;
    bool isReEnd = true;
    bool isHurt = false;

    private void Awake()
    {
        ps = transform.GetComponent<ParticleSystem>();
        gb = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update () {
		if(!ps.isPlaying&&isReEnd==true)
        {
            Invoke("Restart", 2.0f);
            isReEnd = false;
        }
        else if(ps.isPlaying&&isReEnd==true)
        {
            hit = Physics2D.Raycast(gb.transform.position, Vector2.up, 3.0f, 1 << LayerMask.NameToLayer("Player"));
            if (hit.collider != null&& isHurt==false&& hit.collider.tag.CompareTo("Player") == 0)
            {
                NewPlayerControll tp = hit.collider.gameObject.GetComponent<NewPlayerControll>();
                tp.CURHP -= 25;
                tp.HanleHp(tp.CURHP, tp.MAXHP);
                if (tp.CURHP < 0)
                {
                    tp.HandleDead();
                }
                tp.HandleHpColor();
                isHurt = true;
                Invoke("ReHurt", 2.2f);
            }
        }
	}


    private void Restart()
    {
        ps.Play();
        isReEnd = true;
    }
    private void ReHurt()
    {
        isHurt = false;
    }
}
