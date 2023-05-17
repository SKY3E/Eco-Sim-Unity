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

  void Start()
  {
    mainCamera = Camera.main;
    InvokeRepeating("SpawnPlant", 0.0f, 0.4f);
    SpawnHerbivore();
    SpawnHerbivore();
    SpawnHerbivore();
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
  void SpawnHerbivore()
  {
    AnimalGenetics genetics = new AnimalGenetics("Sheep", 2.0f, 0.3f, 0.1f, 0.6f, Color.white, 50f, 20f);
    GameObject herbivoreGameObject = Herbivore.CreateHerbivore(Square, new Vector3(0, 0, 0), genetics, this);
  }

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
