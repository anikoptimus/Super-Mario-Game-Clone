using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlyerMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D mybody;
    private Animator anim;
    public Transform groundCheckPosition;
    public LayerMask groundLayer;
    private bool isGrounded;
    private bool jumped;
   private float jumpPower = 12f;


    void Awake() {
        mybody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfGrounded();
        PlayerJump();
    }
    void FixedUpdate() {

        PlayerWalk();
    }

    void PlayerWalk() {

        float h = Input.GetAxisRaw("Horizontal");
        if (h > 0)
        {
            mybody.velocity = new Vector2(speed, mybody.velocity.y);
            ChangeDirection(1);
        }
        else if (h < 0)
        {
            mybody.velocity = new Vector2(-speed, mybody.velocity.y);
            ChangeDirection(-1);
        }
      else
       {
            mybody.velocity = new Vector2 (0f,mybody.velocity.y);
        }

        anim.SetInteger("Speed", Mathf.Abs((int)mybody.velocity.x));

        void ChangeDirection(int direction)
        {
            Vector3 tempScale = transform.localScale;
            tempScale.x = direction;
            transform.localScale = tempScale;
        }


    }
    void CheckIfGrounded()
    {
        isGrounded = Physics2D.Raycast(groundCheckPosition.position, Vector2.down, 0.01f, groundLayer);
        if (jumped)
        {
            jumped = false;
            anim.SetBool("Jump", false);

        }
    }

    void PlayerJump()
    {
        if (isGrounded)
        {
            
            if (Input.GetKey(KeyCode.Space  ))
            {
                jumped = true;
                mybody.velocity = new Vector2(mybody.velocity.x, jumpPower);

                anim.SetBool("Jump", true);
            }
        }
    }



}// class end

