using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    public int nextMove;
    Animator anim;
    SpriteRenderer spriteRenderer;
    CapsuleCollider2D ccl;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        ccl = GetComponent<CapsuleCollider2D>();

        Invoke("Think", 3);
    }

    void FixedUpdate()
    {
        //Move
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y); 

        //Platform Check
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.5f, rigid.position.y*0.05f);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));

        if (rayHit.collider == null)
        {
            Turn();
        }
    }

    //재귀 함수: 자기 자신을 호출하는 함수; 딜레이 없이 재귀 함수 사용은 위험!
    void Think()
    {
        //Set Next Active
        nextMove = Random.Range(-1, 2);


        //Sprite Animation
        anim.SetInteger("WalkSpeed", nextMove);

        //방향 전환
        if (nextMove != 0)
            spriteRenderer.flipX = nextMove == 1;

        //Recursive
        float nextThinkTime = Random.Range(5f, 10f);
        Invoke("Think", nextThinkTime);
    }

    void Turn()
    {
        nextMove *= -1;
        spriteRenderer.flipX = nextMove == 1;

        CancelInvoke();
        Invoke("Think", 3);
    }

    public void OnDamaged()
    {
        //Sprite Alpha
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        //Sprite Flip Y
        spriteRenderer.flipY = true;
        //Collider Disable
        ccl.enabled = false;
        //Die Effect Jump
        rigid.AddForce(Vector2.up * 3, ForceMode2D.Impulse);
        //Destroy
        Invoke("DeActive", 2);
    }

    void DeActive()
    {
        gameObject.SetActive(false);
    }
}
