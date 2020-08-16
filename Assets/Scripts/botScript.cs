using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class botScript : MonoBehaviour
{
	public float speed = 1.5f;
	public bool moveRight = true;
	public float chaseDistance = 4.5f;
	Animator enemyAnimator;

	Rigidbody2D enemyRigidBody;
	// Start is called before the first frame update
	AIPath aiPath;
	// Start is called before the first frame update

	void Start()
	{
		enemyAnimator = GetComponent<Animator>();
		enemyRigidBody = GetComponent<Rigidbody2D>();
		aiPath = GetComponent<AIPath>();
		InvokeRepeating("ChangeDirection", 0f, 2f);
	}

	// Update is called once per frame
	void Update()
	{
		DumbMovement();
		StartAttack();

	}

	/*
		Bot will start attack when bot and player is in a fixed distance
		For Fog of War, add sfx when bot start moving    -> Hieu heo lam cai nay nha :D
	*/
	void StartAttack()
	{

		if (aiPath != null)
		{
			if (aiPath.desiredVelocity.x <= 0.01f)
			{
				transform.eulerAngles = new Vector3(0, 180, 0);
			}
			else
			{
				transform.eulerAngles = new Vector3(0, 0, 0);
			}

			if (aiPath.remainingDistance <= chaseDistance)
			{
				aiPath.canMove = true;
				// enemyRigidBody.simulated = true;
			}
			else
			{
				aiPath.canMove = false;
				// enemyRigidBody.simulated = false;
			}
		}

	}

	/*
		Dumb movement left and right when player not in range
	*/
	void DumbMovement()
	{
		if (moveRight)
		{
			enemyRigidBody.velocity = new Vector2(speed, 0);
		}
		else
		{
			enemyRigidBody.velocity = new Vector2(-speed, 0);
		}
	}

	void ChangeDirection()
	{
		if (moveRight == true)
		{
			moveRight = false;
		}
		else
		{
			moveRight = true;
		}
	}
}
