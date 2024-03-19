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

    
    public void ExpandMaxCards()
    {
        if (cashManager.SpendHardCash(maxCardsExpandPrice))
        {
            maxCards++;
            IncreaseMaxCardsExpandPrice();
        }
    }

    private System.Random rand = new System.Random();
    public void OpenNewCard()
    {
        var allTypes = Enum.GetValues(typeof(CardType));
        CardType type;
        do
        {
            type = (CardType)allTypes.GetValue(rand.Next(allTypes.Length));
        } while (cards.ContainsKey(type));
        Debug.Log(type);
        AddNewCard(type);
    }

    private void AddNewCard(CardType type)
    {
        switch (type)
        {
            case CardType.BALLDAMAGE:
                cards.Add(CardType.BALLDAMAGE, new DamageBonus(1));
                break;

            case CardType.BALLSPEED:
                cards.Add(CardType.BALLSPEED, new SpeedBonus(1));
                break;

            case CardType.CRITDAMAGE:
                cards.Add(CardType.CRITDAMAGE, new CritDamageBonus(1));
                break;

            case CardType.CASH:
                cards.Add(CardType.CASH, new CashBonus(1));
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


