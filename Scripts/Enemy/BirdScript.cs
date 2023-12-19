using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    private Rigidbody2D myBody;
    private Animator anim;
    private Vector3 moveDirection = Vector3.left;
    private Vector3 originPosition;
    private Vector3 movePosition;
    public LayerMask playerLayer;
    public GameObject birdEgg;
    private bool attacked;
    private bool canMove;
    public float speed;

    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }
    // Start is called before the first frame update
    void Start()
    {
        originPosition = transform.position;
        originPosition.x += 6f;

        movePosition = transform.position;
        movePosition.x -= 6f;
        canMove = true;

    }

    // Update is called once per frame
    void Update()
    {
        BirdMove();
        DropEgg();
    }
    void BirdMove()
    {
        if (canMove)
        {
            transform.Translate(moveDirection * speed * Time.smoothDeltaTime);
             if(transform.position.x >= originPosition.x)
            {
                moveDirection = Vector3.left;
                changeDirection(0.5f);

            }else if(transform.position.x <=movePosition.x)
            {
                moveDirection = Vector3.right;
                changeDirection(-0.5f);
            }
        }// bird move

        void changeDirection(float direction)
        {
            Vector3 tempScale = transform.localScale;
            tempScale.x = direction;
            transform.localScale = tempScale;
        }

    } void DropEgg()
    {
        if (!attacked)
        {
            if (Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, playerLayer))
            {
                Instantiate(birdEgg, new Vector3(transform.position.x,
         transform.position.y - 1f, transform.position.z), Quaternion.identity);
                anim.Play("BirdMove");
                attacked = true;
            }
        }
    }

    IEnumerator BirdDeath()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == MyTag.BULLET_TAG)
        {
            anim.Play("BirdDeath");
            GetComponent<BoxCollider2D>().isTrigger = true;
            myBody.bodyType = RigidbodyType2D.Dynamic;
            canMove = false;

            StartCoroutine(BirdDeath());
        }
    }



}//class
