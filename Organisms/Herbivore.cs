using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Herbivore : MonoBehaviour
{
  // Access game values
  public GameSimulator gameSimulator;
  // Herbivore movement attributes
  public Vector3 targetLocation;
  public float energy;
  public float reproduce;
  // Herbivore gene attributes
  public string family;
  public float range;
  public float shrinkSpeed;
  public float destroyThreshold;
  public float size;
  public float maxEnergy;
  public float reproduceThreshold;
  public Color color;

  void Start()
  {
    energy = maxEnergy;
    reproduce = 0f;
    gameSimulator = GameObject.Find("Game Simulator").GetComponent<GameSimulator>();
    InvokeRepeating("ChangeTargetLocation", 0f, 5f);
  }

  void Update()
  {
    energy -= 1f * Time.deltaTime;
    reproduce += 1f * Time.deltaTime;
    if (energy > 50f && reproduce >= reproduceThreshold)
    {
      MoveToLocation();
      CheckFamilyOverlap();
    }
    else if (energy <= 1f)
    {
      Destroy(gameObject);
      gameSimulator.herbivoreCount--;
    }
    else {
      MoveToLocation();
    }
    CheckPlantOverlap();
  }

  void OnDrawGizmosSelected()
  {
    Gizmos.color = Color.green;
    Gizmos.DrawWireSphere(transform.position, range);
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
          gameSimulator.plantCount--;
          energy += 15f;
        }
      }
    }
  }
  void CheckFamilyOverlap()
  {
    Collider[] colliders = Physics.OverlapSphere(transform.position, range);

    foreach (Collider collider in colliders)
    {
      if (collider.CompareTag("Herbivore"))
      {
        if (!Object.ReferenceEquals(collider.gameObject, gameObject))
        {
          Herbivore collidedHerbivore = collider.gameObject.GetComponent<Herbivore>();
          if (collidedHerbivore != null)
          {
            string colliderFamily = collidedHerbivore.family;
            if (colliderFamily == family)
            {
              if (collidedHerbivore.energy > 50f)
              {
                if (collidedHerbivore.reproduce >= collidedHerbivore.reproduceThreshold)
                {
                  energy -= 30f;
                  reproduce = 0f;
                  collidedHerbivore.energy -= 30f;
                  collidedHerbivore.reproduce = 0f;
                  Debug.Log("Reproduced");
                  AnimalGenetics genetics = new AnimalGenetics("Herbivore", 2.0f, 0.3f, 0.1f, 0.6f, Color.white, 100f, 20f);
                  CreateHerbivore(gameSimulator.Square, new Vector3(0, 0, 0), genetics, gameSimulator);
                }
              }
            }
          }
        }
      }
    }
  }

  // Create a herbivore game object
  public static GameObject CreateHerbivore(Sprite sprite, Vector3 position, AnimalGenetics genetics, GameSimulator gameSimulator)
  {
    GameObject herbivoreGO = new GameObject(genetics.family);
    SpriteRenderer spriteRenderer = herbivoreGO.AddComponent<SpriteRenderer>();
    SphereCollider sphereCollider = herbivoreGO.AddComponent<SphereCollider>();

    spriteRenderer.sprite = sprite;
    spriteRenderer.color = genetics.color;
    spriteRenderer.sortingOrder = 2;

    sphereCollider.radius = genetics.size;

    Herbivore herbivore = herbivoreGO.AddComponent<Herbivore>();
    herbivore.family = genetics.family;
    herbivore.range = genetics.range;
    herbivore.shrinkSpeed = genetics.shrinkSpeed;
    herbivore.destroyThreshold = genetics.destroyThreshold;
    herbivore.size = genetics.size;
    herbivore.color = genetics.color;
    herbivore.maxEnergy = genetics.maxEnergy;
    herbivore.reproduceThreshold = genetics.reproduceThreshold;

    herbivoreGO.transform.localScale = new Vector3(genetics.size, genetics.size, genetics.size);
    herbivoreGO.tag = "Herbivore";
    herbivoreGO.transform.position = position;

    gameSimulator.herbivoreCount++;
    return herbivoreGO;
  }
}

