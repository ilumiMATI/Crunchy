using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [Header("Scene names")]
    [SerializeField] string startScene;
    [SerializeField] string gameOverScene;
    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
    public void LoadStartScene()
    {
        SceneManager.LoadScene(startScene);
        FindObjectOfType<GameSession>().ResetGame();
    }
    public void LoadGameOverScene()
    {
        SceneManager.LoadScene(gameOverScene);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
