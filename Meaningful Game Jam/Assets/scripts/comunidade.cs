using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class comunidade : MonoBehaviour
{
    public int              dogsAdopted = 0;
    private menus           menu;
    [SerializeField] int    adoptoToLvl1;
    [SerializeField] int    adoptoToLvl2;
    [SerializeField] int    adoptoToLvl3;
    [SerializeField] int    adoptoToLvl4;
    private int             level=0;
    [SerializeField] int    orcamento;
    [SerializeField] int    upOrcamento;
    [SerializeField] float  timeForPay;
    private float           timer;

    private void Start()
    {
        menu = GetComponent<menus>();
        timer = timeForPay;
    }

    private void Update()
    {
        if (dogsAdopted >= adoptoToLvl4 && level == 3)
        {
            level++;
            orcamento += upOrcamento;
            menu.Load("EndScene");
        }
        else if (dogsAdopted >= adoptoToLvl3 && level == 2)
        {
            level++;
            menu.playerInventario.medecinePrice += 5;
            orcamento += upOrcamento;
            menu.messageText.text = "Parabéns!\nGraças aos teus esforços o teu orçamento foi aumentado para " + orcamento + "e as fármacias  estão-te a dar descontos nos seus medicamentos, continua um bom trabalho. :)";
            menu.messageMenu.SetActive(true);
        }
        else if (dogsAdopted >= adoptoToLvl2 && level == 1)
        {
            level++;
            orcamento += upOrcamento;
            menu.messageText.text = "Parabéns!\nGraças aos teus esforços o teu orçamento foi aumentado para " + orcamento + ", continua um bom trabalho. :)";
            menu.messageMenu.SetActive(true);
        }
        else if (dogsAdopted >= adoptoToLvl1 && level==0)
        {
            level++;
            menu.playerInventario.deliciaPrice += 5;
            orcamento += upOrcamento;
            menu.messageText.text = "Parabéns!\nGraças aos teus esforços o teu orçamento foi aumentado para "+ orcamento+ " e o produtor de comida  canina local deu-te um desconto, continua um bom trabalho. :)";
            menu.messageMenu.SetActive(true);
        }
        menu.comunityHappines.value = (float)dogsAdopted / (float)adoptoToLvl4;

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            menu.playerInventario.ModifyValue(orcamento, 0);
            timer = timeForPay;
        }
    }
}
