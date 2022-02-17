using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inventario : MonoBehaviour
{
    [SerializeField] int    startingMoney =0;
    private int             money=0;
    private int             medecine=0;
    private int             delicia=0;
    private int             happyPet = 0;
    private int             medecinePrice = -20;
    private int             deliciaPrice = -10;
    private int             happyPetPrice = -5;
    public int              index;
    public Text[]           moneyDisplay;
    public Text[]           medecineDisplay;
    public Text[]           deliciaDisplay;
    public Text[]           happyPetDisplay;
    [SerializeField] menus  menu;

    private void Start()
    {
        money = startingMoney;
    }

    private void Update()
    {
        foreach(Text texto in moneyDisplay)
            texto.text = money.ToString() + "€";
        foreach (Text texto in medecineDisplay)
            texto.text = "X" + medecine.ToString();
        foreach (Text texto in deliciaDisplay)
            texto.text = "X" + delicia.ToString();
        foreach (Text texto in happyPetDisplay)
            texto.text = "X" + happyPet.ToString();
    }

    public bool ModifyValue(int amount, int index)
    {
        switch (index)
        {
            case 0:
                if (money + amount < 0)
                    return false;
                else
                {
                    money += amount;
                    return true;
                }
            case 1:
                if (medecine + amount < 0)
                    return false;
                else
                {
                    medecine += amount;
                    return true;
                }
            case 2:
                if (delicia + amount < 0)
                    return false;
                else
                {
                    delicia += amount;
                    return true;
                }
            case 3:
                if (happyPet + amount < 0)
                    return false;
                else
                {
                    happyPet += amount;
                    return true;
                }
        }
        return false;
    }
    public bool HasIndex(int index)
    {
        switch (index)
        {
            case 0:
                if (money<=0)
                    return false;
                else
                    return true;
            case 1:
                if (medecine <= 0)
                    return false;
                else
                    return true;
            case 2:
                if (delicia <= 0)
                    return false;
                else
                    return true;
            case 3:
                if (happyPet <= 0)
                    return false;
                else
                    return true;
        }
        return false;
    }
    public void BuyIndex()
    {
        switch (index)
        {
            case 1:
                Buy(medecinePrice, index);
                break;
            case 2:
                Buy(deliciaPrice, index);
                break;
            case 3:
                Buy(happyPetPrice, index);
                break;

        }
    }
    private void Buy(int price, int index)
    {
        if (!ModifyValue(price, 0))
        {
            menu.messageText.text = "Não tem dinheiro sufeciente.";
            menu.messageMenu.SetActive(true);
        } 
        else
            ModifyValue(1, index);
    }


}
