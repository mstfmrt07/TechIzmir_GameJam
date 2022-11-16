using System;
using UnityEngine;
using DG.Tweening;

public class Card : MonoBehaviour
{
    [Header("References")]
    public CardWorldUI cardWorldUI;
    public GameObject visuals;
    public Transform artifactBase;
    public float maxArtifactScale;

    private CardData data;
    private bool isPlayed;
    private int currentHP;

    public CardData Data => data;
    public int CurrentHP => currentHP;

    public Action OnDestroy;
    public Action OnCardPlayed;
    public Action<Card, int> OnGetDamage;

    public void InitializeCard(CardData data)
    {
        this.data = data;
        currentHP = data.armor;
        isPlayed = false;

        visuals.SetActive(false);
        cardWorldUI.Initialize(this);
    }

    public void Play()
    {
        if (isPlayed)
            return;

        var player = Player.Instance;

        //Add stat modifiers
        switch (data.type)
        {
            case CardType.Defense:
                //TODO do something with armor
                break;
            case CardType.Attack:
                player.Attack(data.damage);
                break;
            case CardType.Spell:
                //TODO do something with armor
                player.Attack(data.damage);
                break;
            default:
                break;
        }

        isPlayed = true;

        visuals.SetActive(true);
        var artifact = Instantiate(data.artifactPrefab, artifactBase);
        artifactBase.localScale = Vector3.zero;
        artifactBase.DOScale(maxArtifactScale, 1.2f).SetEase(Ease.OutBounce);

        OnCardPlayed?.Invoke();
    }

    public void DestroyCard()
    {
        //TODO Implement Destroy Card
    }

    public void GetDamage(int damage)
    {
        currentHP -= damage;
        OnGetDamage?.Invoke(this, damage);

        if (currentHP <= 0)
        {
            OnDestroy?.Invoke();
        }
    }
}
