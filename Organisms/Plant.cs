using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public string plantName;
    public float size;
    public Color color;

    public static GameObject CreatePlant(string name, float size, Color color, Sprite sprite)
    {
        GameObject plantGO = new GameObject(name);
        SpriteRenderer spriteRenderer = plantGO.AddComponent<SpriteRenderer>();
        SphereCollider sphereCollider = plantGO.AddComponent<SphereCollider>();

        // Set the attributes of the SpriteRenderer component
        spriteRenderer.sprite = sprite;
        spriteRenderer.color = color;

        // Set the attributes of the SphereCollider component
        sphereCollider.radius = size;

        Plant plant = plantGO.AddComponent<Plant>();
        plant.plantName = name;
        plant.size = size;
        plant.color = color;

        // Set Game Object attributes
        plantGO.transform.localScale = new Vector3(size, size, size);
        plantGO.tag = "Plant";
        plantGO.transform.position = new Vector3(Random.Range(-15f, 15f), Random.Range(-7.5f, 7.5f), 0);

        return plantGO;
    }
}
