using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSimulator : MonoBehaviour
{
  // Game sprite references
  public Sprite Circle;
  public Sprite Square;
  public GameSimulator gameSimulator;
  // Game object values
  public int plantCount = 0;
  public int herbivoreCount = 0;

  void Start()
  {
    gameSimulator = GameObject.Find("Game Simulator").GetComponent<GameSimulator>();
    InvokeRepeating("SpawnPlant", 0.0f, 0.4f);
    SpawnHerbivore();
    SpawnHerbivore();
    SpawnHerbivore();
  }

  // Spawn a plant every 0.4 seconds
  void SpawnPlant() 
  {
    GameObject sunflowerPlant = Plant.CreatePlant("Sunflower", 0.3f, Color.yellow, Circle, this);
  }
  // Spawn a herbivore
  void SpawnHerbivore()
  {
    AnimalGenetics genetics = new AnimalGenetics("Herbivore", 2.0f, 0.3f, 0.1f, 0.6f, Color.white, 50f, 20f);
    GameObject herbivoreGameObject = Herbivore.CreateHerbivore(Square, new Vector3(0, 0, 0), genetics, this);
  }
}
