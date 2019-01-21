using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class m1SkillBullet : MonoBehaviour {

    private int count = 0;
    private float t1;

    // Use this for initialization
    void Start () {
        count = 0;
        Invoke("LoadSkill", 0.5f);
        Invoke("LoadSkill", 1.0f);
        Invoke("LoadSkill", 1.8f);
    }
	
	// Update is called once per frame
	void Update () {
        t1 += Time.deltaTime;
        if(t1>3.0f)
        {
            Destroy(this.gameObject);
        }
	}


    void LoadSkill()
    {
        GameObject gb=GameObject.Instantiate(Resources.Load<GameObject>("Bullet/m1Skill"));
        gb.transform.parent = this.transform;
        count += transform.tag.CompareTo("leftgb") == 0 ? -1 : 1;
        gb.transform.localPosition = (new Vector3(-1.85f * count, -0.65f, 0));
    }
}
