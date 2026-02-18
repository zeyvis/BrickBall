using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private SpeedBoostController _speedBoostController;

    void Start()
    {
        _speedBoostController=GetComponent<SpeedBoostController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IKillable>(out var killable) && _speedBoostController.playerBeastMode)
        {

            killable.Kill();
        }
    }
}
