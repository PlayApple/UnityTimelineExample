using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableSpawner : MonoBehaviour
{
	public GameObject[] collectablesToSpawn;
	public KeyCode spawnKey;
	public int xRange;
	public int zRange;
	public int yHeight;

	void Update ()
	{
		if (Input.GetKeyDown (spawnKey)) {
			SpawnObject ();
		}
	}

	void SpawnObject ()
	{
		int randomCollectable = Random.Range (0, collectablesToSpawn.Length);
		int xPosition = Random.Range (-xRange, xRange);
		int zPosition = Random.Range (-zRange, zRange);
		Vector3 randomPosition = new Vector3 (xPosition, yHeight, zPosition);
		Instantiate (collectablesToSpawn [randomCollectable], transform.position + randomPosition, transform.rotation);
	}
}
