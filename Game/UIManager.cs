using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
  void Update()
  {
    if (Input.GetMouseButtonDown(0))
    {
      Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      Collider2D[] colliders = Physics2D.OverlapCircleAll(clickPosition, 4f);

      foreach (Collider2D collider in colliders)
      {
        if (collider.CompareTag("Plant"))
        {
            Debug.Log("Clicked on plant!");
        }
      }
    }
  }
}
