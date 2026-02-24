using UnityEngine;
using System.Collections.Generic;

public class HazardDropPhase : IGamePhase
{
    public float Duration => 6f; 

    private Transform _container;
    private HazardPool _pool;
    private GameObject _spawnedHazard; 


    public HazardDropPhase(Transform container, HazardPool pool)
    {
        _container = container;
        _pool = pool;
    }

    public void Enter()
    {

        List<Transform> aliveCubes = new List<Transform>();
        for (int i = 0; i < _container.childCount; i++)
        {
            Transform child = _container.GetChild(i);
            if (child.GetComponent<FallingCube>() == null)
            {
                aliveCubes.Add(child);
            }
        }

        if (aliveCubes.Count == 0) return; 


        Transform targetCube = aliveCubes[Random.Range(0, aliveCubes.Count)];


        _spawnedHazard = _pool.GetRandomHazard();


        if (_spawnedHazard != null)
        {
            _spawnedHazard.transform.position = targetCube.position + Vector3.up * 10f;
            float randomY = Random.Range(0, 4) * 90f;


            _spawnedHazard.transform.rotation = Quaternion.Euler(-90, randomY, 0);
        }
        StatueHealhtManager statueHealth = _spawnedHazard.GetComponent<StatueHealhtManager>();
        if (statueHealth != null)
        {
            statueHealth.Initialize(_pool);
        }
        else
        {
            Debug.LogWarning("HazardDropPhase: StatueHealhtManager not found in the Statue");
        }
    }

    public void Update()
    {
    }

    public void Exit()
    {


            _spawnedHazard = null;
        
    }
}