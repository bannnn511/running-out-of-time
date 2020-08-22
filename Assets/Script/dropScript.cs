using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropScript : MonoBehaviour
{
    Rigidbody2D RB2D;
    // Start is called before the first frame update
    public GameObject Player;
    void Start()
    {
        RB2D = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            WaitForSecs(4f);
            RB2D.isKinematic = false;
            //GetComponent<Collider2D>().enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Ground")
        {
            Physics2D.IgnoreCollision(Player.GetComponent<Collider2D>(), GetComponent<PolygonCollider2D>());
            RB2D.isKinematic = true;
     
        }
    }

    IEnumerator WaitForSecs(float secs)
    {
        yield return new WaitForSeconds(secs);
    }
}
