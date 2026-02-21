using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerEnemy : MonoBehaviour
{
    private ContinueButton _continueButton;

    private void Start()
    {
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
        if (gameManager != null)
        {
            _continueButton = gameManager.GetComponent<ContinueButton>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _continueButton.RegisterDeath(this.gameObject);
        }
    }
}