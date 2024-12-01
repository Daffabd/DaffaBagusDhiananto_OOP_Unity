using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int Level;
    public EnemySpawner enemySpawner;
    [SerializeField] public CombatManager combatManager;

    protected virtual void Awake()
    {
        //
    }
    private void OnDestroy()
    {
        if (enemySpawner != null && combatManager != null)
        {
            enemySpawner.onDeath();
            combatManager.onDeath(this);

        }
    }

    void Start()
    {
        // Logika dasar untuk Enemy
    }
}
