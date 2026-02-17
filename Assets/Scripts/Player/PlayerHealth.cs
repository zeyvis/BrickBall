using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour,IKillable
{
    public void Kill()
    {
        Debug.Log("Player died");
    }
}
