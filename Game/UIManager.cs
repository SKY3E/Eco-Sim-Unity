using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
  // Access game values
  public GameSimulator gameSimulator;
  // Picker attributes
  public GameObject cursorPicker;
  public float range = 1f;
  // UI attributes
  public GameObject infoPanel;
  public string selectedOrganism;
  public GameObject selectedOrganismGameObject;
  // UI elements
  public TextMeshProUGUI organismTypeText;
  public TextMeshProUGUI organismFamilyText;
  public TextMeshProUGUI organismColorText;
  public TextMeshProUGUI organismSizeText;
  public TextMeshProUGUI organismExtra1;
  public TextMeshProUGUI organismExtra2;

  // Find UI elements && set initial info panel state
  void Start()
  {
    gameSimulator = GameObject.Find("Game Simulator").GetComponent<GameSimulator>();
    FindUiElements();
    infoPanel.SetActive(false);
  }

  // Draw cursor circle & check for click on organisms
  void Update()
  {
    DrawCursorCircle();

    if (Input.GetMouseButtonDown(0))
    {
      Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      Collider2D[] colliders = Physics2D.OverlapCircleAll(clickPosition, range / 2f);

      foreach (Collider2D collider in colliders)
      {
        if (collider.CompareTag("Plant"))
        {
          infoPanel.SetActive(true);
          selectedOrganism = collider.tag;
          selectedOrganismGameObject = collider.gameObject;
          UpdateOrganismUi(collider.tag, collider.GetComponent<Plant>().family, collider.GetComponent<Plant>().color, collider.GetComponent<Plant>().size, 0f, 0f);
        }
        if (collider.CompareTag("Herbivore"))
        {
          infoPanel.SetActive(true);
          selectedOrganism = collider.tag;
          selectedOrganismGameObject = collider.gameObject;
          UpdateOrganismUi(collider.tag, collider.GetComponent<Herbivore>().family, collider.GetComponent<Herbivore>().color, collider.GetComponent<Herbivore>().size, collider.GetComponent<Herbivore>().reproduceThreshold, collider.GetComponent<Herbivore>().energy);
        }
      }
    }

    if (selectedOrganismGameObject != null)
    {
      gameSimulator.FollowCamera(selectedOrganismGameObject);
    }
  }

  // Draw cursor circle
  void DrawCursorCircle()
  {
    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    cursorPicker.transform.position = mousePosition;
    float circleRadius = range / 2f;
    cursorPicker.transform.localScale = new Vector3(circleRadius * 2, circleRadius * 2, 1f);
  }
  // Find UI elements
  void FindUiElements()
  {
    cursorPicker = GameObject.Find("Cursor Picker");
    infoPanel = GameObject.Find("Info Panel");
    organismTypeText = GameObject.Find("Organism Type").GetComponent<TextMeshProUGUI>();
    organismFamilyText = GameObject.Find("Organism Family").GetComponent<TextMeshProUGUI>();
    organismColorText = GameObject.Find("Organism Color").GetComponent<TextMeshProUGUI>();
    organismSizeText = GameObject.Find("Organism Size").GetComponent<TextMeshProUGUI>();
    organismExtra1 = GameObject.Find("Organism Extra 1").GetComponent<TextMeshProUGUI>();
    organismExtra1.gameObject.SetActive(false);
    organismExtra2 = GameObject.Find("Organism Extra 2").GetComponent<TextMeshProUGUI>();
    organismExtra2.gameObject.SetActive(false);
  }
  // Update UI to show organism info
  void UpdateOrganismUi(string type, string family, Color color, float size, float reproduceThreshold, float energy)
  {
    organismTypeText.text = "Type : " + type;
    organismFamilyText.text = "Family : " + family;
    organismColorText.text = "Color : " + GetColorName(color);
    organismSizeText.text = "Size : " + size;

    if (selectedOrganism == "Herbivore")
    {
      organismExtra1.gameObject.SetActive(true);
      organismExtra1.text = "Reproduce Threshold : " + reproduceThreshold;
      organismExtra2.gameObject.SetActive(true);
      organismExtra2.text = "Energy : " + energy;
    }
    else 
    {
      organismExtra1.gameObject.SetActive(false);
      organismExtra2.gameObject.SetActive(false);
    }
  }
  // Close info panel
  public void CloseInfoPanel()
  {
    infoPanel.SetActive(false);
    selectedOrganism = null;
    selectedOrganismGameObject = null;
  }
  // Get color name from color
  string GetColorName(Color color)
  {
    if (color == Color.red) return "Red";
    else if (color == Color.green) return "Green";
    else if (color == Color.blue) return "Blue";
    else if (color == Color.yellow) return "Yellow";
    else if (color == Color.white) return "White";
    else if (color == Color.black) return "Black";
    else if (color == Color.gray) return "Gray";
    else return "Unknown";
  }
}
