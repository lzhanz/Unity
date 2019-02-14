using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;


public class NewPlayerControll : MonoBehaviour {

    private PlayerFacade _player;
    private Animator anim;
    private Transform trs;
    private Rigidbody2D rig;
    private RaycastHit2D hit;

    private Image head;

    private SpriteRenderer spR;
    Color _Color;
    float _time = 0;
    private Material material1;
    Material material2;
    private bool isLeave = true;
    public static bool isLight = false;
    private static int cur_HP = 400;
    private static int max_HP = 400;


    public int CURHP
    {
        get { return cur_HP; }
        set { cur_HP = value; }
    }

    public int MAXHP
    {
        get { return max_HP; }
    }


    private void Awake()
    {
        material1 = transform.GetComponent<Renderer>().material;
        CURHP = 400;
        material2 = new Material(Resources.Load<Material>("Material/pl"));
    }

    // Use this for initialization
    void Start () {
        anim= GetComponent<Animator>();
        trs = GetComponent<Transform>();
        rig = GetComponent<Rigidbody2D>();
        head = GameObject.Find("Head").GetComponent<Image>();
        spR = GetComponent<SpriteRenderer>();
        
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

        /*  */
     

        _player.Update();
    }


    private void FixedUpdate()
    {
       
        _player.FixedUpdate();
        changeColor();
    }




    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.collider.tag.CompareTo("path") == 0)
        {
            if(anim.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
            {
                AudioManager.Instance.PlaySound(6);
            }
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
        /*if(collision.tag.CompareTo("monster")==0)
        {
            collision.gameObject.GetComponent<MonsterControll>().HandleColor();
        }*/
        if (collision.tag.CompareTo("trap") == 0)
        {
            
            if (CURHP > 0)
            {
                
                CURHP -= 400;
                HanleHp(CURHP, MAXHP);
            }
            
            
            HandleDead();
        }
        if(collision.tag.CompareTo("Tonext") == 0)
         {
            /*_player.trs.parent = collision.transform;
            _player.trs.localPosition = new Vector3(-0.38f, -0.76f, -1.5f);
            _player.trs.parent = null;
            _player.trs.position = new Vector3(_player.trs.position.x, _player.trs.position.y, -1.5f);*/
            anim.SetInteger("state", 7);
            _player.SetState(new PlayerTpState(_player));
        }
        if (collision.tag.CompareTo("Butt") == 0)
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
        if (isLeave == true && (collision.tag.CompareTo("lift") == 0 || collision.tag.CompareTo("backlift") == 0))
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
            isLeave = false;
        }
        if (collision.tag.CompareTo("winCount") == 0)
        {
            collision.enabled = false;
            Destroy(collision.gameObject);
            ++ToNextStage.boxNum;
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

     private void SetStateNum(int num)
    {
        anim.SetInteger("state", num);
        _player.SetState(new PlayerIdleState(_player));
    }

    private void changeColor()
    {
        if (anim.GetFloat("skillnum") == 0&&isLight == true)
            {
                transform.GetComponent<Renderer>().material = material2;
                material2.SetColor("_EdgeColor", new Color(0.33725f, 0.72941f, 0));
                _time = 0;
                anim.SetFloat("skillnum", 2);
            }
        else if (isLight == true)
        {
            _time += Time.deltaTime;
            _Color = material2.GetColor("_EdgeColor");
            _Color.b = (int)Mathf.Abs(255 * Mathf.Sin(0.7856f * _time));
            _Color.b /= 255;
            material2.SetColor("_EdgeColor", _Color);
        }
    }


    public void HandleHpColor()
    {
        transform.tag = "HurtPlayer";
        spR.DOColor(new Color32(255, 150, 150, 255), 0.5f)
            .OnComplete(() => {
        spR.DOColor(new Color32(255, 255, 255, 255), 0.7f)
.OnComplete(() => { transform.tag = "Player"; });
    });
        head.DOColor(new Color32(255, 150, 150, 255), 0.5f)
            .OnComplete(() => { head.DOColor(new Color32(255, 255, 255, 255), 0.8f); });
    }


    public void HanleHp(int cur, int max)
    {
        HealthManager.Instance.UpdateHealthBar(cur, max, false);
        HealthManager.Instance.SetHealthValueText(cur, max, false);
    }



    private void startSound(int num)
    {
        AudioManager.Instance.PlaySound(num);
    }

     public void HandleDead()
    {
        _player.anim.SetInteger("state",6);
        _player.SetState(new PlayerDeadState(_player));
    }

    private void ToNextScene()
    {
        PlayerPrefs.SetInt("nextScene", 2);
        PlayerPrefs.SetInt("JumpTime", 14);
        SceneManager.LoadScene("Loading");
    }
}
