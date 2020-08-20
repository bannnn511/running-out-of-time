using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapUpDown : MonoBehaviour
{
    public float speed;
    public GameObject target;
    public GameObject initialPosition;
    bool isUp;

    private void Start()
    {
        initialPosition.transform.position = transform.position;
        isUp = false;
    }
    private void Update()
    {

        if (isUp == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            if (transform.position == target.transform.position)
            {
                isUp = true;
            }
        }

        if (isUp == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, initialPosition.transform.position, speed * Time.deltaTime);
            if (transform.position == initialPosition.transform.position)
                isUp = false;
        }
    }
}
