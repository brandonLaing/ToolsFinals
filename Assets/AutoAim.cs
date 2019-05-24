using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// You may or may not want to create a custom inspector for this class,
/// depending on your approach to solving the problem.
/// 
/// Remember: You are submitting a series of times, rotations, and elevations - not code.
/// You only need to write a custom inspector if it will be useful to you in 
/// finding valid shots.
/// </summary>
public class AutoAim : MonoBehaviour
{

  public float[] shotTimes;
  public float[] platformRotations;
  public float[] barrelElevations;

  private int shotIndex = 0;

  private BarrelControl barrelControl;

  public List<Vector3> positions = new List<Vector3>();
  public List<float> timeDifference = new List<float>();

  public GameObject wanderer = null;
  public BarrelControl aim = null;

  public List<bool> hitTracker = new List<bool>();

  private void Awake()
  {
    System.Text.StringBuilder timeSB = new System.Text.StringBuilder();
    System.Text.StringBuilder rotationSB = new System.Text.StringBuilder();
    System.Text.StringBuilder elevationSB = new System.Text.StringBuilder();

    for (int i = 0; i < 10; i++ )
    {
      timeSB.AppendLine(shotTimes[i].ToString());
      rotationSB.AppendLine(platformRotations[i].ToString());
      elevationSB.AppendLine(barrelElevations[i].ToString());
    }

    Debug.Log(timeSB);
    Debug.Log(rotationSB);
    Debug.Log(elevationSB);
  }
  void Start()
  {
    barrelControl = GetComponent<BarrelControl>();
  }

  void Update()
  {
    if (shotIndex >= shotTimes.Length)
    {
      this.enabled = false;
      return;
    }

    float shotTime = shotTimes[shotIndex];
    if (Time.timeSinceLevelLoad >= shotTime)
    {
      Debug.Log(wanderer.transform.position);

      // Fire!
      barrelControl.platformRotation = platformRotations[shotIndex];
      barrelControl.barrelElevation = barrelElevations[shotIndex];

      Debug.Log($"Time to hit: {aim.GetEstimatedShotLandingTime()}");
      //if (shotIndex == 9)
      //Debug.Log(wanderer.transform.position);

      barrelControl.Fire();
      shotIndex++;
    }

  }
  private void OnDrawGizmos()
  {
    for (int i = 0; i < positions.Count; i++)
    {
      if (hitTracker[i])
      {
        Gizmos.color = Color.green;
      }
      else
      {
        Gizmos.color = Color.red;
      }
      Vector3 point = positions[i];
      Gizmos.DrawWireSphere(point, 1);
    }

    if (Application.isPlaying)
    {
      Gizmos.color = Color.black;
      try
      {
        Gizmos.DrawSphere(positions[shotIndex], 1);
      }
      catch
      {

      }
    }
  }
}
