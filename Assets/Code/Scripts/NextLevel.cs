using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public int nextLevel;

    public void AdvancingLevels()
    {
        SceneManager.LoadScene(nextLevel); ;
    }
}
