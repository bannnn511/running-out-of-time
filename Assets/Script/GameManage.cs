using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManage : MonoBehaviour
{
    bool gameHasEnded = false;

    public GameObject player;
    public DeathMenu deathScreen;

    public void FinishLevel()
    {
        Debug.Log("GG!");
    }

    public void EndGame()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("Noob");
            RestartGame();
        }
        
    }

    void RestartGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        deathScreen.gameObject.SetActive(true);
        //player.gameObject.SetActive(false);
    }
}
