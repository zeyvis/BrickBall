using System.Collections.Generic;
using UnityEngine;

// T, IManageable arayüzüne sahip herhangi bir MonoBehaviour olmalý
public abstract class BaseManager<T> : MonoBehaviour where T : MonoBehaviour, IManageable
{
    protected readonly HashSet<T> _activeEntities = new();

    public void RegisterEntity(T entity)
    {
        _activeEntities.Add(entity);
    }

    public void UnregisterEntity(T entity)
    {
        _activeEntities.Remove(entity);
    }

    public void StopAllEntities()
    {
        foreach (var entity in _activeEntities)
        {
            if (entity != null)
                entity.StopAction();
        }
    }

    public void ResumeAllEntities()
    {
        foreach (var entity in _activeEntities)
        {
            if (entity != null)
                entity.ResumeAction();
        }
    }
}