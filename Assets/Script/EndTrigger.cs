using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    public GameManage gameManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            gameManager.FinishLevel();
        }
    }
}
