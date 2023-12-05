using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;
using System.Dynamic;
using TMPro;


public class Sniper : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private TMP_Text upgradeAmount;
    [SerializeField] private TMP_Text levelCounter;



    [Header("Attribute")]
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float bps = .4f;
    [SerializeField] private int baseUpgradeCost = 150;
    [SerializeField] private int deletionCost;
    [SerializeField] private float maxLevel = 5f;


    private float bpsBase;
    private float targetingRangeBase;


    private Transform target;
    private float timeUntilFire;

    private int level = 1;
    private void Start()
    {
        bpsBase = bps;
        targetingRangeBase = targetingRange;
        upgradeButton.onClick.AddListener(Upgrade);

    }

    private void Update()
    {
        if (target == null)
        {
            FindTarget();
            return;
        }

        RotateTowardsTarget();

        if (!CheckTargetIsInRange() || target.position.x > 8.5)
        {
            target = null;
        }

        else
        {
            timeUntilFire += Time.deltaTime;

            if (timeUntilFire >= 1f / bps)
            {
                Shoot();
                timeUntilFire = 0f;
            }
        }
    }

    private void Shoot()
    {
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        bulletScript.SetTarget(target);
    }
    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)
        transform.position, 0f, enemyMask);

        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }

    private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    private void RotateTowardsTarget()
    {
        if (target.position.x <= 8.5)
        {
            float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;

            Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
            turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation,
            targetRotation, rotationSpeed * Time.deltaTime);
        }


    }

    public void OpenUpgradeUI()
    {
        upgradeUI.SetActive(true);

    }

    public void CloseUpgradeUI()
    {
        upgradeUI.SetActive(false);
        UIManager.main.SetHoveringState(false);
    }

    public void Upgrade()
    {
        if (CalculateCost() > LevelManager.main.currency) return;

        LevelManager.main.SpendCurrency(CalculateCost());

        level++;

        bps = CalculateBPS();

        targetingRange = CalculateRange();

        UpgradeCostCounter(baseUpgradeCost);

        LevelCounter(level);

        CloseUpgradeUI();
    }

    private int CalculateCost()
    {
        int cost = Mathf.RoundToInt(baseUpgradeCost * Mathf.Pow(level, .8f));
        return cost;
    }

    private float CalculateBPS()
    {
        return bpsBase * Mathf.Pow(level, .6f);
    }
    private float CalculateRange()
    {
        return targetingRangeBase * Mathf.Pow(level, .4f);
    }

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }

    public void UpgradeCostCounter(int baseUpgradeCost)
    {
        int upgradeCost = Mathf.RoundToInt(baseUpgradeCost * Mathf.Pow(level, .8f));

        upgradeAmount.text = "Upgrade: " + upgradeCost.ToString();



    }



    public void LevelCounter(int level)
    {

        if (level < maxLevel)
        {
            levelCounter.text = "Level: " + level.ToString();
        }
        else if (level == maxLevel)
        {
            levelCounter.text = "Max Level";
            upgradeButton.interactable = false;
        }

    }


}
