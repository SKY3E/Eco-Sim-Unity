using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Herbivore : MonoBehaviour
{
  private Vector3 targetLocation;
  private float range = 5.0f;
  private float shrinkSpeed = 0.1f;
  private float destroyThreshold = 0.1f;

  // Start is called before the first frame update
  void Start()
  {
    InvokeRepeating("ChangeTargetLocation", 0.0f, 5.0f);
  }

  // Update is called once per frame
  void Update()
  {
    MoveToLocation();
    CheckPlantOverlap();
  }

  private void OnDrawGizmos()
  {
    Gizmos.color = Color.blue;
    Gizmos.DrawWireSphere(transform.position, range);
  }

  void MoveToLocation()
  {
    transform.position = Vector3.MoveTowards(transform.position, targetLocation, 2f * Time.deltaTime);
  }

  void ChangeTargetLocation()
  {
    targetLocation = new Vector3(Random.Range(-15f, 15f), Random.Range(-7.5f, 7.5f), 0);
  }

  void CheckPlantOverlap() 
  {
    Collider[] colliders = Physics.OverlapSphere(transform.position, range);

    foreach (Collider collider in colliders)
    {
      Debug.Log("Found a collider!");
      if (collider.CompareTag("Plant"))
      {
        Debug.Log("Found a plant!");
        // Shrink the plant object
        collider.transform.localScale -= Vector3.one * shrinkSpeed * Time.deltaTime;

        // If the plant object is small enough, delete it
        if (collider.transform.localScale.x <= destroyThreshold)
        {
          Destroy(collider.gameObject);
        }
      }
    }
  }
}
