using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int life;


    private void FixedUpdate()
    {
        if(life <= 0) EndGame();
    }


    public void EndGame()
    {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
