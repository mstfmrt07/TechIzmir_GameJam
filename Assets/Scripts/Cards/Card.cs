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

    public Action<Card> OnDestroy;
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
                SoundManager.Instance.PlaySound(SoundManager.Instance.hit);
                break;
            case CardType.Spell:
                //TODO do something with armor
                player.Attack(data.damage);
                SoundManager.Instance.PlaySound(SoundManager.Instance.hit);
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
        OnDestroy?.Invoke(this);
        artifactBase.DOScale(maxArtifactScale, 1.2f).SetEase(Ease.OutBounce);
        this.Wait(0.5f, () => Destroy(gameObject));
    }

    public void GetDamage(int damage)
    {
        DOTween.Kill(GetInstanceID());
        currentHP -= damage;
        OnGetDamage?.Invoke(this, damage);
        artifactBase.transform.DOPunchScale(Vector3.one * 0.1f * maxArtifactScale, 0.5f, 10).SetId(GetInstanceID());

        if (currentHP <= 0)
        {
            this.Wait(0.5f, () => DestroyCard());
        }
    }
}
