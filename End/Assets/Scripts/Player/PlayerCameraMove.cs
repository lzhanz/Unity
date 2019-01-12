using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerCameraMove : MonoBehaviour {


    private bool isStop = false;
    private bool finish = false;
    private Vector3 doorPos=Vector3.zero;
    private Vector3 mTragetPos = Vector3.zero;
    private float moveTime;
    private GameObject gb;


    void LateUpdate()
    {
        LimitMove();
        JudgeCameraMove();
        moveTime =isStop == false ? 0.1f : 0.024f;
        mTragetPos = isStop==false?GetCameraMovePos():doorPos;
        if (mTragetPos != Camera.main.transform.position)
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, mTragetPos, moveTime);
        }
    }

    public GameObject GB
    {
        set { gb = value; }
    }
    public Vector3 DoorPos
    {
        set { doorPos = value; }
    }
    public bool STOP
    {
        set { isStop = value; }
    }
    public bool FINISH
    {
        set { finish = value; }
    }

    //地图左下角点
    public Transform LeftDown;

    //地图右上角的点
    public Transform RightUp;

    Vector3 GetCameraMovePos()
    {
        Vector3 pos = this.transform.position;
        float screenX = SceneToWorldSize(Screen.width * 0.5f, Camera.main,
                                                pos.z);

        //pos.y = Camera.main.transform.position.y;
        pos.z = Camera.main.transform.position.z;

        float maxX = RightUp.position.x;
        float minX = LeftDown.position.x;

        if (pos.x - screenX < minX)
        {
            pos.x = minX + screenX;
        }
        else if (pos.x + screenX > maxX)
        {
            pos.x = maxX - screenX;
        }

        return pos;
    }

    /// <summary>
    /// 像素单位转世界单位
    /// </summary>
    /// <param name="size"></param>
    /// <param name="ca"></param>
    /// <returns></returns>
    public float SceneToWorldSize(float size, Camera ca, float Worldz)
    {
        if (ca.orthographic)
        {
            float height = Screen.height / 2;
            float px = (ca.orthographicSize / height);
            return px * size;
        }
        else
        {
            float halfFOV = (ca.fieldOfView * 0.5f);//摄像机夹角 的一半//
            halfFOV *= Mathf.Deg2Rad;//弧度转角度//

            float height = Screen.height / 2;
            float px = height / Mathf.Tan(halfFOV);//得到应该在的Z轴//
            Worldz = Worldz - ca.transform.position.z;
            return (Worldz / px) * size;
        }
    }


    private void LimitMove()
    {
        Vector3 pos = this.gameObject.transform.position;
       

        float maxX = RightUp.position.x;
        float minX = LeftDown.position.x;



        if (pos.x < minX)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x + 0.1f, gameObject.transform.position.y, gameObject.transform.position.z);
        }
        if (pos.x > maxX-0.75f)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x - 0.1f, gameObject.transform.position.y, gameObject.transform.position.z);
        }
    }


    private void JudgeCameraMove()
    {
  
        if (isStop == true&&Camera.main.transform.position.x>= doorPos.x-0.2f)
        {
            if (finish == false)
            {
                gb.transform.parent.gameObject.AddComponent<HandleDoor>();
                finish = true;
            }
        }
    }
}

