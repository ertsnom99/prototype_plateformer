﻿using UnityEngine;

public class BossFight : MonoBehaviour, IHealthSubscriber
{
    [Header("Boss")]
    [SerializeField]
    private Health m_bossHealth;

    private void Awake()
    {
        m_bossHealth.Subscribe(this);
    }

    // Methods of the IHealable interface
    public void NotifyDamageApplied(Health healthScript, int damage) { }

    public void NotifyHealthChange(Health healthScript, int health) { }

    public void NotifyHealthDepleted(Health healthScript)
    {
        GameManager.Instance.EndGame(true);
    }

    public void NotifyJustSubscribed(Health healthScript) { }
}
