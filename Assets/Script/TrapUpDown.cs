using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapUpDown : MonoBehaviour
{
    private Vector3 velocity;
    private void FixedUpdate()
    {
        transform.position += (velocity * Time.deltaTime);
    }
}
