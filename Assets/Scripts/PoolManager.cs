using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager
{
    private static Dictionary<string, LinkedList<GameObject>> poolsDicnionary;
    private static Transform deactivateObjectParent;

    public static void Init (Transform poolObjectContainer)
    {
        deactivateObjectParent = poolObjectContainer;
        poolsDicnionary = new Dictionary<string, LinkedList<GameObject>>();
    }

    public static GameObject getGameObjectFromPool(GameObject prefab)
    {
        if (!poolsDicnionary.ContainsKey(prefab.name))
        {
            poolsDicnionary[prefab.name] = new LinkedList<GameObject>();
        }

        GameObject result;

        if (poolsDicnionary[prefab.name].Count > 0)
        {
            result = poolsDicnionary[prefab.name].First.Value;
            poolsDicnionary[prefab.name].RemoveFirst();
            result.SetActive(true);
            return result;
        }

        result = GameObject.Instantiate(prefab);
        result.name = prefab.name;

        return result;
    }

    public static void putGameObjectPool(GameObject target)
    {
        poolsDicnionary[target.name].AddFirst(target);
        target.transform.parent = deactivateObjectParent;
        target.SetActive(false);
    }
}
