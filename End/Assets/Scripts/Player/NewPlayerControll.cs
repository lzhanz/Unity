using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NewPlayerControll : MonoBehaviour {

    private PlayerFacade _player;
    private Animator anim;
    private Transform trs;
    private Rigidbody2D rig;
    private RaycastHit2D hit;
    private bool isLeave = true;
    Color _Color;
    float _time = 0;
    bool isLight = false;
    Material material1;
    Material material2;



    // Use this for initialization
    void Start () {
        anim= GetComponent<Animator>();
        trs = GetComponent<Transform>();
        material1 = transform.GetComponent<Renderer>().material;
        material2 = new Material(Resources.Load<Material>("Material/pl"));
        rig = GetComponent<Rigidbody2D>();
        _player = new PlayerFacade(anim, rig,trs);

	}
	
	// Update is called once per frame
	void Update () {
        /*Vector2 pos = transform.position;
        pos.x += 0.2f;
        pos.y += 0.5f;
        hit = Physics2D.Raycast(pos, anim.GetFloat("Speed")==1?Vector2.right:Vector2.left,0.7f,1 << LayerMask.NameToLayer("monster"));
        if (hit.collider!=null)
        {
                Debug.Log(hit.collider.name);
        }*/

        if(Input.GetKeyDown(KeyCode.U))
        {
            if(isLight==false)
            {
                transform.GetComponent<Renderer>().material = material2;
                material2.SetColor("_EdgeColor", new Color(0.33725f, 0.72941f, 0));
                _time = 0;
                isLight = true;
            }
            else
            {
                transform.GetComponent<Renderer>().material = material1;
            }
        }
        if (isLight==true)
        {
             _time += Time.deltaTime;
             _Color = material2.GetColor("_EdgeColor");
             _Color.b =(int) Mathf.Abs(255 * Mathf.Sin(0.7856f * _time));
            _Color.b /= 255;
            material2.SetColor("_EdgeColor", _Color);
        }
      
        _player.Update();

       

    }


    private void FixedUpdate()
    {
        _player.FixedUpdate();
    }




    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag.CompareTo("path") == 0)
        {
            _player.SetState(new PlayerIdleState(_player));
            anim.SetInteger("state", 0);
            anim.SetFloat("JumpDirection", 0);
            anim.SetInteger("JumpAt", 0);
            rig.velocity = new Vector2(0, 0);
            rig.gravityScale = 2.5f;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.CompareTo("Butt")==0)
        {
            GameObject gb = collision.gameObject.GetComponent<DoorGameObject>().gb;
            Vector3 pos = gb.transform.position;
            pos.z = -10;
            transform.GetComponent<PlayerCameraMove>().STOP = true;
            transform.GetComponent<PlayerCameraMove>().DoorPos = pos;
            transform.GetComponent<PlayerCameraMove>().GB = gb;
            collision.enabled = false;
            Time.timeScale = 0;
        }
        if (isLeave==true&&(collision.tag.CompareTo("lift") == 0||collision.tag.CompareTo("backlift")==0))
        {
            
            transform.GetComponent<PlayerCameraMove>().STOP = false;
            this.transform.parent = collision.transform.parent.transform;
            if (collision.tag.CompareTo("lift") == 0)
            {
                collision.gameObject.AddComponent<HandleLift>();
            }
            else
            {
                collision.gameObject.AddComponent<HandleBackLift>();
            }
            collision.enabled = false;
            isLeave =false;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if(transform.parent== null&&(collision.tag.CompareTo("lift") == 0 || collision.tag.CompareTo("backlift") == 0))
        {
            isLeave = true;
        }
    }

    private void SetJAttack(int num)
    {
        anim.SetInteger("JumpAt", num);
    }



}
