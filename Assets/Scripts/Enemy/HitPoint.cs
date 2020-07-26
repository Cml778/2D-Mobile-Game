using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoint : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D coll)
	{
		if(coll.CompareTag("Player"))
		{

		}
		if (coll.CompareTag("Bomb"))
		{

		}
	}
}
