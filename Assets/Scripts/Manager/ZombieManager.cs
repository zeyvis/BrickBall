using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    
    private readonly List<ZombieAI> _activeZombies = new();
   
   
    public void RegisterZombie(ZombieAI zombie)
    {
        if (!_activeZombies.Contains(zombie))
            _activeZombies.Add(zombie);
    }

    public void UnregisterZombie(ZombieAI zombie)
    {
        if (_activeZombies.Contains(zombie))
            _activeZombies.Remove(zombie);
    }
    public void StopAllZombiesFromManager()
    {
        for (int i = _activeZombies.Count - 1; i >= 0; i--)
        {
            var zombie = _activeZombies[i];
            if (zombie != null)
                zombie.StopZombie();
        }
    }
    public void ResumeAllZombiesFromManager()
    {
        for (int i = _activeZombies.Count - 1; i >= 0; i--)
        {
            var zombie = _activeZombies[i];
            if (zombie != null)
                zombie.ResumeZombie();
        }
    }

}
