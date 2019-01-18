using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToNextStage : MonoBehaviour {

    public static int boxNum = 0;
    public GameObject gb;
	
	// Update is called once per frame
	void Update () {
		if(boxNum>=4)
        {
            gb.AddComponent<HandleMoveDoor>();
            Destroy(this);
        }

	}
}
