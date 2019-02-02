﻿using UnityEngine;

public class BossHealthBarEnabler : MonoBehaviour
{
    [Header("Boss")]
    [SerializeField]
    private GameObject m_healthBar;
    [SerializeField]
    private bool m_enable = true;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(GameManager.PlayerTag))
        {
            m_healthBar.SetActive(m_enable);
        }
    }
}
