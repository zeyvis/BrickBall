using System.Collections;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    private IGamePhase _currentPhase;
    private Transform _targetContainer;

    [SerializeField] private ZombiePool _zombiePool;
    [SerializeField] private HazardPool _hazardPool;

    private Coroutine _gameLoopCoroutine;//test için
    private void Start()
    {

        if (_zombiePool != null)
            _zombiePool.InitializePool();
        else
            Debug.LogError("PhaseManager:ZombiePool not found");

        if (_hazardPool != null) _hazardPool.InitializePool();
        else Debug.LogError("PhaseManager:HazardPool not found");

        _gameLoopCoroutine = StartCoroutine(GameLoop());//test için
        //StartCoroutine(GameLoop());
    }

    private void Update()
    {
        if (_currentPhase != null) _currentPhase.Update();



        // T tuþuna basýnca istediðin fazý oynat
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("TEST:  selected phase started");
            StopNormalLoop();
            SwitchPhase(new ZombiePhase(_zombiePool, _targetContainer));
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
        if (Random.value < 0.33f)
        {
            Debug.Log("tilt phase ");
            SwitchPhase(new TiltPhase(_targetContainer, this));
        }
        else if(Random.value<0.66f)
        {
            Debug.Log("drop phase ");
            SwitchPhase(new DropPhase(_targetContainer));
        }
        else if(Random.value<0.75f)
        {
            Debug.Log("zombie phase");
            SwitchPhase(new ZombiePhase(_zombiePool, _targetContainer));
        }
        else
        {
            Debug.Log("hazard drop phase");

            SwitchPhase(new HazardDropPhase(_targetContainer, _hazardPool));
        }
    }

    public void SwitchPhase(IGamePhase newPhase)
    {
        if (_currentPhase != null) _currentPhase.Exit();
        _currentPhase = newPhase;
        if (_currentPhase != null) _currentPhase.Enter();
    }
}