using UnityEngine;

public class Quit : MonoBehaviour
{
   // Call this method from a button
    public void QuitApplication()
    {
        Debug.Log("Quit button pressed.");

        // Quit the application
        Application.Quit();

        // Note: This only works in a built application, not in the Unity Editor.
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // stop play mode in editor
#endif
    }
}
