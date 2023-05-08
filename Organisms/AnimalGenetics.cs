using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalGenetics
{
  public string name;
  public float range;
  public float shrinkSpeed;
  public float destroyThreshold;
  public float size;
  public Color color;

  public AnimalGenetics(string name, float range, float shrinkSpeed, float destroyThreshold, float size, Color color)
  {
    this.name = name;
    this.range = range;
    this.shrinkSpeed = shrinkSpeed;
    this.destroyThreshold = destroyThreshold;
    this.size = size;
    this.color = color;
  }
}
