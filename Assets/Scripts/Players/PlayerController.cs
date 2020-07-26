using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Rigidbody2D rb;
    public float speed;
    public float jumpForce;
    [Header("地面检测")]
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask groundLayer;

    [Header("状态检测")]
    public bool isGround;
    public bool isJump;
    public bool canJump = false;

    [Header("Jump FX")]
    public GameObject jumpFX;
    public GameObject landFX;

    [Header("Attack Settings")]
    public GameObject bombPrefab;
    public float nextAttack = 0;
    public float attackRate;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpFX.SetActive(false);
        landFX.SetActive(false);
    }

    void Update()
    {
        CheckInput();
    }
    private void FixedUpdate()
    {
        PhysicCheck();
        Movement();
        jump();
    }


    void CheckInput()
    {
        if (Input.GetButtonDown("Jump") && isGround)
        {
            canJump = true;
        }
		if (Input.GetKeyDown(KeyCode.J))
		{
            Attack();
		}
    }
    void Movement()
    {
        float HorizontalInput = Input.GetAxisRaw("Horizontal");    //-1~1

        rb.velocity = new Vector2(HorizontalInput * speed, rb.velocity.y);      //持续判断是否为移动，没移动但受到了爆炸效果这向反方向移动
        if(HorizontalInput == 1 || HorizontalInput == -1)
        {
            transform.localScale = new Vector3(HorizontalInput, 1, 1);
        }
        
    }
    void jump()
    {
        if (canJump)
        {
            isJump = true;
            jumpFX.SetActive(true);
            jumpFX.transform.position = transform.position + new Vector3(0,-0.45f,0);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            rb.gravityScale = 4;
            canJump = false;
        }
    }

    public void Attack()
	{
		if (Time.time > nextAttack)
		{
            Instantiate(bombPrefab, transform.position, transform.rotation);
            nextAttack = Time.time + attackRate;
		}
	}

    //物理检测，检测是否站在地面上
    public void PhysicCheck()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
        if (isGround)
        {
            isJump = false;
            rb.gravityScale = 1;
        }
    }

    //落地之后
    public void Landed()
    {
        landFX.SetActive(true);
        landFX.transform.position = transform.position + new Vector3(0, -0.75f, 0);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
    }
}
