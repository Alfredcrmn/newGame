using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;
public class YouWinScreen : MonoBehaviour
{
// Start is called once before the first execution of Update after the MonoBehaviour is created
    public void RestartGame()
    {
        // Load the scene with the name "MainMenu"
        SceneManager.LoadScene("Faction selection");
    }
    public void QuitGame()
    {
        // Quit the application
        Application.Quit();
    }
}
