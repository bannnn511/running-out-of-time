using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotDumpPatrol : MonoBehaviour
{
  public float speed;
  public Transform[] moveSpots;
  private int randomSpot;
  private int spotIndex;
  public float startWaitTime;
  private float waitTime;

  // Start is called before the first frame update
  void Start()
  {
    spotIndex = 0;
    randomSpot = Random.Range(0, moveSpots.Length);
  }

  // Update is called once per frame
  void Update()
  {
    DoomPatrol();
  }

  /*
      transform.position == moveSpots[randomSpot].position does not function as intended because of float exponents
      Check distance between two points for a fixed value instead
      
      */
  private void DoomPatrol()
  {
    transform.position = Vector2.MoveTowards(transform.position, moveSpots[spotIndex].position, speed * Time.deltaTime);

    if (Vector2.Distance(transform.position, moveSpots[spotIndex].position) < 0.2f)
    {
      if (waitTime <= 0)
      {
        spotIndex++;
        waitTime = startWaitTime;
      }
      else
      {
        waitTime -= Time.deltaTime;
      }
    }

    if (spotIndex == moveSpots.Length)
    {
      spotIndex = 0;
    }
  }

  private void DoomPatrolRandom()
  {
    transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);

    if (Vector2.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f)
    {
      if (waitTime <= 0)
      {
        randomSpot = Random.Range(0, moveSpots.Length);
        waitTime = startWaitTime;
      }
      else
      {
        waitTime -= Time.deltaTime;
      }
    }
  }

}
