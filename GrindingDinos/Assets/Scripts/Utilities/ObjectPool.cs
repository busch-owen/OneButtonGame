using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    // This checks the name of the prefab being spawned.
    public string LookUpString;
    public List<GameObject> InactiveObjects = new List<GameObject>();
}

public class PooledObjectManager : MonoBehaviour
{
    public static List<ObjectPool> ObjectPools = new List<ObjectPool>();

    // this method spawns the object in question given the aprams on the method and it also null checks, neat!
    public static GameObject SpawnObject(GameObject objToSpawn, Vector3 spawnPos, Quaternion spawnRotation)
    {
        ObjectPool pool = ObjectPools.Find(p => p.LookUpString == objToSpawn.name);

        if (pool == null)
        {
            pool = new ObjectPool() { LookUpString = objToSpawn.name };
            ObjectPools.Add(pool);
        }

        GameObject spawnableObj = null;
        foreach (GameObject obj in pool.InactiveObjects)
        {
            if (obj != null)
            {
                spawnableObj = obj;
                break;
            }
        }

        if (spawnableObj == null)
        {
            spawnableObj = Instantiate(objToSpawn, spawnPos, spawnRotation);
        }
        else
        {
            spawnableObj.transform.position = spawnPos;
            spawnableObj.transform.rotation = spawnRotation;
            pool.InactiveObjects.Remove(spawnableObj);
            spawnableObj.SetActive(true);
        }

        return spawnableObj;
    }

    public static void ReturnObjectToPool(GameObject obj)
    {
        // This line removes the "(Clone)" from the name of the object being spawned for the check below.
        string poolName = obj.name.Substring(0, obj.name.Length - 7);

        ObjectPool pool = ObjectPools.Find(p => p.LookUpString == poolName);

        if (pool == null)
        {
            Debug.Log($"Attempted to release an unpooled object: {obj.name}");
        }
        else
        {
            obj.SetActive(false);
            pool.InactiveObjects.Add(obj);
        }
    }
}
