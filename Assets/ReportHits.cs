using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#region Nothing to see here (do not change)

public class ReportHits : MonoBehaviour
{
  private bool firstHitReported = false;
  private void OnCollisionEnter(Collision collision)
  {

    if (!firstHitReported)
    {
      if (collision.collider.tag == "Player")
      {
        Debug.LogError("Player hit!");
      }
      else
      {
        Debug.LogError("Miss");
      }
      firstHitReported = true;
    }

  }
}
#endregion
