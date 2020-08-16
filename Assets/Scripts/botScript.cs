using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class botScript : MonoBehaviour
{
	public int maxHealth = 120;
	Animator enemyAnimator;
	int currentHealth;
	Rigidbody2D enemyRigidBody;
	// Start is called before the first frame update
	AIPath aiPath;
	// Start is called before the first frame update
	void Start()
	{
		enemyAnimator = GetComponent<Animator>();
		currentHealth = maxHealth;
		enemyRigidBody = GetComponent<Rigidbody2D>();
		enemyRigidBody.simulated = false;
		aiPath = GetComponent<AIPath>();
	}

	// Update is called once per frame
	void Update()
	{
		if (aiPath.desiredVelocity.x <= 0.01f)
		{
			transform.eulerAngles = new Vector3(0, 180, 0);
		}
		else
		{
			transform.eulerAngles = new Vector3(0, 0, 0);
		}

		if (aiPath.remainingDistance <= 4)
		{
			Debug.Log("aaaaaaaaaaaaa");
			enemyRigidBody.simulated = true;
		}
		else
		{
			Debug.Log("bbbbbbbbbbb");
			enemyRigidBody.simulated = false;
		}
	}
}
