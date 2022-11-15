using UnityEngine;
using System;

public class Boss : MonoBehaviour
{
    public BossData data;
    public BossUI bossUI;


    private int currentHP;
    private Range<int> damageRange;
    public BossData Data => data;
    public Action OnDestroy;
    public Action<int> OnGetDamage;

    private Player currentEnemy;

    public int CurrentHP => currentHP;

    private void Awake()
    {
        bossUI.gameObject.SetActive(false);
    }

    public void StartFight(Player player)
    {
        currentHP = data.hp;
        damageRange = data.damageRange;
        currentEnemy = player;

        bossUI.gameObject.SetActive(true);
        bossUI.Initialize(this);
    }

    public void GetDamage(int damage)
    {
        currentHP -= damage;

        if(currentHP <= 0)
        {
            OnDestroy?.Invoke();
        }

        OnGetDamage?.Invoke(damage);
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
