using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class secondHandsMovement : MonoBehaviour
{

  float rotationMultiplier = 6.0f;
  public float timeRemaining = 60;

  void Update()
  {
    if (timeRemaining > 0)
    {
      timeRemaining -= Time.deltaTime;
      transform.Rotate(Vector3.forward, Time.deltaTime * rotationMultiplier * -1f);
    }
    else
    {
      timeRemaining = 0;
      Debug.Log("Time's up!");
    }

  }

  private void OnCollisionEnter2D(Collision2D other)
  {
    if (other.collider.tag == "DeathTrap")
    {
      Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other.collider);
    }
  }


}
