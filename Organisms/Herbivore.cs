using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Herbivore : MonoBehaviour
{
  // Herbivore movement attributes
  public Vector3 targetLocation;
  // Herbivore gene attributes
  public string family;
  public float range;
  public float shrinkSpeed;
  public float destroyThreshold;
  public float size;
  public Color color;

  void Start()
  {
    InvokeRepeating("ChangeTargetLocation", 0f, 5f);
  }

  void Update()
  {
    MoveToLocation();
    CheckPlantOverlap();
  }

  // Move to target location
  void MoveToLocation()
  {
    transform.position = Vector3.MoveTowards(transform.position, targetLocation, 2f * Time.deltaTime);
  }
  // Change target location every 5 seconds
  void ChangeTargetLocation()
  {
    targetLocation = new Vector3(Random.Range(-14f, 14f), Random.Range(-6.5f, 6.5f), 0);
  }
  // Check if the herbivore is overlapping with a plant
  void CheckPlantOverlap()
  {
    Collider[] colliders = Physics.OverlapSphere(transform.position, range);

    foreach (Collider collider in colliders)
    {
      if (collider.CompareTag("Plant"))
      {
        collider.transform.localScale -= Vector3.one * shrinkSpeed * Time.deltaTime;
        if (collider.transform.localScale.x <= destroyThreshold)
        {
          Destroy(collider.gameObject);
        }
      }
    }
  }

  // Create a herbivore game object
  public static GameObject CreateHerbivore(Sprite sprite, Vector3 position, AnimalGenetics genetics)
  {
    GameObject herbivoreGO = new GameObject(genetics.family);
    SpriteRenderer spriteRenderer = herbivoreGO.AddComponent<SpriteRenderer>();

    spriteRenderer.sprite = sprite;
    spriteRenderer.color = genetics.color;
    spriteRenderer.sortingOrder = 2;

    Herbivore herbivore = herbivoreGO.AddComponent<Herbivore>();
    herbivore.family = genetics.family;
    herbivore.range = genetics.range;
    herbivore.shrinkSpeed = genetics.shrinkSpeed;
    herbivore.destroyThreshold = genetics.destroyThreshold;
    herbivore.size = genetics.size;
    herbivore.color = genetics.color;

    herbivoreGO.transform.localScale = new Vector3(genetics.size, genetics.size, genetics.size);
    herbivoreGO.tag = "Herbivore";
    herbivoreGO.transform.position = position;

    return herbivoreGO;
  }
}

