using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
	public GameObject target;
	private bool[,] movableTiles;
	public float visibleRange;
	private IEnumerator goHitArray;
	private RaycastHit2D[] hitObjects;
	private bool search = false;
	void Start()
	{
		StartCoroutine(checkHitArray(1f));
	}

	void FixedUpdate()
	{
		hitObjects = Physics2D.CircleCastAll(transform.position, visibleRange, transform.position, 0f, 8, -5, 5);
		

	}

	private IEnumerator checkHitArray(float waitTime)
	{
		while (true)
		{
			Debug.Log("Coroutine");

			yield return new WaitForSeconds(waitTime);

			Debug.Log("And now it's after the wait");
		}
	}
}