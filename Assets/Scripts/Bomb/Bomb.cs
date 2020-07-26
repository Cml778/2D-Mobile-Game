using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Animator anim;
    public float startTime;
    public float waitTime;
    public float bombForce;
    private Collider2D coll;
    private Rigidbody2D rb;
    [Header("Check")]
    public float radius;
    public LayerMask targetLayer;
    void Start()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Bomb_off"))
		{
            if (Time.time > startTime + waitTime)
            {
                anim.Play("Bomb_explotion");
            }
        }
    }

    public void OnDrawGizmos()
	{
        Gizmos.DrawWireSphere(transform.position, radius);
	}
    //爆炸  检测周围所有的碰撞体，并施加一个力
    public void Explotion()      //anim 的Event
	{
        coll.enabled = false;
        Collider2D[] aroundObjects = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);
        rb.gravityScale = 0;
        foreach(var item in aroundObjects)
		{
            //判断该碰撞体在炸弹的左边还是右边，然后给一个相反的力
            Vector3 pos = transform.position - item.transform.position;
            item.GetComponent<Rigidbody2D>().AddForce((-pos + Vector3.up) * bombForce, ForceMode2D.Impulse);   //impulse 瞬间的力，另一个就是持续的力   Vector3.One    (1,1,1)
			if (item.CompareTag("Bomb") && item.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Bomb_off"))
			{
                item.GetComponent<Bomb>().TurnOn();    //重新点燃炸弹
			}
        }
	}

    public void MyDestroy()
	{
        Destroy(this.gameObject);
	}

    //吹灭的方法
    public void TurnOff()
	{
        anim.Play("Bomb_off");
        gameObject.layer = LayerMask.NameToLayer("NPC");
	}
    //点燃炸弹
    public void TurnOn()
	{
        startTime = Time.time;
        anim.Play("Bomb");
        gameObject.layer = LayerMask.NameToLayer("Bomb");
    }
}
