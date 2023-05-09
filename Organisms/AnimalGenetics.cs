using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalGenetics
{
  // Animal gene attributes
  public string family;
  public float range;
  public float shrinkSpeed;
  public float destroyThreshold;
  public float size;
  public Color color;
  public float maxEnergy;
  public float reproduceThreshold;

  // Create an animal genetics object
  public AnimalGenetics(string family, float range, float shrinkSpeed, float destroyThreshold, float size, Color color, float maxEnergy, float reproduceThreshold)
  {
    this.family = family;
    this.range = range;
    this.shrinkSpeed = shrinkSpeed;
    this.destroyThreshold = destroyThreshold;
    this.size = size;
    this.color = color;
    this.maxEnergy = maxEnergy;
    this.reproduceThreshold = reproduceThreshold;
  }
}
