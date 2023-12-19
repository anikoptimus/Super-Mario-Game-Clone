using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailMovement : MonoBehaviour
{
    public Transform Down_Collision,Left_Collission,Right_Collission,Top_Collission;
    private Rigidbody2D mybody;
    private Animator anim;
    public float moveSpeed = 1f;
    private bool moveLeft;
    private bool canMove;
    private bool stunned;
    private Vector3 Left_Collission_pos, Right_Collission_pos;
    public LayerMask playerLayer;





    void Awake() {
        mybody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Left_Collission_pos = Left_Collission.position;
        Right_Collission_pos = Right_Collission.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        moveLeft = true;
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            if (moveLeft)
            {
                mybody.velocity = new Vector2(-moveSpeed, mybody.velocity.y);
            }
            else
            {
                mybody.velocity = new Vector2(moveSpeed, mybody.velocity.y);
            }
        }
      
        CheckCollision();
        
    }// end update
    void CheckCollision()
    {
        RaycastHit2D leftHit = Physics2D.Raycast(Left_Collission.position, Vector2.left, 0.1f, playerLayer);
        RaycastHit2D rightHit = Physics2D.Raycast(Right_Collission.position, Vector2.right, 0.1f, playerLayer);

        Collider2D topHit = Physics2D.OverlapCircle(Top_Collission.position, 0.2f, playerLayer);

        if (topHit != null)
        {
            if (topHit.gameObject.tag == MyTag.PLAYER_TAG)


             
            {
                if (!stunned)
                {
                    topHit.gameObject.GetComponent<Rigidbody2D>().velocity =
                        new Vector2(topHit.gameObject.GetComponent<Rigidbody2D>().velocity.x, 7f);

                    canMove = false;
                    mybody.velocity = new Vector2(0, 0);

                    anim.Play("Stunned");
                    stunned = true;
                    // beetal game object
                    if (gameObject.tag == MyTag.BEETLE_TAG)
                    {
                        anim.Play("Stunned");
                        StartCoroutine(Dead(0.5f));

                    }
                }// 3rd if
            }// end 2nd if
        }// end 1st if


        

        if (leftHit)
        {
            if(leftHit.collider.gameObject.tag == MyTag.PLAYER_TAG)
            {
                if (!stunned)
                {
                    // player death
                    print("damage");
                }
                else
                {
                    if(tag != MyTag.BEETLE_TAG)
                    {
                        mybody.velocity = new Vector2(15f, mybody.velocity.y);
                        StartCoroutine(Dead(3));
                    }
                    
                }
            }
        }
        if (rightHit)
        {
            if (rightHit.collider.gameObject.tag == MyTag.PLAYER_TAG)
            {
                if (!stunned)
                {
                    // player death
                    print("damage");
                }
                else
                {
                    if (tag != MyTag.BEETLE_TAG)
                    {
                        mybody.velocity = new Vector2(-15f, mybody.velocity.y);
                        StartCoroutine(Dead(3));
                    }
                }
            }
        }

        if (!Physics2D.Raycast(Down_Collision.position, Vector2.down, 0.1f))
        {
            ChangeDirection();
        }
    }

        void ChangeDirection()

    {
        moveLeft = !moveLeft;
        Vector3 tempScale = transform.localScale;

        if (moveLeft)
        {
            tempScale.x = Mathf.Abs(tempScale.x);
            Left_Collission_pos = Left_Collission.position;
            Right_Collission_pos = Right_Collission.position;

        }
        else
        {
            tempScale.x =-Mathf.Abs(tempScale.x);
            Left_Collission_pos = Right_Collission.position;
            Right_Collission_pos = Left_Collission.position;
        }

        transform.localScale = tempScale;
    }

  
	IEnumerator Dead(float timer) {
		yield return new WaitForSeconds (timer);
		gameObject.SetActive (false);
	}


    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == MyTag.BULLET_TAG)
        {
            canMove = false;
            anim.Play("Stunned");
            mybody.velocity = new Vector2(0, 0);
            StartCoroutine(Dead(0.9f));
        }
        if (target.tag == MyTag.SNAIL_TAG)
        {
            if (!stunned)
            {
                canMove = false;
                anim.Play("Stunned");
                mybody.velocity = new Vector2(0, 0);
                stunned = true;
                StartCoroutine(Dead(2));

            }
        }
        
    }







}//end of the calss

