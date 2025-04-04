using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void RestartGame()
    {
        // Reload the current scene to restart the game
        SceneManager.LoadScene("Faction selection");
    }
}
