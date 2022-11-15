using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Boss : MonoBehaviour
{
    private BossData data;
    private int hp;
    private int damage;
    public BossData Data => data;
    public Action OnDestroy;

    public void InitializeBoss(BossData data)
    {
        this.data = data;
        this.hp = data.hp;
        this.damage = data.damage;
    }

    public void GetDamage(int damage)
    {
        hp -= damage;

        if(hp <= 0)
        {
            OnDestroy?.Invoke();
        }
    }

    public void Attack(Card card)
    {

    }
}
