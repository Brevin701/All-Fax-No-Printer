using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FastForward : MonoBehaviour
{
    private int pressCount = 0;
    private TMP_Text fastForward;

    public void OnClick()
    {
        pressCount++;

        if (pressCount == 1)
        {
            Time.timeScale = 2.0f;
            fastForward.text = (pressCount + "x").ToString();
        }
        else if (pressCount == 2)
        {
            Time.timeScale = 3.0f;
        }
        else if (pressCount == 4)
        {
            Time.timeScale = 5.0f;
        }
        else if (pressCount == 5)
        {
            Time.timeScale = 1.0f;
            pressCount = 0; // Reset counter after reaching 2 presses
        }
    }
}
