using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
  public float range = 1f;

  public GameObject infoPanel;

  public TextMeshProUGUI organismTypeText;
  public TextMeshProUGUI organismFamilyText;
  public TextMeshProUGUI organismColorText;
  public TextMeshProUGUI organismSizeText;
  
  public GameObject cursorPicker;

  void Start()
  {
    FindUiElements();
    infoPanel.SetActive(false);
    cursorPicker = GameObject.Find("Cursor Picker");
  }

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
          UpdateOrganismUi(collider.tag, collider.GetComponent<Plant>().family, collider.GetComponent<Plant>().color, collider.GetComponent<Plant>().size);
        }
        if (collider.CompareTag("Herbivore"))
        {
          infoPanel.SetActive(true);
          UpdateOrganismUi(collider.tag, collider.GetComponent<Herbivore>().family, collider.GetComponent<Herbivore>().color, collider.GetComponent<Herbivore>().size);
        }
      }
    }
  }

  void DrawCursorCircle()
  {
    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    cursorPicker.transform.position = mousePosition;
    float circleRadius = range / 2f;
    cursorPicker.transform.localScale = new Vector3(circleRadius * 2, circleRadius * 2, 1f);
  }

  void FindUiElements()
  {
    infoPanel = GameObject.Find("Info Panel");
    organismTypeText = GameObject.Find("Organism Type").GetComponent<TextMeshProUGUI>();
    organismFamilyText = GameObject.Find("Organism Family").GetComponent<TextMeshProUGUI>();
    organismColorText = GameObject.Find("Organism Color").GetComponent<TextMeshProUGUI>();
    organismSizeText = GameObject.Find("Organism Size").GetComponent<TextMeshProUGUI>();
  }

  void UpdateOrganismUi(string Type, string Family, Color Color, float Size)
  {
    organismTypeText.text = "Type : " + Type;
    organismFamilyText.text = "Family : " + Family;
    organismColorText.text = "Color : " + Color;
    organismSizeText.text = "Size : " + Size;
  }

  public void CloseInfoPanel()
  {
    infoPanel.SetActive(false);
  }
}
