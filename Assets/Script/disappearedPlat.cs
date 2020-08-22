using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disappearedPlat : MonoBehaviour
{
   
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            gameObject.SetActive(false);
        }
    }

}
