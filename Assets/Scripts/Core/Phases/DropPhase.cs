using UnityEngine;
using System.Collections.Generic;

public class DropPhase : IGamePhase
{
    private Transform _container;
    private float _timer;
   

    private const int kTotalDrops = 5;
    private const float kDropInterval = 3f; 

    private float _dropTimer;
    private int _droppedCount;
    private List<FallingCube> _activeCubes = new List<FallingCube>();
    public float Duration => 15f;

    private readonly SkyboxColorTransitioner _skybox;
    public DropPhase(Transform container,SkyboxColorTransitioner skybox)
    {
        _container = container;
        _skybox = skybox;
    }

    public void Enter()
    {
        _skybox.TransitionToHex("#B91C1C", 2f);

        _activeCubes.Clear();
        _dropTimer = 0f;
        _droppedCount = 0;
    }

    public void Update()
    {



        if (_container == null) return;
        if (_droppedCount >= kTotalDrops) return;

        _dropTimer += Time.deltaTime;

        if (_dropTimer >= kDropInterval)
        {
            _dropTimer -= kDropInterval;

            SelectCubeToFall();
            _droppedCount++;
        }
    }

    public void Exit()
    {
        _skybox.TransitionToHex("#245A9B", 1.5f);
        _activeCubes.Clear();

    }

    private void SelectCubeToFall()
    {
        if (_container.childCount == 0) return;

        int randomIndex = Random.Range(0, _container.childCount);
        Transform selectedCube = _container.GetChild(randomIndex);

        if (selectedCube.GetComponent<FallingCube>() != null) return;

        FallingCube script = selectedCube.gameObject.AddComponent<FallingCube>();
        _activeCubes.Add(script);
    }
}