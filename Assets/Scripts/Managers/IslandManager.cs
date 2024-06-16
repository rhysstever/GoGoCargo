using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandManager : MonoBehaviour
{
    #region Singleton Code
    // A public reference to this script
    public static IslandManager instance = null;

    // Awake is called even before start
    private void Awake()
    {
        // If the reference for this script is null, assign it this script
        if(instance == null)
            instance = this;
        // If the reference is to something else (it already exists)
        // than this is not needed, thus destroy it
        else if(instance != this)
            Destroy(gameObject);
    }
    #endregion Singleton Code

    [SerializeField]
    private GameObject water;

    [SerializeField]
    private Transform islandParent;
    public GameObject IslandParent { get { return islandParent.gameObject; } }

    [SerializeField]
    private float islandSpawnPosBufferPercentage;

    [SerializeField]
    private int mapRegionSize, islandDensity;

    [SerializeField]
    private List<GameObject> islandPrefabs;

    private MapRegion[,] mapRegions;

    // Start is called before the first frame update
    void Start()
    {
        if(islandSpawnPosBufferPercentage <= 0.0f)
            islandSpawnPosBufferPercentage = 5.0f;

        if(mapRegionSize <= 0.0f)
            mapRegionSize = 200;

        if(islandDensity <= 0)
            islandDensity = 1;

        SetupMapRegions();
        SpawnIslands();
    }

    private void SetupMapRegions()
    {
        int mapRegionsCountX = (int)water.transform.localScale.x / mapRegionSize;
        int mapRegionsCountZ = (int)water.transform.localScale.x / mapRegionSize;
        mapRegions = new MapRegion[mapRegionsCountX, mapRegionsCountZ];

        // Setup Map Regions
        for(int i = 0; i < mapRegions.GetLength(0); i++)
        {
            for(int j = 0; j < mapRegions.GetLength(1); j++)
            {
                Vector3 startPosition = new Vector3(
                    i * mapRegionSize - MapOffset().x, 
                    0, 
                    j * mapRegionSize - MapOffset().z);
                Vector3 endPosition = new Vector3(
                    (i + 1) * mapRegionSize - MapOffset().x, 
                    0, 
                    (j + 1) * mapRegionSize - MapOffset().z);

                mapRegions[i, j] = new MapRegion(startPosition, endPosition);
                if(i > 0)
                {
                    mapRegions[i, j].AddDownNeighbor(mapRegions[i - 1, j]);
                    mapRegions[i - 1, j].AddUpNeighbor(mapRegions[i, j]);
                }

                if(j > 0)
                {
                    mapRegions[i, j].AddLeftNeighbor(mapRegions[i, j - 1]);
                    mapRegions[i, j - 1].AddRightNeighbor(mapRegions[i, j]);
                }
            }
        }
    }

    private void SpawnIslands()
    {
        // Clear islands
        for(int i = islandParent.transform.childCount - 1; i >= 0; i--)
            Destroy(islandParent.transform.GetChild(i).gameObject);

        for(int i = 1; i < mapRegions.GetLength(0) - 1; i++)
        {
            for(int j = 1; j < mapRegions.GetLength(1) - 1; j++)
            {
                mapRegions[i, j].ClearIslands();

                for(int k = 0; k < islandDensity; k++)
                {
                    GameObject randomIslandPrefab = islandPrefabs[Random.Range(0, islandPrefabs.Count)];

                    Vector3 randomPositionInRegion = new Vector3(
                        RandomCondensedRange(mapRegions[i, j].BoundsStart.x, mapRegions[i, j].BoundsEnd.x, islandSpawnPosBufferPercentage),
                        randomIslandPrefab.transform.position.y,
                        RandomCondensedRange(mapRegions[i, j].BoundsStart.z, mapRegions[i, j].BoundsEnd.z, islandSpawnPosBufferPercentage)
                        );

                    GameObject newIsland = Instantiate(randomIslandPrefab, randomPositionInRegion, Quaternion.identity, islandParent.transform);
                    mapRegions[i, j].AddIsland(newIsland);
                }
            }
        }
    }

    private Vector3 MapOffset()
    {
        return new Vector3(
            water.transform.localScale.x / 2,
            0,
            water.transform.localScale.z / 2);
    }

    private float RandomCondensedRange(float min, float max, float bufferPercentage)
    {
        float size = max - min;
        float bufferSize = size * bufferPercentage / 100;
        return Random.Range(min + bufferSize, max - bufferSize);
    }

    public MapRegion FindCurrentRegion(Vector3 position)
    {
        float regionX = (position.x + MapOffset().x) / mapRegionSize;
        float regionZ = (position.z + MapOffset().z) / mapRegionSize;
        return mapRegions[(int)regionX, (int)regionZ];
    }
}
