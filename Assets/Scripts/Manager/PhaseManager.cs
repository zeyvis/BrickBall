using System.Collections;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    private IGamePhase _currentPhase;
    private Transform _targetContainer;

    [SerializeField] private ZombiePool _zombiePool;
    private void Start()
    {

        if (_zombiePool != null)
            _zombiePool.InitializePool();
        else
            Debug.LogError("ZombiePool atanmadý!");
        StartCoroutine(GameLoop());
    }

    private void Update()
    {
        if (_currentPhase != null) _currentPhase.Update();

       
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

            yield return new WaitForSeconds(8f);

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
        else
        {
            Debug.Log("zombie phase");
            SwitchPhase(new ZombiePhase(_zombiePool, _targetContainer));
        }
    }

    public void SwitchPhase(IGamePhase newPhase)
    {
        if (_currentPhase != null) _currentPhase.Exit();
        _currentPhase = newPhase;
        if (_currentPhase != null) _currentPhase.Enter();
    }
}