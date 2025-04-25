using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyTracker : MonoBehaviour
{
    public Damageable[] enemies; // Asigna aquí a los dos caballeros desde el editor
    private bool gameEnded = false;

    void Update()
    {
        if (gameEnded) return;

        int aliveCount = 0;
        foreach (Damageable enemy in enemies)
        {
            if (enemy != null && enemy.IsAlive)
            {
                aliveCount++;
            }
        }

        if (aliveCount == 0)
        {
            gameEnded = true;
            StartCoroutine(LoadVictory());
        }
    }

    private System.Collections.IEnumerator LoadVictory()
    {
        yield return new WaitForSeconds(1f); // Espera opcional
        SceneManager.LoadScene("YouWin 1");    // Verifica el nombre exacto
    }
}
