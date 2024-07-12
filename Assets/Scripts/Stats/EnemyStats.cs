using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    private Enemy enemy;
    public Stat MoneyDropAmount;

    [Header("Level Info")]
    public int level = 1;

    [Range(0f, 1f)]
    [SerializeField] private float percentageModifier = .4f;

    protected override void Start()
    {        
        ApplyLevelModifier();

        base.Start();

        enemy = GetComponent<Enemy>();
    }
    private void ApplyLevelModifier()
    {       
        Modify(damage);        
        Modify(maxHealth);
        Modify(armor);        
    }

    private void Modify(Stat _stat)
    {
        for (int i = 0; i < level; i++)
        {
            float modifier = _stat.GetValue() * percentageModifier;

            _stat.AddModifier(Mathf.RoundToInt(modifier));
        }
    }

    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);
    }

    protected override void Die()
    {
        base.Die();
        enemy.Die();
        PlayerManager.instance.currency += MoneyDropAmount.GetValue();
        Destroy(gameObject, 3f);
    }
}