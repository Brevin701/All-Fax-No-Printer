using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperPlot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    public GameObject towerObj;
    public Sniper sniper;
    private Color startColor;

    private void Start()
    {
        startColor = sr.color;
    }
    private void OnMouseEnter()
    {
        sr.color = hoverColor;
    }

    private void OnMouseExit()
    {
        sr.color = startColor;
    }

    private void OnMouseDown()
    {
        if (UIManager.main.IsHoveringUI()) return;
        if (towerObj == null)
        {


            Tower towerToBuild = BuildManager.main.GetSelectedTower();


            if (towerToBuild.cost > LevelManager.main.currency)
            {
                Debug.Log("OOPS");
                return;
            }

            LevelManager.main.SpendCurrency(towerToBuild.cost);
            towerObj = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
            sniper = towerObj.GetComponent<Sniper>();
        }
        else
        {
            sniper.OpenUpgradeUI();
            return;
        }
    }
}
