using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dog : MonoBehaviour
{
    private static float decreValues = -5;
    
    [Header("Apearance")]
    public string           dogName;
    public string           breed;
    public bool             exist;
    public bool             owned;
    [Header("Variables")]
    [Range (0f,100f)]
    public float            happines;
    [Range(0f, 100f)]
    public float            health;
    public bool             sick;
    public float            timeSick=0;
    private float           timerForSick;
    [Range(0f, 100f)]
    public float            hunger;
    public float            timeHunger;
    private float           timerForHunger;
    [Range(0f, 100f)]
    public float            pets=0;
    public float            timePets;
    private float           timerForPets;
    private SpriteRenderer  rederer;
    public menus            menu;
    public dogSpawner       master;
    [SerializeField] bool   generateAtStart;
    [Header("UI")]
    public Slider           hap;
    public Slider           hun;
    public Slider           hea;
    public Text             nameDisplay;
    private Animator        animator;
    private bool            warncondition = false;

    public void CopyValues(Dog temp) 
    {
        dogName = temp.dogName;
        breed = temp.breed;
        happines = temp.happines;
        health = temp.health;
        hunger = temp.hunger;
        sick = temp.sick;
        timeHunger = temp.timeHunger;
        timePets = temp.timePets;
        timeSick = temp.timeSick;
    }

    private void Start()
    {
        if (generateAtStart)
            generate();
        rederer = GetComponent<SpriteRenderer>();
        rederer.enabled = exist;

        CalHappines();

        if (GetComponent<Animator>() != null)
            animator = GetComponent<Animator>();

        timerForHunger = timeHunger;
        timerForPets = timePets;
    }

    private void generate()
    {
        dogName = master.dogNames[UnityEngine.Random.Range(0, master.dogNames.Length)];
        breed = master.breeds[UnityEngine.Random.Range(0, master.breeds.Length)];
        print(master.breeds.Length);
        exist = true;
        owned = false;

        health = UnityEngine.Random.Range(25, 50);
        if (UnityEngine.Random.value > 0.3f)
            sick = false;
        else
            sick = true;

        hunger = UnityEngine.Random.Range(25, 50);
        pets = UnityEngine.Random.Range(0, 20);
    }

    private void UpdateSliders()
    {
        if(hap != null)
        {
            hap.value = happines / 100;
            hea.value = health / 100;
            hun.value = hunger / 100;
            nameDisplay.text = dogName;
        }
    }

    public void GetsSick()
    {
        sick = true;
        timerForSick = timeSick;
        menu.messageText.text = dogName + " has gotten sick.";
        menu.messageMenu.SetActive(true);
    }

    private void Update()
    {
        VerifyCondition();
        rederer.enabled = exist;

        UpdateSliders();

        if (animator != null && exist)
            animator.speed = happines / 100;

        if (!owned)
        {
            timerForHunger -= 0.25f*Time.deltaTime;
            timerForPets -= 0.25f*Time.deltaTime;
            if(sick)
                timerForSick -= 0.25f*Time.deltaTime;
        }
        else
        {
            timerForHunger -= Time.deltaTime;
            timerForPets -= Time.deltaTime;
            if (sick)
                timerForSick -= Time.deltaTime;
        }

        if (timerForHunger <= 0f)
        {
            timerForHunger = timeHunger;
            ModifyHunger(decreValues);
        }
        if (timerForPets <= 0f)
        {
            timerForPets = timePets;
            GivePets(decreValues);
        }
        if (timerForSick <= 0f && sick)
        {
            timerForSick = timeSick;
            ModifyHealth(decreValues);
        }
    }

    private void LateUpdate()
    {
        var subsprites = Resources.LoadAll<Sprite>(breed);

        SpriteRenderer rederer = GetComponent<SpriteRenderer>();
        string spritename = rederer.sprite.name;
        var newSprite = Array.Find(subsprites, item => item.name == spritename);

        if (newSprite)
        {
            rederer.sprite = newSprite;
        }
    }

    private void CalHappines()
    {
        happines = health * 0.4f + hunger * 0.4f + pets * 0.2f;
        UpdateSliders();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerControls temp = collision.gameObject.GetComponent<playerControls>();
            temp.SetRangeDog(this);
            hap = temp.menu.happines;
            hun = temp.menu.hunger;
            hea = temp.menu.health;
            nameDisplay = temp.menu.dogName;
        }
            
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !owned)
        {
            if (exist)
            {
                menu.messageText.text = "Clica [E] para apanhar.";
                menu.messageMenu.SetActive(true);

            }   
            else if(collision.GetComponent<playerControls>().inLeash.dogName != "")
            {
                menu.messageText.text = "Clica [E] para por.";
                menu.messageMenu.SetActive(true);
            }  
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && collision.gameObject.GetComponent<playerControls>().dogInRange == this)
        {
            collision.gameObject.GetComponent<playerControls>().inRange = false;
            hap = null;
            if (menu.messageMenu.activeSelf)
                menu.messageMenu.SetActive(false);
        }
            
    }

    public void GivePets(float x)
    {
        pets += x;
        if (pets < 0)
            pets = 0;
        if (pets > 100)
            pets = 100;
        CalHappines();
    }

    public void ModifyHunger(float x)
    {
        hunger += x;
        if (hunger < 0)
            hunger = 0;
        if (hunger > 100)
            hunger = 100;
        CalHappines();
    }

    public void ModifyHealth(float x)
    {
        health += x;
        if (health < 0)
            health = 0;
        if (health > 100)
            health = 100;
        CalHappines();
    }

    private void VerifyCondition()
    {
        if(happines == 100)
        {
            menu.messageText.text = dogName + " foi adotado!";
            menu.messageMenu.SetActive(true);
            exist = false;
            owned = false;
        }
        
        if (health <= 5 && hunger <= 5f && exist)
        {

            if (owned)
            {
                menu.messageText.text = dogName + " foi-lhe retirado devido as suas más condições!";
                menu.messageMenu.SetActive(true);
                exist = false;
                owned = false;
            }
            else
                Destroy(gameObject);
        }
        else if (health <= 15f && hunger <= 15f && exist && owned && !warncondition)
        {
            menu.messageText.text = dogName + " esta muito fraco, se a sua condição continuar a baixar, ele será retirado!";
            menu.messageMenu.SetActive(true);
            warncondition = true;
        }
        else if(health > 15f && hunger > 15f && exist && owned)
            warncondition = false;
    }
}
