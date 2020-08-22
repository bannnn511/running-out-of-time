using UnityEngine;
using Pathfinding;

public class botScript : MonoBehaviour
{
  public float direction = 1;
  public float speed = 1.5f;
  public GameObject CenterOfGravity;
  public float GravityForce;
  public float chaseDistance = 4.5f;
  Animator enemyAnimator;
  private SpriteRenderer spriteRenderer;

  Rigidbody2D enemyRigidBody;
  // Start is called before the first frame update
  AIPath aiPath;
  // Start is called before the first frame update

  void Start()
  {
    enemyAnimator = GetComponent<Animator>();
    enemyRigidBody = GetComponent<Rigidbody2D>();
    spriteRenderer = GetComponent<SpriteRenderer>();
    aiPath = GetComponent<AIPath>();

  }

  // Update is called once per frame
  void Update()
  {
    DumbMovement();
    MirrorAnimation();
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
      if (aiPath.remainingDistance <= chaseDistance)
      {
        enemyRigidBody.simulated = true;
      }
      else
      {
        enemyRigidBody.simulated = false;
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
    }
  }


  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.CompareTag("turn"))
    {
      direction *= -1;
    }

  }

  private void MirrorAnimation()
  {
    Vector2 localVelocity = transform.InverseTransformDirection(enemyRigidBody.velocity);

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

    // Flip for ai path
    if (aiPath != null)
    {

      if (aiPath.desiredVelocity.x > 0.01f)
      {
        if (spriteRenderer.flipX == false)
        {
          spriteRenderer.flipX = true;
        }
      }

      else if (aiPath.desiredVelocity.x <= 0.01f)
      {
        if (spriteRenderer.flipX == true)
        {
          spriteRenderer.flipX = false;
        }
      }
    }
  }
}