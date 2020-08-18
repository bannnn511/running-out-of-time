using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class botScript : MonoBehaviour
{
  public float direction = 1;
  public float speed = 1.5f;
  public GameObject CenterOfGravity;
  public float GravityForce;
  public bool moveRight = false;
  public float chaseDistance = 4.5f;
  public float repeat = 2f;
  public Transform collisionPoint;
  public float collisionRange = 0.5f;
  public LayerMask enemyLayer;
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
    InvokeRepeating("ChangeDirection", 0f, repeat);
  }

  // Update is called once per frame
  void Update()
  {
    DumbMovement();
    StartAttack();
    GravityDrag();
  }

  private void GravityDrag()
  {
    if (CenterOfGravity != null && aiPath == null)
    {
      enemyRigidBody.AddForce((CenterOfGravity.transform.position - transform.position) * GravityForce);
      Vector3 dif = CenterOfGravity.transform.position - transform.position;
      float RotationZ = Mathf.Atan2(dif.y, dif.x) * Mathf.Rad2Deg;
      transform.rotation = Quaternion.Euler(0.0F, 0.0F, RotationZ + 90);
    }
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

  private float CalculateAngularSpeedLimitation()
  {

    if (CenterOfGravity != null)
    {
      float speedLimitation;
      float distance;

      distance = Vector3.Distance(transform.position, CenterOfGravity.transform.position);
      distance = distance / 10;

      speedLimitation = Mathf.Lerp(0.5F, 2F, distance);
      speedLimitation = speedLimitation / 5;

      return speedLimitation;

    }
    else { return 1; } //If the player don't have gravity center, no speed limitation is set.
  }

  /*
		Dumb movement left and right when player not in range
	*/
  void DumbMovement()
  {
    if (aiPath == null)
    {
      Vector2 localvelocity;
      localvelocity = transform.InverseTransformDirection(enemyRigidBody.velocity);
      localvelocity.x = direction * Time.deltaTime * speed * 100 * CalculateAngularSpeedLimitation();
      enemyRigidBody.velocity = transform.TransformDirection(localvelocity);
      Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(collisionPoint.position, collisionRange, enemyLayer);
      if (hitEnemies.Length != 0)
      {
        Debug.Log("change direction");
        direction *= -1;
      }
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
