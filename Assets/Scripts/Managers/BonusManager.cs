using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusManager : MonoBehaviour
{

    private CashManager cashManager;

    public delegate void UpdateCard(CardType type, BonusStats<float> value);
    public UpdateCard updateCard;

    private Dictionary<CardType, Card> cards = new Dictionary<CardType, Card>();

    private List<CardType> activeCards = new List<CardType>();
    private int maxCards = 1;

    private int maxCardsExpandPrice = 100;

    private void IncreaseMaxCardsExpandPrice()
    {
        maxCardsExpandPrice += 100;
    }

    public int ActiveCardsCount() { 
       return activeCards.Count;
    }
    
    public int MaxCardsCount()
    {
        return maxCards;
    }

    public int ExpandMaxCards()
    {
        if (cashManager.SpendHardCash(maxCardsExpandPrice))
        {
            maxCards++;
            IncreaseMaxCardsExpandPrice();
        }

        return maxCards;
    }

    private System.Random rand = new System.Random();
    public CardType OpenNewCard()
    {
        var allTypes = Enum.GetValues(typeof(CardType));
        CardType type;
        do
        {
            type = (CardType)allTypes.GetValue(rand.Next(allTypes.Length));
        } while (cards.ContainsKey(type));
        Debug.Log(type);
        AddNewCard(type);

        return type;
    }

    private void AddNewCard(CardType type)
    {
        switch (type)
        {
            case CardType.BALLDAMAGE:
                cards.Add(CardType.BALLDAMAGE, new DamageCard(1));
                break;

            case CardType.BALLSPEED:
                cards.Add(CardType.BALLSPEED, new SpeedCard(1));
                break;

            case CardType.CRITDAMAGE:
                cards.Add(CardType.CRITDAMAGE, new CritDamageCard(1));
                break;

            case CardType.CASH:
                cards.Add(CardType.CASH, new CashCard(1));
                break;

            case CardType.CASHBRICK:
                cards.Add(CardType.CASHBRICK, new CashBrickCard(1));
                break;

            case CardType.CASHBRICKCHANCE:
                cards.Add(CardType.CASHBRICKCHANCE, new CashBrickChanceCard(1));
                break;

            case CardType.RADIUS:
                cards.Add(CardType.RADIUS, new RadiusCard(1));
                break;

            case CardType.STAGECASH:
                cards.Add(CardType.STAGECASH, new StageCashCard(1));
                break;
        }
    }

    public void ActivateCard(CardType type)
    {
        if(activeCards.Count < maxCards)
        {
            if (!activeCards.Contains(type))
            {
                activeCards.Add(type);
                BonusStats<float> bs = new BonusStats<float>(true, cards[type].value);
                updateCard?.Invoke(type, bs);
            }
        }   
    }

    public void DeactivateCard(CardType type)
    {
        activeCards.Remove(type);
        BonusStats<float> bs = new BonusStats<float>(false, cards[type].value);
        updateCard?.Invoke(type, bs);
    }


    public void AddCountToCard(CardType type, int count)
    {
        float oldValue = cards[type].value;

        if (cards[type].AddCount(count))
        {
            if (activeCards.Contains(type))
            {
                BonusStats<float> bs = new BonusStats<float>(true, cards[type].value - oldValue);
                updateCard?.Invoke(type, bs);
            }
        }
        
        
    }

    public BonusStats<float> GetCardValue(CardType type)
    {
        if (activeCards.Contains(type))
        {
            return new BonusStats<float>(true, cards[type].value);
        }

        return new BonusStats<float>(false, 0f);
    }

    void Start()
    {
        cashManager = GetComponent<CashManager>();
        //OpenNewCard();
    }
}


