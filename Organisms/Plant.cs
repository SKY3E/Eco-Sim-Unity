using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
  // Plant gene attributes
  public string family;
  public float size;
  public Color color;

  // Create a plant game object
  public static GameObject CreatePlant(string family, float size, Color color, Sprite sprite, GameSimulator gameSimulator)
  {
    GameObject plantGO = new GameObject(family);
    SpriteRenderer spriteRenderer = plantGO.AddComponent<SpriteRenderer>();
    SphereCollider sphereCollider = plantGO.AddComponent<SphereCollider>();

    spriteRenderer.sprite = sprite;
    spriteRenderer.color = color;
    spriteRenderer.sortingOrder = 1;

    sphereCollider.radius = size;

    Plant plant = plantGO.AddComponent<Plant>();
    plant.family = family;
    plant.size = size;
    plant.color = color;

    plantGO.transform.localScale = new Vector3(size, size, size);
    plantGO.tag = "Plant";
    plantGO.transform.position = new Vector3(Random.Range(-15f, 15f), Random.Range(-7.5f, 7.5f), 0);

    gameSimulator.plantCount++;
    return plantGO;
  }
}
