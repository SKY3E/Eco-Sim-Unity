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
  public List<Color> AnimalGeneticColors = new List<Color>(); 
  public List<string> AnimalGeneticNames = new List<string>();

  public float animalMaxRange = 3f;
  public float animalMinRange = 0f;
  public float animalMaxSize = 0.8f;
  public float animalMinSize = 0.4f;

  void Start()
  {
    mainCamera = Camera.main;
    // Spawn plants every .4 seconds
    InvokeRepeating("SpawnPlant", 0.0f, 0.4f);
    // Initialize animal genetic lists
    InitializeLists();
    // Get random color & family from lists & spawn herbivores
    int randomIndex = UnityEngine.Random.Range(0, AnimalGeneticColors.Count);
    Color color = AnimalGeneticColors[randomIndex];
    string family = AnimalGeneticNames[randomIndex];
    SpawnHerbivore(10, new Vector3(0, 0, 0), family, color, animalMinRange, animalMaxRange, 0.3f, 0.1f, animalMinSize, animalMaxSize, 50f, 20f);
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
  public void SpawnHerbivore(int count, Vector3 position, string Family, Color Color, float AnimalMinRange, float AnimalMaxRange, float ShrinkSpeed, float DestroyThreshold, float AnimalMinSize, float AnimalMaxSize, float MaxEnergy, float ReproduceThreshold)
  {
    for (int i = 0; i < count; i++)
    {
      AnimalGenetics genetics = new AnimalGenetics(Family, Random.Range(AnimalMinRange, AnimalMaxRange), ShrinkSpeed, DestroyThreshold, Random.Range(AnimalMinSize, AnimalMaxSize), Color, MaxEnergy, ReproduceThreshold);
      GameObject herbivoreGameObject = Herbivore.CreateHerbivore(Square, position, genetics, this);
    }
  }

  // Initialize Color & Family lists
  void InitializeLists()
  {
    AnimalGeneticColors.Add(Color.white);
    AnimalGeneticColors.Add(Color.yellow);
    AnimalGeneticColors.Add(Color.red);
    AnimalGeneticColors.Add(Color.gray);
    AnimalGeneticColors.Add(Color.blue);

    AnimalGeneticNames.Add("Sheep");
    AnimalGeneticNames.Add("Giraffe");
    AnimalGeneticNames.Add("Red Panda");
    AnimalGeneticNames.Add("Wolf");
    AnimalGeneticNames.Add("Platypus");
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
