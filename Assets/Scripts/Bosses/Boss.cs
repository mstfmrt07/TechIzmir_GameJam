using UnityEngine;
using System;
using System.Collections;

public class Boss : MonoBehaviour
{
    public BossData data;
    public BossUI bossUI;
    public int attackCooldown;

    private int currentHP;
    private int currentMana;
    private Range<int> damageRange;
    private Player currentEnemy;

    public BossData Data => data;

    public Action OnDestroy;
    public Action OnBossUpdated;

    public int CurrentHP => currentHP;
    public int CurrentMana => currentMana;

    private void Awake()
    {
        bossUI.gameObject.SetActive(false);
    }

    public void StartFight(Player player)
    {
        currentHP = data.hp;
        currentMana = data.mana;
        damageRange = data.damageRange;
        currentEnemy = player;

        bossUI.gameObject.SetActive(true);
        bossUI.Initialize(this);
        OnBossUpdated?.Invoke();
    }

    public void GetDamage(int damage)
    {
        currentHP -= damage;

        if(currentHP <= 0)
        {
            OnDestroy?.Invoke();
        }

        OnBossUpdated?.Invoke();
    }

    public IEnumerator PerformAttackSequence()
    {
        //While it has enough mana to attack
        while(SpendMana(1))
        {
            AttemptAttack();
            yield return new WaitForSeconds(attackCooldown);
        }

        //TODO Give turn to the player
        WarningMessage.Instance.Show("Boss does not have enough mana!");
    }

    public void AttemptAttack()
    {
        var enemyCards = currentEnemy.CardsOnTable;
        Card cardToAttack = null;

        //TODO Implement boss attack
        foreach (Card card in enemyCards)
        {

        }

        Attack(currentEnemy, cardToAttack);
    }

    public void Attack(Player player, Card card = null)
    {
        var dealtDamage = UnityEngine.Random.Range(damageRange.min, damageRange.max + 1);
        player.GetDamage(card, dealtDamage);
        OnBossUpdated?.Invoke();
    }

    public bool SpendMana(int amount)
    {
        bool hasEnoughMana = (currentMana - amount >= 0);
        if (hasEnoughMana)
        {
            currentMana -= amount;
            OnBossUpdated?.Invoke();
        }

        return hasEnoughMana;
    }
}
