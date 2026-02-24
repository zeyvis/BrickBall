using UnityEngine;
using System.Collections.Generic;

public class HazardPool : MonoBehaviour
{
    [SerializeField] private GameObject[] _hazardPrefabs;
    [SerializeField] private int _poolSizePerPrefab = 3; 

    private List<GameObject> _pool = new List<GameObject>();

    public void InitializePool()
    {
       
        _pool.Clear();

        
        foreach (GameObject prefab in _hazardPrefabs)
        {
            for (int i = 0; i < _poolSizePerPrefab; i++)
            {
                GameObject obj = Instantiate(prefab, transform);

                
                if (obj.GetComponent<HazardMovement>() == null)
                {
                    obj.AddComponent<HazardMovement>();
                }

                obj.SetActive(false); 
                _pool.Add(obj);
            }
        }
    }

    
    public GameObject GetRandomHazard()
    {
        List<GameObject> availableHazards = new List<GameObject>();

       
        foreach (GameObject obj in _pool)
        {
            if (!obj.activeInHierarchy)
            {
                availableHazards.Add(obj);
            }
        }

        
        if (availableHazards.Count > 0)
        {
            GameObject selected = availableHazards[Random.Range(0, availableHazards.Count)];
            selected.SetActive(true);
            return selected;
        }

        Debug.LogWarning("HazardPool: Pool is empty");
        return null;
    }

    
    public void ReturnHazard(GameObject hazard)
    {
        if (hazard != null)
        {
            hazard.SetActive(false);
        }
    }
}