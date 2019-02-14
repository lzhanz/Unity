using UnityEngine;
using System.Collections;

public class fllowplayer : MonoBehaviour {

    private static fllowplayer _instance;
    public static fllowplayer Instance
    {
        get
        {
            return _instance;
        }
    }

    public Material mat;
    public Material Deadmat;
    public Transform pp;
    float Radius=1.0f;
    bool turning=false;
    bool up = false;
    bool down = false;
    bool dead = false;
    float Timer=3f;//
    //public float a;
	// Use this for initialization
    void Awake()
    {
        _instance = this;
    }

	void Start () {
        GetComponent<Renderer>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (!turning)
            return;
        if (down)
            DownRadius();

        else if (!up && Timer > 0)
            Timer -= Time.deltaTime;
        else
            UpRadius();
        Vector3 temp;
        if (pp.rotation.y != 0)
        {
            float x1 =pp.transform.position.x- 1.0f;
             temp = Camera.main.WorldToScreenPoint(new Vector3(x1, pp.position.y, pp.position.z));
        }
        else
        {
             temp = Camera.main.WorldToScreenPoint(pp.position);
        }
        
        //temp.y -= 70;
        mat.SetVector("_Center", dealwith(temp));

	}
    public void init()
    {
        Timer = 3f;//
        down = true;
        if (turning)
            return;
            //return;
            //return;
        turning = true;
        //down = true;
        Radius = 0.5f;
        GetComponent<Renderer>().enabled = true;
        //Timer = 5f;
    }


    Vector3 dealwith(Vector3 a)
    {
        // a = a / 100;
        //a = a / 30;// +new Vector3(0.33f,0.295f);
        //Debug.Log(a);
        return new Vector3(a.x / Screen.width, a.y/ Screen.height);
    }
    void UpRadius()
    {
        if(dead==true)
        {
            return;
        }
        if (Radius < 1.0f)
            Radius += 0.5f * Time.deltaTime;
        else
        {
            Radius = 1.0f;
            up=false;
            turning = false;
            GetComponent<Renderer>().enabled = false;
        }
            //Radius=0.8f;
        mat.SetFloat("_Radius",Radius);
    }
    void DownRadius()
    {
        if (Radius > 0.12f)
            Radius -= 0.5f * Time.deltaTime;
        else
        {
            Radius = 0.12f;
            down = false;
        }
            //Radius = 0.1f;
        mat.SetFloat("_Radius", Radius);
    }
}
