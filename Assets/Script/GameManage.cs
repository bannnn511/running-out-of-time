using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManage : MonoBehaviour
{
    bool gameHasEnded = false;

    public GameObject player;
    public DeathMenu deathScreen;
    public GameObject levelWonScreen;

    public void FinishLevel()
    {
        Debug.Log("GG!");
        player.GetComponent<SpriteRenderer>().enabled = false;
        player.GetComponent<Player_Movement>().enabled = false;
        levelWonScreen.SetActive(true);
        if(SceneManager.GetActiveScene().buildIndex != 3){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void EndGame()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            player.GetComponent<Player_Movement>().enabled = false;
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
