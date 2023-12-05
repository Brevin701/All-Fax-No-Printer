using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FastForward : MonoBehaviour
{
    private int pressCount = 0;
    [SerializeField] private TMP_Text fastForward;

    public void OnClick()
    {
        pressCount++;

        if (pressCount == 1)
        {
            Time.timeScale = 2.0f;
            fastForward.text = (Time.timeScale + "x").ToString();
        }
        else if (pressCount == 2)
        {
            Time.timeScale = 3.0f;
            fastForward.text = "3x";
        }
        else if (pressCount == 3)
        {
            Time.timeScale = 5.0f;
            fastForward.text = "5x";
        }
        else if (pressCount == 4)
        {
            Time.timeScale = 1.0f;
            fastForward.text = "1x";
            pressCount = 0; // Reset counter after reaching 2 presses
        }
    }
}
