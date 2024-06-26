using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Projeto.Core.Singleton;
using TMPro;

public class ItemManager : Singleton<ItemManager>
{
    public int coins;
    public TextMeshProUGUI textMoedas;

    private void Start()
    {
        Reset();
    }

    private void Reset()
    {
        coins = 0;
    }

    public void AddCoins(int amount = 1)
    {
        coins += amount;
        textMoedas.text = coins.ToString();
    }
}
