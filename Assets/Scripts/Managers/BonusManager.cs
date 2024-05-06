using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusManager : MonoBehaviour
{

    #region Player prefs keys
    private const string MAXCARDSKEY = "maxCards";
    private const string MAXCARDSEXPANDPRICEKEY = "maxCardsExpandPrice";

    private const string BALLDAMAGECARDLEVELKEY = "ballDamageCardLevel";
    private const string BALLSPEEDCARDLEVELKEY = "ballSpeedCardLevel";
    private const string CRITDAMAGECARDLEVELKEY = "critDamageCardLevel";
    private const string CASHCARDLEVELKEY = "cashCardLevel";
    private const string CASHBRICKCARDLEVELKEY = "cashBrickCardLevel";
    private const string CASHBRICKCHANCECARDLEVELKEY = "cashBrickChanceCardLevel";
    private const string RADIUSCARDLEVELKEY = "radiusCardLevel";
    private const string STAGECASHCARDLEVELKEY = "stageCashCardLevel";
    private const string SPEEDBUFFCARDLEVELKEY = "speedBuffCardLevel";
    private const string DAMAGEBUFFCARDLEVELKEY = "damageBuffCardLevel";
    private const string CASHMULTBUFFCARDLEVELKEY = "cashmultBuffCardLevel";
    private const string PASSIVEINCOMEMULCARDLEVELKEY = "passiveIncomeCardLevel";

    private const string BALLDAMAGECARDCOUNTKEY = "ballDamageCardCount";
    private const string BALLSPEEDCARDCOUNTKEY = "ballSpeedCardCount";
    private const string CRITDAMAGECARDCOUNTKEY = "critDamageCardCount";
    private const string CASHCARDCOUNTKEY = "cashCardCount";
    private const string CASHBRICKCARDCOUNTKEY = "cashBrickCardCount";
    private const string CASHBRICKCHANCECARDCOUNTKEY = "cashBrickChanceCardCount";
    private const string RADIUSCARDCOUNTKEY = "radiusCardCount";
    private const string STAGECASHCARDCOUNTKEY = "stageCashCardCount";
    private const string SPEEDBUFFCARDCOUNTKEY = "speedBuffCardCount";
    private const string DAMAGEBUFFCARDCOUNTKEY = "damageBuffCardCount";
    private const string CASHMULTBUFFCARDCOUNTKEY = "cashmultBuffCardCount";
    private const string PASSIVEINCOMEMULCARDCOUNTKEY = "passiveIncomeCardCount";

    private const string BALLDAMAGECARDACTIVEKEY = "ballDamageCardActive";
    private const string BALLSPEEDCARDACTIVEKEY = "ballSpeedCardActive";
    private const string CRITDAMAGECARDACTIVEKEY = "critDamageCardActive";
    private const string CASHCARDACTIVEKEY = "cashCardActive";
    private const string CASHBRICKCARDACTIVEKEY = "cashBrickCardActive";
    private const string CASHBRICKCHANCECARDACTIVEKEY = "cashBrickChanceCardActive";
    private const string RADIUSCARDACTIVEKEY = "radiusCardActive";
    private const string STAGECASHCARDACTIVEKEY = "stageCashCardActive";
    private const string SPEEDBUFFCARDACTIVEKEY = "speedBuffCardActive";
    private const string DAMAGEBUFFCARDACTIVEKEY = "damageBuffCardActive";
    private const string CASHMULTBUFFCARDACTIVEKEY = "cashmultBuffCardActive";
    private const string PASSIVEINCOMEMULCARDACTIVEKEY = "passiveIncomeCardActive";
    #endregion

    private CashManager cashManager;

    //обновляет уже активные бонусы
    public delegate void UpdateCard(CardType type, BonusStats<float> value);
    public UpdateCard updateCard;

    //обновляет информацию о карте в UI
    public delegate void UpdateCardInfo(CardType type, Card card);
    public UpdateCardInfo updateCardInfo;

    //обновляет информацию о количестве карт
    public delegate void UpdateIntegerInfo(int count);
    public UpdateIntegerInfo updateActiveCardsCount;
    public UpdateIntegerInfo updateMaxCardsCount;

    private Dictionary<CardType, Card> cards = new Dictionary<CardType, Card>();

    private List<CardType> activeCards = new List<CardType>();
    private int maxCards = 1;
    private const int totalMaxCards = 12;

    private const int cardPrice = 20;

    private int maxCardsExpandPrice = 100;

    private void IncreaseMaxCardsExpandPrice()
    {
        maxCardsExpandPrice += 100;
        new SaveLoadData<int>(MAXCARDSEXPANDPRICEKEY, maxCardsExpandPrice).SaveData();
    }

    public int ActiveCardsCount() {return activeCards.Count;}
    
    public int MaxCardsCount(){ return maxCards;}

    //увеличивает максимальное количество используемых карт
    public void ExpandMaxCards()
    {
        if(maxCards + 1 < totalMaxCards)
        {
            if (cashManager.SpendHardCash(maxCardsExpandPrice))
            {
                maxCards++;
                new SaveLoadData<int>(MAXCARDSKEY, maxCards).SaveData();
                IncreaseMaxCardsExpandPrice();
            }
        }
        
        updateMaxCardsCount?.Invoke(maxCards);
    }

    //получает карту в зависимости от типа
    public Card GetCurrentCard(CardType type)
    {
        if(cards.ContainsKey(type)) return cards[type];
        return null;
    }


    private System.Random rand = new System.Random();
    //покупка карты, возвращает тип карты, которую открыл
    public CardType OpenNewCard()
    {
        var allTypes = Enum.GetValues(typeof(CardType));
        CardType type = CardType.NONE;

        if (cashManager.SpendHardCash(cardPrice))
        {
            do
            {
                type = (CardType)allTypes.GetValue(rand.Next(allTypes.Length));
            }while(type == CardType.NONE);

            if (cards.ContainsKey(type))
            {
                cards[type].AddCount(1);
            }
            else
            {
                AddNewCard(type, 1);
            }

            updateCardInfo?.Invoke(type, cards[type]);
        }

        return type;
    }

    private void AddNewCard(CardType type, int level)
    {
        switch (type)
        {
            case CardType.BALLDAMAGE:
                cards.Add(CardType.BALLDAMAGE, new DamageCard(level, 0));
                new SaveLoadData<int>(BALLDAMAGECARDLEVELKEY, level).SaveData();
                new SaveLoadData<int>(BALLDAMAGECARDCOUNTKEY, 0).SaveData();
                break;

            case CardType.BALLSPEED:
                cards.Add(CardType.BALLSPEED, new SpeedCard(level, 0));
                new SaveLoadData<int>(BALLSPEEDCARDLEVELKEY, level).SaveData();
                new SaveLoadData<int>(BALLSPEEDCARDCOUNTKEY, 0).SaveData();
                break;

            case CardType.CRITDAMAGE:
                cards.Add(CardType.CRITDAMAGE, new CritDamageCard(level, 0));
                new SaveLoadData<int>(CRITDAMAGECARDLEVELKEY, level).SaveData();
                new SaveLoadData<int>(CRITDAMAGECARDCOUNTKEY, 0).SaveData();
                break;

            case CardType.CASH:
                cards.Add(CardType.CASH, new CashCard(level, 0));
                new SaveLoadData<int>(CASHCARDLEVELKEY, level).SaveData();
                new SaveLoadData<int>(CASHCARDCOUNTKEY, 0).SaveData();
                break;

            case CardType.CASHBRICK:
                cards.Add(CardType.CASHBRICK, new CashBrickCard(level, 0));
                new SaveLoadData<int>(CASHBRICKCARDLEVELKEY, level).SaveData();
                new SaveLoadData<int>(CASHBRICKCARDCOUNTKEY, 0).SaveData();
                break;

            case CardType.CASHBRICKCHANCE:
                cards.Add(CardType.CASHBRICKCHANCE, new CashBrickChanceCard(level, 0));
                new SaveLoadData<int>(CASHBRICKCHANCECARDLEVELKEY, level).SaveData();
                new SaveLoadData<int>(CASHBRICKCHANCECARDCOUNTKEY, 0).SaveData();
                break;

            case CardType.RADIUS:
                cards.Add(CardType.RADIUS, new RadiusCard(level, 0));
                new SaveLoadData<int>(RADIUSCARDLEVELKEY, level).SaveData();
                new SaveLoadData<int>(RADIUSCARDCOUNTKEY, 0).SaveData();
                break;

            case CardType.STAGECASH:
                cards.Add(CardType.STAGECASH, new StageCashCard(level, 0));
                new SaveLoadData<int>(STAGECASHCARDLEVELKEY, level).SaveData();
                new SaveLoadData<int>(STAGECASHCARDCOUNTKEY, 0).SaveData();
                break;

            case CardType.SPEEDBUFF:
                cards.Add(CardType.SPEEDBUFF, new SpeedBuffCard(level, 0));
                new SaveLoadData<int>(SPEEDBUFFCARDLEVELKEY, level).SaveData();
                new SaveLoadData<int>(SPEEDBUFFCARDCOUNTKEY, 0).SaveData();
                break;

            case CardType.DAMAGEBUFF:
                cards.Add(CardType.DAMAGEBUFF, new DamageBuffCard(level, 0));
                new SaveLoadData<int>(DAMAGEBUFFCARDLEVELKEY, level).SaveData();
                new SaveLoadData<int>(DAMAGEBUFFCARDCOUNTKEY, 0).SaveData();
                break;

            case CardType.CASHMULTBUFF:
                cards.Add(CardType.CASHMULTBUFF, new CashmultBuffCard(level, 0));
                new SaveLoadData<int>(CASHMULTBUFFCARDLEVELKEY, level).SaveData();
                new SaveLoadData<int>(CASHMULTBUFFCARDCOUNTKEY, 0).SaveData();
                break;

            case CardType.PASSIVEINCOMEMUL:
                cards.Add(CardType.PASSIVEINCOMEMUL, new PassiveIncomeMulCard(level, 0));
                new SaveLoadData<int>(PASSIVEINCOMEMULCARDLEVELKEY, level).SaveData();
                new SaveLoadData<int>(PASSIVEINCOMEMULCARDCOUNTKEY, 0).SaveData();
                break;
        }
    }

    public bool ActivateCard(CardType type)
    {
        if(activeCards.Count < maxCards)
        {
            if (!activeCards.Contains(type))
            {
                AddCardToActivePool(type);

                switch (type)
                {
                    case CardType.BALLDAMAGE:
                        new SaveLoadData<int>(BALLDAMAGECARDACTIVEKEY, 1).SaveData();
                        break;
                    case CardType.BALLSPEED:
                        new SaveLoadData<int>(BALLSPEEDCARDACTIVEKEY, 1).SaveData();
                        break;
                    case CardType.CRITDAMAGE:
                        new SaveLoadData<int>(CRITDAMAGECARDACTIVEKEY, 1).SaveData();
                        break;
                    case CardType.CASH:
                        new SaveLoadData<int>(CASHCARDACTIVEKEY, 1).SaveData();
                        break;
                    case CardType.CASHBRICK:
                        new SaveLoadData<int>(CASHBRICKCARDACTIVEKEY, 1).SaveData();
                        break;
                    case CardType.CASHBRICKCHANCE:
                        new SaveLoadData<int>(CASHBRICKCHANCECARDACTIVEKEY, 1).SaveData();
                        break;
                    case CardType.RADIUS:
                        new SaveLoadData<int>(RADIUSCARDACTIVEKEY, 1).SaveData();
                        break;
                    case CardType.STAGECASH:
                        new SaveLoadData<int>(STAGECASHCARDACTIVEKEY, 1).SaveData();
                        break;
                    case CardType.SPEEDBUFF:
                        new SaveLoadData<int>(SPEEDBUFFCARDACTIVEKEY, 1).SaveData();
                        break;
                    case CardType.DAMAGEBUFF:
                        new SaveLoadData<int>(DAMAGEBUFFCARDACTIVEKEY, 1).SaveData();
                        break;
                    case CardType.CASHMULTBUFF:
                        new SaveLoadData<int>(CASHMULTBUFFCARDACTIVEKEY, 1).SaveData();
                        break;
                    case CardType.PASSIVEINCOMEMUL:
                        new SaveLoadData<int>(PASSIVEINCOMEMULCARDACTIVEKEY, 1).SaveData();
                        break;
                }

                return true;
            }
            return false;
        }
        return false;
    }

    private void AddCardToActivePool(CardType type)
    {
        activeCards.Add(type);
        updateCard?.Invoke(type, new BonusStats<float>(true, cards[type].value));
        updateActiveCardsCount?.Invoke(activeCards.Count);
    }


    public void DeactivateCard(CardType type)
    {
        activeCards.Remove(type);
        updateCard?.Invoke(type, new BonusStats<float>(false, cards[type].value));
        updateActiveCardsCount?.Invoke(activeCards.Count);

        switch(type)
        {
            case CardType.BALLDAMAGE:
                new SaveLoadData<int>(BALLDAMAGECARDACTIVEKEY).RemoveData();
                break;
            case CardType.BALLSPEED:
                new SaveLoadData<int>(BALLSPEEDCARDACTIVEKEY).RemoveData();
                break;
            case CardType.CRITDAMAGE:
                new SaveLoadData<int>(CRITDAMAGECARDACTIVEKEY).RemoveData();
                break;
            case CardType.CASH:
                new SaveLoadData<int>(CASHCARDACTIVEKEY).RemoveData();
                break;
            case CardType.CASHBRICK:
                new SaveLoadData<int>(CASHBRICKCARDACTIVEKEY).RemoveData();
                break;
            case CardType.CASHBRICKCHANCE:
                new SaveLoadData<int>(CASHBRICKCHANCECARDACTIVEKEY).RemoveData();
                break;
            case CardType.RADIUS:
                new SaveLoadData<int>(RADIUSCARDACTIVEKEY).RemoveData();
                break;
            case CardType.STAGECASH:
                new SaveLoadData<int>(STAGECASHCARDACTIVEKEY).RemoveData();
                break;
            case CardType.SPEEDBUFF:
                new SaveLoadData<int>(SPEEDBUFFCARDACTIVEKEY).RemoveData();
                break;
            case CardType.DAMAGEBUFF:
                new SaveLoadData<int>(DAMAGEBUFFCARDACTIVEKEY).RemoveData();
                break;
            case CardType.CASHMULTBUFF:
                new SaveLoadData<int>(CASHMULTBUFFCARDACTIVEKEY).RemoveData();
                break;
            case CardType.PASSIVEINCOMEMUL:
                new SaveLoadData<int>(PASSIVEINCOMEMULCARDACTIVEKEY).RemoveData();
                break;
        }
    }


    public void AddCountToCard(CardType type, int count)
    {
        float oldValue = cards[type].value;
        int oldLevel = cards[type].level;

        if (cards[type].AddCount(count))
        {
            if (activeCards.Contains(type))
            {
                updateCard?.Invoke(type, new BonusStats<float>(false, oldValue));
                updateCard?.Invoke(type, new BonusStats<float>(true, cards[type].value));
            }

            switch (type)
            {
                case CardType.BALLDAMAGE:
                    new SaveLoadData<int>(BALLDAMAGECARDCOUNTKEY, cards[type].count).SaveData();
                    if(oldLevel < cards[type].level)
                    {
                        new SaveLoadData<int>(BALLDAMAGECARDLEVELKEY, cards[type].level).SaveData();
                    }
                    break;

                case CardType.BALLSPEED:
                    new SaveLoadData<int>(BALLSPEEDCARDCOUNTKEY, cards[type].count).SaveData();
                    if (oldLevel < cards[type].level)
                    {
                        new SaveLoadData<int>(BALLSPEEDCARDLEVELKEY, cards[type].level).SaveData();
                    }
                    break;

                case CardType.CRITDAMAGE:
                    new SaveLoadData<int>(CRITDAMAGECARDCOUNTKEY, cards[type].count).SaveData();
                    if (oldLevel < cards[type].level)
                    {
                        new SaveLoadData<int>(CRITDAMAGECARDLEVELKEY, cards[type].level).SaveData();
                    }
                    break;

                case CardType.CASH:
                    new SaveLoadData<int>(CASHCARDCOUNTKEY, cards[type].count).SaveData();
                    if (oldLevel < cards[type].level)
                    {
                        new SaveLoadData<int>(CASHCARDLEVELKEY, cards[type].level).SaveData();
                    }
                    break;

                case CardType.CASHBRICK:
                    new SaveLoadData<int>(CASHBRICKCARDCOUNTKEY, cards[type].count).SaveData();
                    if (oldLevel < cards[type].level)
                    {
                        new SaveLoadData<int>(CASHBRICKCARDLEVELKEY, cards[type].level).SaveData();
                    }
                    break;

                case CardType.CASHBRICKCHANCE:
                    new SaveLoadData<int>(CASHBRICKCHANCECARDCOUNTKEY, cards[type].count).SaveData();
                    if (oldLevel < cards[type].level)
                    {
                        new SaveLoadData<int>(CASHBRICKCHANCECARDLEVELKEY, cards[type].level).SaveData();
                    }
                    break;
            
                case CardType.RADIUS:
                    new SaveLoadData<int>(RADIUSCARDCOUNTKEY, cards[type].count).SaveData();
                    if (oldLevel < cards[type].level)
                    {
                        new SaveLoadData<int>(RADIUSCARDLEVELKEY, cards[type].level).SaveData();
                    }
                    break;

                case CardType.STAGECASH:
                    new SaveLoadData<int>(STAGECASHCARDCOUNTKEY, cards[type].count).SaveData();
                    if (oldLevel < cards[type].level)
                    {
                        new SaveLoadData<int>(STAGECASHCARDLEVELKEY, cards[type].level).SaveData();
                    }
                    break;
            
                case CardType.SPEEDBUFF:
                    new SaveLoadData<int>(SPEEDBUFFCARDCOUNTKEY, cards[type].count).SaveData();
                    if (oldLevel < cards[type].level)
                    {
                        new SaveLoadData<int>(SPEEDBUFFCARDLEVELKEY, cards[type].level).SaveData();
                    }
                    break;
            
                case CardType.DAMAGEBUFF:
                    new SaveLoadData<int>(DAMAGEBUFFCARDCOUNTKEY, cards[type].count).SaveData();
                    if (oldLevel < cards[type].level)
                    {
                        new SaveLoadData<int>(DAMAGEBUFFCARDLEVELKEY, cards[type].level).SaveData();
                    }
                    break;
            
                case CardType.CASHMULTBUFF:
                    new SaveLoadData<int>(CASHMULTBUFFCARDCOUNTKEY, cards[type].count).SaveData();
                    if (oldLevel < cards[type].level)
                    {
                        new SaveLoadData<int>(CASHMULTBUFFCARDLEVELKEY, cards[type].level).SaveData();
                    }
                    break;

                case CardType.PASSIVEINCOMEMUL:
                    new SaveLoadData<int>(PASSIVEINCOMEMULCARDCOUNTKEY, cards[type].count).SaveData();
                    if (oldLevel < cards[type].level)
                    {
                        new SaveLoadData<int>(PASSIVEINCOMEMULCARDLEVELKEY, cards[type].level).SaveData();
                    }
                    break;
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
        LoadData();
        //OpenNewCard();
    }

    private void LoadData()
    {
        if (PlayerPrefs.HasKey(MAXCARDSEXPANDPRICEKEY))
        {
            maxCardsExpandPrice = new SaveLoadData<int>(MAXCARDSEXPANDPRICEKEY).LoadData();
        }

        if (PlayerPrefs.HasKey(MAXCARDSKEY))
        {
            maxCards = new SaveLoadData<int>(MAXCARDSKEY).LoadData();
        }

        if (PlayerPrefs.HasKey(BALLSPEEDCARDCOUNTKEY))
        {
            cards.Add(CardType.BALLSPEED, new SpeedCard(new SaveLoadData<int>(BALLSPEEDCARDLEVELKEY).LoadData(), 
                new SaveLoadData<int>(BALLSPEEDCARDCOUNTKEY).LoadData()));
        }

        if (PlayerPrefs.HasKey(BALLDAMAGECARDCOUNTKEY))
        {
            cards.Add(CardType.BALLSPEED, new DamageCard(new SaveLoadData<int>(BALLDAMAGECARDLEVELKEY).LoadData(),
                new SaveLoadData<int>(BALLDAMAGECARDCOUNTKEY).LoadData()));
        }

        if (PlayerPrefs.HasKey(CRITDAMAGECARDCOUNTKEY))
        {
            cards.Add(CardType.CRITDAMAGE, new CritDamageCard(new SaveLoadData<int>(CRITDAMAGECARDLEVELKEY).LoadData(),
                new SaveLoadData<int>(CRITDAMAGECARDCOUNTKEY).LoadData()));
        }

        if (PlayerPrefs.HasKey(CASHCARDCOUNTKEY))
        {
            cards.Add(CardType.CASH, new CashCard(new SaveLoadData<int>(CASHCARDLEVELKEY).LoadData(),
                new SaveLoadData<int>(CASHCARDCOUNTKEY).LoadData()));
        }

        if (PlayerPrefs.HasKey(CASHBRICKCARDCOUNTKEY))
        {
            cards.Add(CardType.CASHBRICK, new CashBrickCard(new SaveLoadData<int>(CASHBRICKCARDLEVELKEY).LoadData(),
                new SaveLoadData<int>(CASHBRICKCARDCOUNTKEY).LoadData()));
        }

        if (PlayerPrefs.HasKey(CASHBRICKCHANCECARDCOUNTKEY))
        {
            cards.Add(CardType.CASHBRICKCHANCE, new CashBrickChanceCard(new SaveLoadData<int>(CASHBRICKCHANCECARDLEVELKEY).LoadData(),
                new SaveLoadData<int>(CASHBRICKCHANCECARDCOUNTKEY).LoadData()));
        }

        if (PlayerPrefs.HasKey(RADIUSCARDCOUNTKEY))
        {
            cards.Add(CardType.RADIUS, new RadiusCard(new SaveLoadData<int>(RADIUSCARDLEVELKEY).LoadData(),
                new SaveLoadData<int>(RADIUSCARDCOUNTKEY).LoadData()));
        }

        if (PlayerPrefs.HasKey(STAGECASHCARDCOUNTKEY))
        {
            cards.Add(CardType.STAGECASH, new StageCashCard(new SaveLoadData<int>(STAGECASHCARDLEVELKEY).LoadData(),
                new SaveLoadData<int>(STAGECASHCARDCOUNTKEY).LoadData()));
        }

        if (PlayerPrefs.HasKey(SPEEDBUFFCARDCOUNTKEY))
        {
            cards.Add(CardType.SPEEDBUFF, new SpeedBuffCard(new SaveLoadData<int>(SPEEDBUFFCARDLEVELKEY).LoadData(),
                new SaveLoadData<int>(SPEEDBUFFCARDCOUNTKEY).LoadData()));
        }

        if (PlayerPrefs.HasKey(DAMAGEBUFFCARDCOUNTKEY))
        {
            cards.Add(CardType.DAMAGEBUFF, new DamageBuffCard(new SaveLoadData<int>(DAMAGEBUFFCARDLEVELKEY).LoadData(),
                new SaveLoadData<int>(DAMAGEBUFFCARDCOUNTKEY).LoadData()));
        }

        if (PlayerPrefs.HasKey(CASHMULTBUFFCARDCOUNTKEY))
        {
            cards.Add(CardType.CASHMULTBUFF, new CashmultBuffCard(new SaveLoadData<int>(CASHMULTBUFFCARDLEVELKEY).LoadData(),
                new SaveLoadData<int>(CASHMULTBUFFCARDCOUNTKEY).LoadData()));
        }

        if (PlayerPrefs.HasKey(PASSIVEINCOMEMULCARDCOUNTKEY))
        {
            cards.Add(CardType.PASSIVEINCOMEMUL, new PassiveIncomeMulCard(new SaveLoadData<int>(PASSIVEINCOMEMULCARDLEVELKEY).LoadData(),
                new SaveLoadData<int>(PASSIVEINCOMEMULCARDCOUNTKEY).LoadData()));
        }


        if(PlayerPrefs.HasKey(BALLDAMAGECARDACTIVEKEY))
        {
            AddCardToActivePool(CardType.BALLDAMAGE);
        }
        if (PlayerPrefs.HasKey(BALLSPEEDCARDACTIVEKEY))
        {
            AddCardToActivePool(CardType.BALLSPEED);
        }
        if (PlayerPrefs.HasKey(CRITDAMAGECARDACTIVEKEY))
        {
            AddCardToActivePool(CardType.CRITDAMAGE);
        }
        if (PlayerPrefs.HasKey(CASHCARDACTIVEKEY))
        {
            AddCardToActivePool(CardType.CASH);
        }
        if (PlayerPrefs.HasKey(CASHBRICKCARDACTIVEKEY))
        {
            AddCardToActivePool(CardType.CASHBRICK);
        }
        if (PlayerPrefs.HasKey(CASHBRICKCHANCECARDACTIVEKEY))
        {
            AddCardToActivePool(CardType.CASHBRICKCHANCE);
        }
        if (PlayerPrefs.HasKey(RADIUSCARDACTIVEKEY))
        {
            AddCardToActivePool(CardType.RADIUS);
        }
        if (PlayerPrefs.HasKey(STAGECASHCARDACTIVEKEY))
        {
            AddCardToActivePool(CardType.STAGECASH);
        }
        if (PlayerPrefs.HasKey(SPEEDBUFFCARDACTIVEKEY))
        {
            AddCardToActivePool(CardType.SPEEDBUFF);
        }
        if (PlayerPrefs.HasKey(DAMAGEBUFFCARDACTIVEKEY))
        {
            AddCardToActivePool(CardType.DAMAGEBUFF);
        }
        if (PlayerPrefs.HasKey(CASHMULTBUFFCARDACTIVEKEY))
        {
            AddCardToActivePool(CardType.CASHMULTBUFF);
        }
        if (PlayerPrefs.HasKey(PASSIVEINCOMEMULCARDACTIVEKEY))
        {
            AddCardToActivePool(CardType.PASSIVEINCOMEMUL);
        }
    }
}


