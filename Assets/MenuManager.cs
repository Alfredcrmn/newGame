using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement; // Para cargar la escena de GameOver

public class MenuManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnStart()
    {
        // Cargar la escena "MainMenu" al iniciar el juego
        UnityEngine.SceneManagement.SceneManager.LoadScene("Faction selection");
    }
    public void QuitGame()
    {
        // Quit the application
        Application.Quit();
    }
}
