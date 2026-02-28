using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPool : MonoBehaviour
{
    [SerializeField] private GameObject[] _ghostPrefabs;
    [SerializeField] private int _poolSizePerPrefab = 5;

    private List<GameObject> _pool = new List<GameObject>();

    private void Start()
    {
        InitializePool();
    }

    public void InitializePool()
    {
        _pool.Clear();

        foreach (GameObject prefab in _ghostPrefabs)
        {
            for (int i = 0; i < _poolSizePerPrefab; i++)
            {
                GameObject obj = Instantiate(prefab, transform);

                SetupGhost(obj);

                obj.SetActive(false);
                _pool.Add(obj);
            }
        }
    }

    public GameObject GetRandomGhost()
    {

        List<GameObject> availableGhosts = _pool.FindAll(obj => !obj.activeInHierarchy);

        if (availableGhosts.Count > 0)
        {
            GameObject selected = availableGhosts[Random.Range(0, availableGhosts.Count)];


            SetupGhost(selected);

            selected.SetActive(true);
            return selected;
        }

        Debug.LogWarning("GhostPool: Havuzda boþ hayalet kalmadý!");
        return null;
    }

    private void SetupGhost(GameObject ghost)
    {
        GhostHealth health = ghost.GetComponent<GhostHealth>();
        if (health != null)
        {
            health.Initialize(this); 
        }
    }

    public void ReturnGhost(GameObject ghost)
    {
        if (ghost != null)
        {
            ghost.SetActive(false);
        }
    }
}