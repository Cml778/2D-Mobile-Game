using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	EnemybaseState currentState;

	public Animator anim;
	public int animState;

	[Header("Movement")]
	public float speed;
	public Transform pointA, pointB;
	public Transform targetPoint;

	[Header("Attack Setting")]
	private float nextAttack = 0;
	public float attackRate;
	public float attackRange, skillRange;

	public List<Transform> attackList = new List<Transform>();   //通过add添加

	public PatrolState patrolState = new PatrolState();    //巡逻状态
	public AttackState attackState = new AttackState();    //攻击状态

	public virtual void Init()
	{
		anim = GetComponent<Animator>();
	}
	private void Awake()
	{
		Init();
	}
	private void Start()
	{
		TransitionToState(patrolState);
	}
	private void Update()
	{
		currentState.OnUpdate(this);
		anim.SetInteger("state", animState);
	}
	public void TransitionToState(EnemybaseState state)
	{
		currentState = state;
		currentState.EnterState(this);
	}
	public void MoveToTarget()    //向目标点移动
	{
		transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);
		FilpDirection();
	}
	
	public void FilpDirection()   //反转人物
	{
		if (transform.position.x < targetPoint.position.x)
			transform.rotation = Quaternion.Euler(0f, 0f, 0f);
		else
			transform.rotation = Quaternion.Euler(0f, 180f, 0f);
	}
	public void SwitchPoint()    //切换目标点
	{
		if(Mathf.Abs(transform.position.x - pointA.position.x)> Mathf.Abs(transform.position.x - pointB.position.x))
		{
			targetPoint = pointA;
		}
		else
		{
			targetPoint = pointB;
		}

	}
	//攻击敌人
	public void AttackAction()
	{
		//判断是否可以攻击玩家
		if (Vector2.Distance(transform.position, targetPoint.position) < attackRange)
		{
			if(Time.time > nextAttack)
			{
				//播放攻击动画以及效果
				anim.SetTrigger("attack");
				Debug.Log("attack");
				nextAttack = Time.time + attackRate;
			}
		}

	}
	public virtual void SkillAction()           //对炸弹使用技能
	{
		if(Vector2.Distance(transform.position,targetPoint.position) < skillRange)
		{
			if(Time.time > nextAttack)
			{
				//播放特殊攻击动画以及效果
				anim.SetTrigger("canSkill");
				Debug.Log("skill");
				nextAttack = Time.time + attackRate;
			}
		}
	}
	public void OnTriggerStay2D(Collider2D coll)
	{
		if(!attackList.Contains(coll.transform))
			attackList.Add(coll.transform);
	}
	public void OnTriggerExit2D(Collider2D coll)
	{
		if (attackList.Contains(coll.transform))
			attackList.Remove(coll.transform);
	}
}
