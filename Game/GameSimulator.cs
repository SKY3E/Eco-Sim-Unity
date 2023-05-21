using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSimulator : MonoBehaviour
{
  // Game sprite references
  public Sprite Circle;
  public Sprite Square;
  // Game object values
  public int plantCount = 0;
  public int herbivoreCount = 0;
  // Camera values
  public float zoomSpeed = 1f;
  public float minZoomSize = 1f;
  public float maxZoomSize = 10f;
  public float moveSpeed = 5f;
  // Camera references
  public Camera mainCamera;
  // Animal gentic variables
  public string Name { get; private set; }
  public Color Color { get; private set; }
  public string[] AnimalGeneticNames = { "Sheep", "Giraffe", "Red Panda", "Wolf", "Platypus" }; 
  public Color[] AnimalGeneticColors = { Color.white, Color.yellow, Color.red, Color.gray, Color.blue };

  public float AnimalMaxRange = 3f;
  public float AnimalMinRange = 0f;
  public float AnimalMaxSize = 0.8f;
  public float AnimalMinSize = 0.4f;

  void Start()
  {
    mainCamera = Camera.main;
    InvokeRepeating("SpawnPlant", 0.0f, 0.4f);
    SpawnHerbivore(10, new Vector3(0, 0, 0));
  }

  void Update()
  {
    UpdateCamera();
  }

  // Spawn a plant every 0.4 seconds
  void SpawnPlant() 
  {
    GameObject sunflowerPlant = Plant.CreatePlant("Sunflower", 0.3f, Color.yellow, Circle, this);
  }
  // Spawn a herbivore
  public void SpawnHerbivore(int count, Vector3 position)
  {
    System.Random random = new System.Random();

    for (int i = 0; i < count; i++)
    {
      int randomIndex = random.Next(0, AnimalGeneticNames.Length);
      Name = AnimalGeneticNames[randomIndex];
      Color = AnimalGeneticColors[randomIndex];
      Debug.Log("Length of Genetics Name : " + count + " " + AnimalGeneticNames.Length);
      Debug.Log("Index Id : " + count + " " + randomIndex);
      AnimalGenetics genetics = new AnimalGenetics(Name, Random.Range(AnimalMinRange, AnimalMaxRange), 0.3f, 0.1f, Random.Range(AnimalMinSize, AnimalMaxSize), Color, 50f, 20f);
      GameObject herbivoreGameObject = Herbivore.CreateHerbivore(Square, position, genetics, this);
    }

    Debug.Log("Spawning herbivore");
  }

  // Move camera w/ controls
  void UpdateCamera()
  {
    float scrollInput = Input.GetAxis("Mouse ScrollWheel");
    float horizontalInput = Input.GetAxis("Horizontal");
    float verticalInput = Input.GetAxis("Vertical");

    if (Mathf.Abs(scrollInput) > 0.01f)
    {
      float newZoomSize = mainCamera.orthographicSize - scrollInput * zoomSpeed;
      newZoomSize = Mathf.Clamp(newZoomSize, minZoomSize, maxZoomSize);
      mainCamera.orthographicSize = newZoomSize;
    }

    Vector3 moveDirection = new Vector3(horizontalInput, verticalInput, 0f);
    mainCamera.transform.position += moveDirection * moveSpeed * Time.deltaTime;
  }
}
