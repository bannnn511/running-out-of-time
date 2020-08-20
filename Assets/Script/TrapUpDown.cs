using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapUpDown : MonoBehaviour
{
    private Vector3 velocity;
    public float speed;
    public GameObject target;
    public GameObject initialPosition;
    bool isUp;

    private void Start()
    {
        initialPosition.transform.position = transform.position;
    }
    private void Update()
    {

        if (transform.position != target.transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            Debug.Log("hi" + transform.position);
            if (transform.position == target.transform.position)
                isUp = true;
        }
        //transform.Translate(Vector3.right * Time.deltaTime);

        if (isUp == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, initialPosition.transform.position, speed * Time.deltaTime);
            Debug.Log("hi" + transform.position);
            if (transform.position == initialPosition.transform.position)
                isUp = false;
        }
    }
}
