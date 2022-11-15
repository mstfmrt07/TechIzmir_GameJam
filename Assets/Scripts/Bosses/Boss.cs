using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Boss : MonoBehaviour
{
    public BossData data;
    private int hp;
    private Range<int> damageRange;
    public BossData Data => data;
    public Action OnDestroy;

    private Player currentEnemy;

    private void Awake()
    {
        StartFight(Player.Instance);
    }

    public void StartFight(Player player)
    {
        hp = data.hp;
        damageRange = data.damageRange;
        currentEnemy = player;
    }

    public void GetDamage(int damage)
    {
        hp -= damage;

        if(hp <= 0)
        {
            OnDestroy?.Invoke();
        }
    }

    public void AttemptAttack()
    {
        var enemyCards = currentEnemy.CardsOnTable;
        Card cardToAttack = null;

        foreach (Card card in enemyCards)
        {

        }

        Attack(currentEnemy, cardToAttack);
    }

    public void Attack(Player player, Card card = null)
    {
        var dealtDamage = UnityEngine.Random.Range(damageRange.min, damageRange.max + 1);
        player.GetDamage(card, dealtDamage);
    }
}
