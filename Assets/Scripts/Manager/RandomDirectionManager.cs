using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDirectionManager : MonoBehaviour
{
    public event System.Action<Vector3, Vector3> OnDirectionsChanged;
    [SerializeField] private Vector3[] queuedDirections = new Vector3[2];

    private void Awake()
    {

        if (queuedDirections == null || queuedDirections.Length != 2)
            queuedDirections = new Vector3[2];

        queuedDirections[0] = GetRandomFlatDirection();
        queuedDirections[1] = GetRandomFlatDirection();
        OnDirectionsChanged?.Invoke(queuedDirections[0], queuedDirections[1]);

    }
    public Vector3 GetRandomFlatDirection()
    {
        int directionIndex = Random.Range(0, 4);

        switch (directionIndex)
        {
            case 0: return Vector3.right;  
            case 1: return Vector3.left;    
            case 2: return Vector3.forward;
            case 3: return Vector3.back;
            default: return Vector3.zero;
        }
    }

    public void UpdateDirections()
    {
        queuedDirections[0] = queuedDirections[1];
        queuedDirections[1] = GetRandomFlatDirection();
        OnDirectionsChanged?.Invoke(queuedDirections[0], queuedDirections[1]);

    }

    public Vector3 GetCurrentDirection() => queuedDirections[0];
    public Vector3 GetNextDirection() => queuedDirections[1];

}
