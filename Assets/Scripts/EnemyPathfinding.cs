using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyPathfinding : MonoBehaviour
{
	public GameObject target;
	private bool[,] movableTiles;
	public float visibleRange;
	private IEnumerator goHitArray;
	private RaycastHit2D[] hitObjects;
	void FixedUpdate()
	{
		hitObjects = Physics2D.CircleCastAll(this.transform.position, visibleRange, this.transform.position, 0f, 8, -5, 5);
		goHitArray = checkHitArray(0.5f);
		StartCoroutine(goHitArray);
	}

	private IEnumerator checkHitArray(float waitTime)
	{
		for (int i = 0; i < hitObjects.Length; i++)
		{
			if ((((Vector2)transform.position - hitObjects[i].point).magnitude <= visibleRange) && hitObjects[i].collider.tag == "Goblin")
			{
				Debug.Log("Goblin hit");
				yield return new WaitForSeconds(waitTime);
			}
		}
	}
}
