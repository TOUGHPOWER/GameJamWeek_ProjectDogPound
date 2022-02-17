using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dogSpawner : MonoBehaviour
{
    public menus                menus;
    [SerializeField] GameObject cao;
    private float               timer =0;
    [SerializeField] float      timeToSpawn = 5;
    private GameObject          caoSpawned;
    public string[]             dogNames;
    public string[]             breeds;

    private void Update()
    {
        if (caoSpawned == null)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                SpawnDog();
                timer = timeToSpawn;
            }
            else if(timer < 0)
                timer = 0;

        }      
    }

    private void SpawnDog()
    {
        Dog dog = cao.GetComponent<Dog>();
        dog.menu = menus;
        dog.master = this;

        caoSpawned = Instantiate(cao, transform.position, transform.rotation);
    }
}
