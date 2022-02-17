using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class venda : MonoBehaviour
{
    enum TypeOfSell {Farmacia, BancaLocal, SuperMercado }
    
    [SerializeField] menus          menu;
    [SerializeField] inventario     playerInventory;
    [SerializeField] TypeOfSell     seller;
    [SerializeField] string         sellerName;
    [SerializeField] Sprite         prodoct;
    private int                     index;

    private void Start()
    {
        switch (seller)
        {
            case TypeOfSell.Farmacia:
                index = 1;
                break;
            case TypeOfSell.BancaLocal:
                index = 2;
                break;
            case TypeOfSell.SuperMercado:
                index = 3;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            menu.SellMenu.SetActive(true);
            menu.sellerName.text = sellerName;
            menu.sellerProdoct.sprite = prodoct;
            playerInventory.index = index;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            menu.SellMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
