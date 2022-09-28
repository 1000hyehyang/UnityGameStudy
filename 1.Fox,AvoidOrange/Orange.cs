using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orange : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private Rigidbody2D rigidbody;
    
    public AudioClip clip;
   
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            GameManager.Instance.Score();
            animator.SetTrigger("orange");
        }

        if (collision.gameObject.tag == "Player")
        {
            SoundManager.instance.SFXPlay("Pop", clip);
            GameManager.Instance.GameOver();
            animator.SetTrigger("orange");
        }
    }
}
