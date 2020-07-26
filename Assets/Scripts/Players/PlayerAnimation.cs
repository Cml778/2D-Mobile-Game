using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private PlayerController playerController;
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        anim.SetFloat("speed", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("veloctyY", rb.velocity.y);
        anim.SetBool("jump", playerController.isJump);
        anim.SetBool("isGround", playerController.isGround);
    }
}
