using UnityEngine;
using UnityEngine.SceneManagement;
public class restart : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
public void RestartCurrentScene()
    {
        // Get the current active scene
        Scene currentScene = SceneManager.GetActiveScene();

        // Reload it
        SceneManager.LoadScene(currentScene.name);
    }
}
