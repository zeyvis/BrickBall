using System.Collections;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    private IGamePhase _currentPhase;
    private Transform _targetContainer;

    [SerializeField] private ZombiePool _zombiePool;
    [SerializeField] private HazardPool _hazardPool;
    [SerializeField] private GhostPool _ghostPool;
    [SerializeField] private DirectionalCameraController _camController;
    [SerializeField] private PhaseSoundManager _phaseSoundManager;

    [SerializeField] private SkyboxColorTransitioner _skyboxTransitioner;

    private Coroutine _gameLoopCoroutine;//test için
    private void Start()
    {

        if (_zombiePool != null)
            _zombiePool.InitializePool();
        else
            Debug.LogError("PhaseManager:ZombiePool not found");

        if (_hazardPool != null) _hazardPool.InitializePool();
        else Debug.LogError("PhaseManager:HazardPool not found");

        if (_ghostPool != null) _ghostPool.InitializePool();

        _gameLoopCoroutine = StartCoroutine(GameLoop());//test için
        //StartCoroutine(GameLoop());
    }

    private void Update()
    {
        if (_currentPhase != null) _currentPhase.Update();



        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("TEST: Ghost Phase started");
            StopNormalLoop();
            SwitchPhase(new ZombiePhase(_zombiePool, _targetContainer, _skyboxTransitioner));
        }



    }
    // Test için
    private void StopNormalLoop()
    {
        if (_gameLoopCoroutine != null)
        {
            StopCoroutine(_gameLoopCoroutine);
            _gameLoopCoroutine = null;
            Debug.Log("Automatic game loop has been stopped for testing");
        }
    }
    private IEnumerator GameLoop()
    {
        while (_targetContainer == null)
        {
            GameObject foundObj = GameObject.Find("Platform_Container");
            if (foundObj != null) _targetContainer = foundObj.transform;
            else yield return null;
        }

        Debug.Log("game started");
        yield return new WaitForSeconds(2f);

        while (true)
        {
            
            PickAndStartRandomPhase();

            
            float waitTime = _currentPhase.Duration;
            yield return new WaitForSeconds(waitTime);

            Debug.Log("time up reseting");
            SwitchPhase(null);

            // faz molasý 
            yield return new WaitForSeconds(4f);
        }
    }

    private void PickAndStartRandomPhase()
    {
        float roll = Random.value;
        if (roll < 0.20f)
        {
            Debug.Log("tilt phase ");
            SwitchPhase(new TiltPhase(_targetContainer, this,_skyboxTransitioner));
        }
        else if (roll < 0.40f) 
        {
            Debug.Log("drop phase ");
            SwitchPhase(new DropPhase(_targetContainer, _skyboxTransitioner));
        }
        else if(roll <0.60f)
        {
            Debug.Log("zombie phase");
            SwitchPhase(new ZombiePhase(_zombiePool, _targetContainer, _skyboxTransitioner));
        }
        else if(roll < 0.80f)
        {
            Debug.Log("hazard drop phase");

            SwitchPhase(new HazardDropPhase(_targetContainer, _hazardPool,_camController,_phaseSoundManager, _skyboxTransitioner));
        }
        else 
        {
            Debug.Log("ghost phase");
            SwitchPhase(new GhostPhase(_ghostPool, _targetContainer, _skyboxTransitioner));
        }
    }

    public void SwitchPhase(IGamePhase newPhase)
    {
        if (_currentPhase != null) _currentPhase.Exit();
        _currentPhase = newPhase;
        if (_currentPhase != null) _currentPhase.Enter();
    }
}