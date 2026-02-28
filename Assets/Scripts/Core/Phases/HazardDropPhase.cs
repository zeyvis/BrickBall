using UnityEngine;
using System.Collections.Generic;

public class HazardDropPhase : IGamePhase
{
    public float Duration => 15f;
    private const float kSpawnInterval = 5f;
    private float _spawnTimer;

    private bool _playedIntro; 

    private Transform _container;
    private HazardPool _pool;
    private GameObject _spawnedHazard;
    private DirectionalCameraController _camController;
    private PhaseSoundManager _phaseSoundManager;



    private readonly SkyboxColorTransitioner _skybox;


   


    public HazardDropPhase(Transform container, HazardPool pool, DirectionalCameraController camController,PhaseSoundManager phaseSoundManager,SkyboxColorTransitioner skybox)
    {
        _container = container;
        _pool = pool;
        _camController = camController;
        _phaseSoundManager = phaseSoundManager;
        _skybox = skybox;
    }

    public void Enter()
    {
        _skybox.TransitionToHex("#7F1D1D", 2f);

        _spawnTimer = 0f;
        _playedIntro = false;

       

        
        PlayIntroOnce();
    }

    public void Update()
    {

        _spawnTimer += Time.deltaTime;

        if (_spawnTimer >= kSpawnInterval)
        {
            _spawnTimer -= kSpawnInterval;
            SpawnHazard();
        }
    }

    public void Exit()
    {

        _skybox.TransitionToHex("#245A9B", 1.5f);
        _spawnedHazard = null;
        
    }
    private void PlayIntroOnce()
    {
        if (_playedIntro) return;
        _playedIntro = true;

        if (_camController != null) _camController.ShakeCamera(0.4f, 0.2f);
        if (_phaseSoundManager != null) _phaseSoundManager.PlayPhaseSound(0);
    }

    private void SpawnHazard()
    {
        if (_container == null || _pool == null) return;

        List<Transform> aliveCubes = new List<Transform>();
        for (int i = 0; i < _container.childCount; i++)
        {
            Transform child = _container.GetChild(i);
            if (child.GetComponent<FallingCube>() == null)
                aliveCubes.Add(child);
        }

        if (aliveCubes.Count == 0) return;

        Transform targetCube = aliveCubes[Random.Range(0, aliveCubes.Count)];
        GameObject hazard = _pool.GetRandomHazard();
        if (hazard == null) return;

        hazard.transform.position = targetCube.position + Vector3.up * 10f;
        float randomY = Random.Range(0, 4) * 90f;
        hazard.transform.rotation = Quaternion.Euler(-90, randomY, 0);

        StatueHealhtManager statueHealth = hazard.GetComponent<StatueHealhtManager>();
        if (statueHealth != null) statueHealth.Initialize(_pool);
        else Debug.LogWarning("HazardDropPhase: StatueHealhtManager not found in the Statue");

        _spawnedHazard = hazard;


        PlayIntroOnce();
    }
}