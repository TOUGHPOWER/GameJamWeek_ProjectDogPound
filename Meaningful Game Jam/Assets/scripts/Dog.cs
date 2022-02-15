using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    public string   Name;
    public string   Breed;
    [Range (0f,100f)]
    public float    happines;
    [Range(0f, 100f)]
    public float    health;
    [Range(0f, 100f)]
    public float    hunger;
    [Range(0f, 100f)]
    public float    appeal;
    public bool     sick;

    private void calHappines()
    {
        happines = health * 0.4f + (100f - hunger) * 0.4f + appeal * 0.2f;
    }
}
