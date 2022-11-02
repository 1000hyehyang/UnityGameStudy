using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float x; //x축 입력값
    Rigidbody2D rigid;
    bool isOnGround;
    bool isDamaged;
    Animator animator;
    SpriteRenderer sr;
    public AudioSource star;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }


    void Update() //Input과 같은 것은 여기에 넣어주자
    {
        if(!isDamaged)  //이거 isDamaged로 안묶어주면 enemy에 맞고 튕길 때 y축으로만 뜀
        {
            x = Input.GetAxisRaw("Horizontal"); //방향키 입력

            if (Input.GetKeyDown(KeyCode.Space) && isOnGround) //점프
            {
                rigid.AddForce(Vector2.up * 7f, ForceMode2D.Impulse);
                animator.SetTrigger("shouldJump");
                isOnGround = false;
                animator.SetBool("isOnGround", false);
            }
        }
       

        //경사면에서 튕김 방지
        if (isOnGround)
        {
            if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                rigid.velocity = new Vector2(rigid.velocity.x, -1.67f);
            }

        }
        if (Mathf.Abs(x) > 0.25f && isOnGround) animator.SetBool("isSkipping", true);
        else animator.SetBool("isSkipping", false);

        if (x < 0) sr.flipX = true;
        else if (x > 0) sr.flipX = false; //캐릭터 이동시 좌우 반전
    }

    void FixedUpdate() //물리법칙과 관련된 것들을 연속적으로 사용할 때 넣어주자(rigidbody와 관련된 것 등)
    {
       if(!isDamaged)
        {
            rigid.velocity = new Vector2(x * 200f * Time.deltaTime, rigid.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            isOnGround = true;
            animator.SetBool("isOnGround", true);
        }
        //Star먹음
        if (collision.gameObject.tag == "Star")
        {
            Destroy(collision.gameObject);
            star.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            isOnGround = false;
            animator.SetBool("isOnGround", false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //Attack
            if (rigid.velocity.y < 0 && transform.position.y > collision.transform.position.y)
            {
                OnAttack(collision.transform);
            }
            else  //Damaged 
                OnDamaged(collision.transform.position);
            
        }
    }

    void OnAttack(Transform enemy)
    {
        //Point

        //Enemy Die
        EnemyMove enemyMove = enemy.GetComponent<EnemyMove>();
        enemyMove.OnDamaged();
    }

    void OnDamaged(Vector2 targetPos)
    {
        isDamaged = true;

        gameObject.layer = 10;

        sr.color = new Color(1, 1, 1, 0.6f);

        //Reaction Force
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1) * 3, ForceMode2D.Impulse);

        //Animation
        animator.SetTrigger("damaged");

        Invoke("OffDamaged", 1);
    }

    void OffDamaged()
    {
        isDamaged = false;
        gameObject.layer = 9;
        sr.color = new Color(1, 1, 1, 1);
    }
}
