using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
  public float range = 1f;

  public TextMeshProUGUI organismTypeText;
  public TextMeshProUGUI organismFamilyText;
  public GameObject cursorPicker;

  void Start()
  {
    cursorPicker = GameObject.Find("Cursor Picker");
    FindUiElements();
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
            Debug.Log("Clicked on plant!");
            UpdateOrganismUi(collider.tag, collider.GetComponent<Plant>().family);
        }
        if (collider.CompareTag("Herbivore"))
        {
            Debug.Log("Clicked on herbivore!");
            UpdateOrganismUi(collider.tag, collider.GetComponent<Herbivore>().family);
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
    organismTypeText = GameObject.Find("Organism Type").GetComponent<TextMeshProUGUI>();
    organismFamilyText = GameObject.Find("Organism Family").GetComponent<TextMeshProUGUI>();
  }

  void UpdateOrganismUi(string Type, string Family)
  {
    organismTypeText.text = "Type : " + Type;
    organismFamilyText.text = "Family : " + Family;
  }
}
