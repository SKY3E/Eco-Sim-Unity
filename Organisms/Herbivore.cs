using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Herbivore : MonoBehaviour
{
  public Vector3 targetLocation;

  public string name;
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

  void MoveToLocation()
  {
    transform.position = Vector3.MoveTowards(transform.position, targetLocation, 2f * Time.deltaTime);
  }

  void ChangeTargetLocation()
  {
    targetLocation = new Vector3(Random.Range(-14f, 14f), Random.Range(-6.5f, 6.5f), 0);
  }

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

  public static GameObject CreateHerbivore(Sprite sprite, Vector3 position, AnimalGenetics genetics)
  {
    GameObject herbivoreGO = new GameObject(genetics.name);
    SpriteRenderer spriteRenderer = herbivoreGO.AddComponent<SpriteRenderer>();

    spriteRenderer.sprite = sprite;
    spriteRenderer.color = genetics.color;
    spriteRenderer.sortingOrder = 2;

    Herbivore herbivore = herbivoreGO.AddComponent<Herbivore>();
    herbivore.name = genetics.name;
    herbivore.range = genetics.range;
    herbivore.shrinkSpeed = genetics.shrinkSpeed;
    herbivore.destroyThreshold = genetics.destroyThreshold;
    herbivore.size = genetics.size;
    herbivore.color = genetics.color;

    // Set Game Object attributes
    herbivoreGO.transform.localScale = new Vector3(genetics.size, genetics.size, genetics.size);
    herbivoreGO.tag = "Herbivore";
    herbivoreGO.transform.position = position;

    return herbivoreGO;
  }
}

