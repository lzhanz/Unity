using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class m1SkillCol : MonoBehaviour {

    RaycastHit2D hit;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        SkillRaycast();

    }


    void SkillRaycast()
    {
        hit = Physics2D.Raycast(this.transform.position, Vector2.up, 1.5f, 1 << LayerMask.NameToLayer("Player"));
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.name);
        }
    }
}
