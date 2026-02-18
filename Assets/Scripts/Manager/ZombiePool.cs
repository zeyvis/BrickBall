using System.Collections.Generic;
using UnityEngine;

public class ZombiePool : MonoBehaviour
{
    [SerializeField] private GameObject _zombiePrefab; 
    [SerializeField] private int _poolSize = 3;

    private Queue<GameObject> _poolQueue = new Queue<GameObject>();
    private Transform _poolContainer;

    public void InitializePool()
    {

        _poolContainer = new GameObject("Zombie_Pool_Container").transform;
        _poolContainer.SetParent(this.transform);

        for (int i = 0; i < _poolSize; i++)
        {
            CreateNewZombieAndEnqueue();
        }
    }

    private void CreateNewZombieAndEnqueue()
    {
        if (_zombiePrefab == null)
        {
            Debug.LogError("Zombie Prefab atanmamýþ!");
            return;
        }

        GameObject zombie = Instantiate(_zombiePrefab, _poolContainer);
        zombie.SetActive(false);
        _poolQueue.Enqueue(zombie);
    }

    public GameObject GetZombie()
    {
        if (_poolQueue.Count == 0)
        {
            CreateNewZombieAndEnqueue();
        }

        GameObject zombie = _poolQueue.Dequeue();
        zombie.SetActive(true);
        return zombie;
    }

    public void ReturnZombie(GameObject zombie)
    {
        zombie.SetActive(false);
        zombie.transform.SetParent(_poolContainer);
        _poolQueue.Enqueue(zombie);
    }
}