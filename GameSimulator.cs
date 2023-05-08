using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSimulator : MonoBehaviour
{
  // Game sprite references
  public Sprite Circle;
  public Sprite Square;

  void Start()
  {
    InvokeRepeating("SpawnPlant", 0.0f, 0.4f);
    SpawnHerbivore();
  }

  // Spawn a plant every 0.4 seconds
  void SpawnPlant() 
  {
    GameObject sunflowerPlant = Plant.CreatePlant("Sunflower", 0.3f, Color.yellow, Circle);
  }
  // Spawn a herbivore
  void SpawnHerbivore()
  {
    GameObject herbivoreGameObject = Herbivore.CreateHerbivore(Square, new Vector3(0, 0, 0), new AnimalGenetics("Herbivore", 2.0f, 0.3f, 0.1f, 0.6f, Color.white));
  }
}
