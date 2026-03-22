using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Tilemaps;
using System.Linq;

public class ObjectSpawner : MonoBehaviour
{
    //public Tilemap tilemap;
    public GameObject MosquitoPrefab;
    public int maxObjects = 20; //max amount of mosquitos 
    public float spawnInterval = 0.5f;

    public Transform[] spawnPoints;
    private List<Vector3> validSpawnPositions = new List<Vector3>();
    private List<GameObject> spawnObjects = new List<GameObject>();
    private bool isSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
        GatherValidPositions();
        StartCoroutine(SpawnObjectsIfNeeded());
    }

    private int ActiveObjectCount()
    {
        spawnObjects.RemoveAll(item => item == null);
        return spawnObjects.Count;
    }


    private IEnumerator SpawnObjectsIfNeeded()
    {
        isSpawning = true;
        GatherValidPositions();
        while (ActiveObjectCount() < maxObjects)
        {
            SpawnObject();
            yield return new WaitForSeconds(spawnInterval);
        }
        isSpawning = false;
    }
    private bool PositionHasObject(Vector3 positionToCheck)
    {
        return spawnObjects.Any(checkObj => checkObj && Vector3.Distance(checkObj.transform.position, positionToCheck) < 1.0f);
    }

    private void SpawnObject()
    {
        if(validSpawnPositions.Count == 0) return;

        Vector3 spawnPosition = Vector3.zero;
        bool validPositionFound =false;

        while(!validPositionFound && validSpawnPositions.Count > 0)
        {
            int randomIndex = Random.Range(0, validSpawnPositions.Count);
            Vector3 potentialPosition = validSpawnPositions[randomIndex];
            Vector3 leftPosition = potentialPosition + Vector3.left;
            Vector3 rightPosition = potentialPosition + Vector3.right;

            if(!PositionHasObject(leftPosition) && !PositionHasObject(rightPosition))
            {
                spawnPosition = potentialPosition;
                validPositionFound = true;
            }

            validSpawnPositions.RemoveAt(randomIndex);

        }

        if(validPositionFound)
        {
            GameObject gameObject = Instantiate(MosquitoPrefab, spawnPosition, Quaternion.identity);
            spawnObjects.Add(gameObject);
        }


    }

    private void GatherValidPositions()
    {
        validSpawnPositions.Clear();
        foreach (Transform point in spawnPoints)
        {
            validSpawnPositions.Add(point.position);
        }
    }

    /*
    private void GatherValidPositions()
    {
        validSpawnPositions.Clear();
        BoundsInt boundsInt = tilemap.cellBounds; //shape of tilemap 
        TileBase[] allTiles = tilemap.GetTilesBlock(boundsInt);
        Vector3 start = tilemap.CellToWorld(new Vector3Int (boundsInt.xMin, boundsInt.yMin, 0));

        for (int x = 0; x < boundsInt.size.x; x++)
        {
            for (int y = 0; y < boundsInt.size.y; y++)
            {
                TileBase tile = allTiles[x + y * boundsInt.size.x];
                if(tile != null)
                {
                    Vector3 place = start + new Vector3 (x + 0.5f, y + 2f, 0);
                    validSpawnPositions.Add(place);
                }
            }
        }

    }*/
}
