using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AimAutomater : MonoBehaviour
{
  public ShotInfo[] shots = new ShotInfo[10];
  public int index = 0;
  public BarrelControl barrel;

  public Transform target;

  void Start()
  {

  }

  private void Update()
  {
    if (index >= shots.Length)
    {
      this.enabled = false;
      return;
    }

    if (Time.realtimeSinceStartup >= shots[index].shootTime)
    {
      if (shots[index] == null)
      {
        shots[index].Position = target.position;
      }
    }
  }

  private void OnDestroy()
  {

  }
}

[System.Serializable]
public class ShotInfo
{
  public float shootTime;
  public float hitTime;

  public float rotation;

  public float elevation;

  private float[] _position = null;

  public Vector3 Position
  {
    get
    {
      return new Vector3(_position[0], _position[1], _position[2]);
    }
    set
    {
      float[] temp = { value.x, value.y, value.z };
      _position = temp;
    }
  }


  public bool hits = false;
}

[CustomEditor(typeof(ShotInfo))]
public class ShotInfoEdior : Editor
{
  public override void OnInspectorGUI()
  {
    base.OnInspectorGUI();

    EditorGUILayout.Vector3Field()
  }
}

