using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menus : MonoBehaviour
{
    [SerializeField] bool InGame;
    [SerializeField] GameObject pausemenu;
    public void Load(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void Start()
    {
        if (InGame)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void Update()
    {
        if (InGame && Input.GetButtonDown("Cancel"))
        {
            if (pausemenu.activeSelf)
            {
                pausemenu.SetActive(false);
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                pausemenu.SetActive(true);
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                Time.timeScale = 0;
            }
                
        }
    }
}
