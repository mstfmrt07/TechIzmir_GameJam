using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Boss : MonoBehaviour, IRoundPlayer
{
    public BossData data;
    public BossUI bossUI;
    public int attackCooldown;

    private int currentHP;
    private int currentMana;

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
        yield return new WaitForSeconds(attackCooldown);
        while (SpendMana(1))
        {
            AttemptAttack();
            yield return new WaitForSeconds(attackCooldown);
        }

        //TODO Give turn to the player
        WarningMessage.Instance.Show("Sýra sende!");
        GiveTurn();
    }

    public void AttemptAttack()
    {
        var enemyCards = currentEnemy.CardsOnTable;

        if (enemyCards.Count == 0)
        {
            Attack(currentEnemy, null);
            return;
        }

        Card cardToAttack = enemyCards[0];
        List<Card> attackCards = new List<Card>();
        List<Card> defenseCards = new List<Card>();

        //TODO Implement boss attack
        foreach (Card card in enemyCards)
        {
            if (card.Data.type == CardType.Attack || card.Data.type == CardType.Spell)
                attackCards.Add(card);
            else
                defenseCards.Add(card);
        }

        int maxAttack = -1;
        foreach (var attackCard in attackCards)
        {
            if (attackCard.Data.damage >= maxAttack)
            {
                cardToAttack = attackCard;
            }
        }

        if (cardToAttack == null)
        {
            int medianDamage = (data.damageRange.min + data.damageRange.max) / 2;
            int acceptableHP = 0;
            foreach (var defenseCard in defenseCards)
            {
                if (medianDamage >= defenseCard.CurrentHP && defenseCard.CurrentHP >= acceptableHP)
                {
                    acceptableHP = defenseCard.CurrentHP;
                    cardToAttack = defenseCard;
                }
            }
        }

        Attack(currentEnemy, cardToAttack);
    }

    public void Attack(Player player, Card card = null)
    {
        var dealtDamage = UnityEngine.Random.Range(data.damageRange.min, data.damageRange.max + 1);
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

    public void TakeTurn()
    {
        currentMana = data.mana;
        StartCoroutine(PerformAttackSequence());
        OnBossUpdated?.Invoke();
    }

    public void GiveTurn()
    {
        RoundManager.Instance.GiveTurn(currentEnemy);
        OnBossUpdated?.Invoke();
    }

    public void StartFight(IRoundPlayer enemy)
    {
        currentHP = data.hp;
        currentMana = data.mana;
        currentEnemy = (Player)enemy;

        bossUI.gameObject.SetActive(true);
        bossUI.Initialize(this);
        OnBossUpdated?.Invoke();
    }
}
