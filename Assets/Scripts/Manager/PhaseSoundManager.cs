using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseSoundManager : MonoBehaviour
{

    [SerializeField] private AudioClip _HazardPhaseSound;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource= GetComponent<AudioSource>();
    }
    public void PlayPhaseSound(int phaseSoundNum)
    {
        switch (phaseSoundNum)
        {
            case 0:
                _audioSource.PlayOneShot(_HazardPhaseSound);
                break;

            case 1:
                Debug.Log("Phase 1");
                break;

            case 2:
                Debug.Log("Phase 2");
                break;

            case 3:
                Debug.Log("Phase 3");
                break;

            case 4:
                Debug.Log("Phase 4");
                break;

            case 5:
                Debug.Log("Phase 5");
                break;

            default:
                Debug.Log("Unknown Phase");
                break;
        }
    }
}
