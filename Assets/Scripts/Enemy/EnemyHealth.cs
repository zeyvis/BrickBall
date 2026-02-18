using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IKillable
{
   public void Kill()
    {
        Debug.Log("zombie died");
    }
}
