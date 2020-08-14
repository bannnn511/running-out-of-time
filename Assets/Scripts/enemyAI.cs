using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class enemyAI : MonoBehaviour
{
	public GameObject CenterOfGravity;
	public float GravityForce;
	public Transform target;
	public float speed = 200f;
	public float nextWaypointDistance = 3f;

	Path path;
	int currentWaypoint = 0;
	bool reachEndOfPath = false;

	Seeker seeker;
	Rigidbody2D rb;
	SpriteRenderer spriteRenderer;

	// Start is called before the first frame update
	void Start()
	{
		seeker = GetComponent<Seeker>();
		rb = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();

		// InvokeRepeating("UpdatePath", 0f, .5f);

	}

	void UpdatePath()
	{
		if (seeker.IsDone())
		{
			seeker.StartPath(rb.position, target.position, OnPathComplete);
		}
	}

	// Update is called once per frame
	void Update()
	{
		checkEndOfPath();
		MirrorEneny();
		UpdatePath();
		GravityDrag();
	}

	private void FixedUpdate()
	{
		enenyMovement();
	}

	void OnPathComplete(Path p)
	{
		if (!p.error)
		{
			path = p;
			currentWaypoint = 0;
		}
	}

	void checkEndOfPath()
	{
		if (path == null)
		{
			return;
		}
		if (currentWaypoint >= path.vectorPath.Count)
		{
			reachEndOfPath = true;
			return;
		}
		else
		{
			reachEndOfPath = false;

		}
	}

	void enenyMovement()
	{
		Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
		Vector2 force = direction * speed * Time.deltaTime;

		float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
		rb.AddForce(force);
		if (distance < nextWaypointDistance)
		{
			currentWaypoint++;
		}

	}

	void MirrorEneny()
	{
		Vector2 localVelocity = transform.InverseTransformDirection(rb.velocity);

		if (localVelocity.x > 0.5F)
		{
			if (spriteRenderer.flipX == false)
			{
				spriteRenderer.flipX = true;
			}
		}

		else if (localVelocity.x < -0.5)
		{
			if (spriteRenderer.flipX == true)
			{
				spriteRenderer.flipX = false;
			}
		}
	}

	private void GravityDrag()
	{
		if (CenterOfGravity != null)
		{
			rb.AddForce((CenterOfGravity.transform.position - transform.position) * GravityForce);
			Vector3 dif = CenterOfGravity.transform.position - transform.position;
			float RotationZ = Mathf.Atan2(dif.y, dif.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0.0F, 0.0F, RotationZ + 90);
		}
	}

}
