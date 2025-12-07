using UnityEngine;

public class ForestGrid : MonoBehaviour
{
    public GameObject treePrefab;

    private int[,] forestGrid; // keeps the information about the type of object (empty, tree, ...)
    private int gridSize = 9; // size of a grid = gridSize * gridSize
    private int objectY = 2; // spawning objects placement on Y-axis

    private void Start()
    {
        forestGrid = new int[gridSize, gridSize];

        // grid consists of gridSize * gridSize cells
        for (int x = 0; x < gridSize; x++)
        {
            for (int z = 0; z < gridSize; z++)
            {
                // assign a random number to cells
                // 0 - empty cell
                // 1 - a tree
                forestGrid[x, z] = Random.Range(0, 2); // 2 is not included

                // spwan a tree
                if (forestGrid[x, z] == 1)
                {
                    // spawn an object (x+1 and z+1 in order to exclude 0 to match the surface object)
                    Instantiate(treePrefab, treePrefab.transform.position = new Vector3(x+1, objectY, z+1), Quaternion.identity);
                }
            }
        }
    }
}
