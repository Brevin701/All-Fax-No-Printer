using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Button button;

    private const int CTRL_KEY = 0x11;
    private const int P_KEY = 0x50;

    [DllImport("user32.dll")]
    static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

    public void OnClick()
    {
        keybd_event(CTRL_KEY, 0, 0, 0); // Simulate Ctrl key down
        keybd_event(P_KEY, 0, 0, 0); // Simulate P key down
        keybd_event(P_KEY, 0, 2, 0); // Simulate P key up
        keybd_event(CTRL_KEY, 0, 2, 0); // Simulate Ctrl key up

        // This line actually exits play mode (optional)
        // EditorApplication.isPlaying = false;
    }
}
