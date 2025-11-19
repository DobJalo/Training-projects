using UnityEngine;

public class Spawnrandomobjects : MonoBehaviour
{
    public GameObject spawningObj; // prefab of the object that will be spawned
    public Vector3[] spawnPoints; // assign spawn points in the inspector

    private int spawnCount = 0;

    void Start()
    {
        // check how many spawn points are there
        spawnCount = spawnPoints.Length;

        while (spawnCount > 0)
        {
            // spawn an object from prefab
            Instantiate(spawningObj, spawningObj.transform.position = spawnPoints[spawnCount - 1], Quaternion.identity);

            spawnCount--;
        }
    }
}
