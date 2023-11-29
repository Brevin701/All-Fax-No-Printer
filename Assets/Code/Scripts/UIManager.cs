using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text waveCounter;

    public static UIManager main;

    private bool isHoveringUI;


    public void UpdateWaveCounter(int currentWave)
    {
        waveCounter.gameObject.SetActive(true);
        waveCounter.text = "Wave: " + currentWave.ToString();
    }

    
    private void Awake()
    {
        main = this;
    }

    public void SetHoveringState(bool state)
    {
        isHoveringUI = state;
    }

    public bool IsHoveringUI()
    {
        return isHoveringUI;
    }
}
