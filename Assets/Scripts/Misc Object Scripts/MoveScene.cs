using System.Collections.Generic;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour
{
    public void NextScene()
    {
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            SceneManager.LoadScene("Level 1");
        }
        if (SceneManager.GetActiveScene().name == "Level 1")
        {
            SceneManager.LoadScene("Level 2");
        }
        else if (SceneManager.GetActiveScene().name == "Level 2")
        {
            SceneManager.LoadScene("Level 3");
        }
        else if (SceneManager.GetActiveScene().name == "Level 3")
        {
            SceneManager.LoadScene("Level 4");
        }
        else if (SceneManager.GetActiveScene().name == "Level 4")
        {
            SceneManager.LoadScene("Level 5");
        }
        else if (SceneManager.GetActiveScene().name == "Level 5")
        {
            SceneManager.LoadScene("Level 6");
        }
        else if (SceneManager.GetActiveScene().name == "Level 6")
        {
            SceneManager.LoadScene("Win Screen");
        }
        else if (SceneManager.GetActiveScene().name == "Win Screen")
        {
            Application.Quit();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            NextScene();
        }
    }
}
