using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{

    public float speedY;
    private float speedX;
    public float jumpForce1;
    private RaycastHit2D hit;
    public float gravity;
    public float cTime;
    private float horizontal;
    int numjump = 0;
    bool isJump = false;


    [HideInInspector]
    public Rigidbody2D rig;
    Animator anim;


    // Use this for initialization
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rayt();
        horizontal = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.X) && isJump==true&&numjump!=3)
        {
            if (anim.GetInteger("JumpAt") == 2)
            {
                anim.SetInteger("JumpAt", 3);
            }
            else
            {
                anim.SetInteger("JumpAt", 1);
            }
        }

        if (horizontal != 0)
        {
            anim.SetFloat("Speed", horizontal > 0 ? 1 : -1);
        }
        speedX = horizontal * 3.0f;
        
        anim.SetFloat("Direction", horizontal);
  
        if (Input.GetKeyUp(KeyCode.C) && numjump != 2)
        {
            ++numjump;
            cTime = 0;
        }
        if (Input.GetKeyDown(KeyCode.C) && numjump != 2)
        {
            if(numjump==1)
            {
                anim.SetInteger("JumpAt", 2);
            }
            rig.gravityScale = 0;
            anim.SetInteger("state", 1);
            isJump = true;
            speedY = 1.0f;
            rig.velocity = new Vector2(speedX, speedY);
        }

        if (Input.GetKey(KeyCode.C) && isJump == true && numjump != 2)
        {
            if (cTime < 12)
            {
                cTime += 1;
                speedY += 1.0f;

            }

            speedY = Mathf.Min(speedY, 10);

        }

        if (isJump == false)
        {
            rig.velocity = new Vector2(speedX, rig.velocity.y);
        }
    
  

    }




    /*void RayTest()
    {
        
        Vector3 pos = transform.position;
        pos.x += m1;
        pos.y += m2;
        Debug.DrawRay(pos, Vector2.down, Color.red);
        hit = Physics2D.Raycast(pos, Vector2.down, m3);
        if (hit.transform != null)
        {
            if (hit.collider.tag.CompareTo("path") == 0&&rig.velocity.y<0)
            {
                rig.velocity = new Vector2(0, 0);
                Debug.Log(1);
                //isJump = false;
                anim.SetInteger("state", 0);
            }
        }
    }*/

    bool rayt()
    {
        Vector3 pos = transform.position;
        hit = Physics2D.Raycast(pos, Vector2.down, 9.5f);
        if (hit.transform != null)
        {
            return hit.collider.tag.CompareTo("path") == 0 ? true : false;
        }
        return false;
    }

    private void FixedUpdate()
    {

        if (isJump == true)
        {
            speedY -= gravity;
            speedY = Mathf.Max(speedY, -10);
            rig.velocity = new Vector2(speedX, speedY);
       
        }
        if (rig.velocity.y >= 0 && rig.gravityScale == 0)
        {
            anim.SetFloat("JumpDirection", 0);
        }
        else if (rig.velocity.y<0&&rig.gravityScale==0)
        {
            anim.SetFloat("JumpDirection", 1);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag.CompareTo("path") == 0)
        {
            
            anim.SetInteger("state", 0);
            anim.SetFloat("JumpDirection", 0);
            anim.SetInteger("JumpAt", 0);
            rig.velocity = new Vector2(0, 0);
            numjump = 0;
            cTime = 0;
            speedY = 0;
            rig.gravityScale = 2.5f;
            isJump = false;

        }
    }



    private void SetJAttack(int num)
    {
        anim.SetInteger("JumpAt", num);
    }

    
}
    


