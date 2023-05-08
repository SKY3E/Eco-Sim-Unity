using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSimulator : MonoBehaviour
{
    public GameObject plantPrefab;
    public GameObject herbivorePrefab;
    public Sprite Circle;

    void Start()
    {
        InvokeRepeating("SpawnPlant", 0.0f, 0.4f);
        SpawnHerbivore();
    }

    void SpawnPlant() 
    {
        GameObject sunflowerPlant = Plant.CreatePlant("Sunflower", 0.3f, Color.yellow, Circle);
    }

    void SpawnHerbivore()
    {
        Instantiate(herbivorePrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }
}
