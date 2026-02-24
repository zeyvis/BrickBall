using UnityEngine;
using System.Collections.Generic;

public class DropPhase : IGamePhase
{
    private Transform _container;
    private float _timer;
    private float _selectionInterval = 0.5f;
    private float _timeActive = 0f;
    private float _spawnDurationLimit = 2.5f;

    private List<FallingCube> _activeCubes = new List<FallingCube>();
    public float Duration => 8f;
    public DropPhase(Transform container)
    {
        _container = container;
    }

    public void Enter()
    {
        _activeCubes.Clear();
        _timeActive = 0f;
    }

    public void Update()
    {
        if (_container == null) return;

        _timeActive += Time.deltaTime;

        if (_timeActive < _spawnDurationLimit)
        {
            _timer += Time.deltaTime;
            if (_timer > _selectionInterval)
            {
                SelectCubeToFall();
                _timer = 0;
            }
        }
    }

    public void Exit()
    {
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