using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    public int gameMenuScreen;

    public void StartGame()
    {
        SceneManager.LoadScene(gameMenuScreen);
    }
}
