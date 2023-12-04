using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMainMenu : MonoBehaviour
{
    public int gameMenuScreen;

    public void PauseMainMenuButton()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(gameMenuScreen);
    }
}
