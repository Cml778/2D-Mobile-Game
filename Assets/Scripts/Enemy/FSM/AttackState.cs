using System;
using UnityEngine;

public class AttackState : EnemybaseState
{
	public override void EnterState(Enemy enemy)
	{
		enemy.animState = 2;
		enemy.targetPoint = enemy.attackList[0];
	}

	public override void OnUpdate(Enemy enemy)
	{
		enemy.animState = 2;
		if (enemy.attackList.Count == 0)
		{
			enemy.TransitionToState(enemy.patrolState);
		}
		if(enemy.attackList.Count > 1)  //可攻击目标的数量是否存在
		{
			enemy.animState = 1;
			for(int i = 0; i < enemy.attackList.Count; i++)
			{
				if(Mathf.Abs(enemy.transform.position.x - enemy.attackList[i].position.x) < 
					Mathf.Abs(enemy.transform.position.x - enemy.targetPoint.position.x))
				{
					enemy.targetPoint = enemy.attackList[i];
				}
			}
		}
		if (enemy.attackList.Count == 1)
			enemy.targetPoint = enemy.attackList[0];
		if (enemy.targetPoint.CompareTag("Player"))
			enemy.AttackAction();
		if (enemy.targetPoint.CompareTag("Bomb"))
			enemy.SkillAction();

		enemy.MoveToTarget();
	}
}
