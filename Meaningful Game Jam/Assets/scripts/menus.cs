using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menus : MonoBehaviour
{
    [Header("Menus")]
    [SerializeField] GameObject     pauseMenu;
    public GameObject               dogMenu;
    public GameObject               feedingMenu;
    public GameObject               messageMenu;
    public GameObject               SellMenu;
    [Header("Sliders")]
    public Slider                   happines;
    public Slider                   hunger;
    public Slider                   health;
    public Slider                   comunityHappines;
    [Header("Buttons")]
    public Button                   medecine;
    public Button                   deliciosa;
    public Button                   happyPet;
    [Header("Texts")]
    public Text                     dogName;
    public Text                     messageText;
    public Text                     sellerName;
    public Text                     sellerPrice;
    [Header("Image")]
    public Image                    sellerProdoct;

    public inventario              playerInventario;
    [Header("Variables")]
    public float                    timeMessage = 5;
    private float                   timer;
    [SerializeField] bool           inGame;
    public Dog                      cao;
    private int[]                   foodGains = new int[2] {15, 5};
    public comunidade               comunity;

    public void Load(string scene)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(scene);
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void Start()
    {
        if (inGame)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            playerInventario = GetComponent<inventario>();
            timer = timeMessage;
        }
    }

    private void Update()
    {
        if (inGame && Input.GetButtonDown("Cancel"))
        {
            if (pauseMenu.activeSelf)
            {
                pauseMenu.SetActive(false);
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                pauseMenu.SetActive(true);
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                Time.timeScale = 0;
            }
                
        }

        if (inGame && dogMenu.activeSelf == true)
            VerifyItems();

        if(inGame && messageMenu.activeSelf == true)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                timer = timeMessage;
                messageMenu.SetActive(false);
            }
        }
    }

    private void VerifyItems()
    {
        medecine.interactable = playerInventario.HasIndex(1);
        deliciosa.interactable = playerInventario.HasIndex(2);
        happyPet.interactable = playerInventario.HasIndex(3);
    }

    public void OpenDogMenu(Dog dog)
    {
        cao = dog;
        dogMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void CloseDogMenu()
    {
        dogMenu.SetActive(false);
        feedingMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void FeedButton()
    {
        if (feedingMenu.activeSelf == true)
            feedingMenu.SetActive(false);
        else
            feedingMenu.SetActive(true);
    }

    public void Feed(int foodType)
    {
        if(playerInventario.HasIndex(2 + foodType))
        {
            cao.ModifyHunger(foodGains[foodType]);
            playerInventario.ModifyValue(-1, 2 + foodType);
        }
            
    }

    public void Heal(float amount)
    {
        if (playerInventario.HasIndex(1))
        {
            print("has medecine");
            cao.ModifyHealth(amount);
            playerInventario.ModifyValue(-1, 1);
            cao.sick = false;
        }
    }

    public void Pets(float amount)
    {
        cao.GivePets(amount);
    }
}
