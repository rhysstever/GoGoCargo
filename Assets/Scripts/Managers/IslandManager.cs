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
    private int islandDensity;
    public int IslandDensity { get { return islandDensity; } }

    [SerializeField]
    private List<GameObject> islandPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        if(islandDensity <= 0)
            islandDensity = 50;

        SpawnIslands();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnIslands()
    {
        ClearIslands();

        for(int i = islandDensity * 2; i < water.transform.localScale.x - (islandDensity * 2); i += islandDensity)
        {
            for(int j = islandDensity * 2; j < water.transform.localScale.z - (islandDensity * 2); j += islandDensity)
            {
                GameObject randomIsland = islandPrefabs[Random.Range(0, islandPrefabs.Count)];
                float randomPosX = RandomCondensedRange(islandDensity, 0.2f) + i - water.transform.localScale.x / 2;
                float randomPosZ = RandomCondensedRange(islandDensity, 0.2f) + j - water.transform.localScale.z / 2;

                GameObject newIsland = Instantiate(randomIsland, randomIsland.transform.position, Quaternion.identity, islandParent.transform);
                newIsland.transform.position = new Vector3(randomPosX, newIsland.transform.position.y, randomPosZ);
            }
        }
    }

    private void ClearIslands()
    {
        for(int i = islandParent.transform.childCount - 1; i >= 0; i--)
            GameObject.Destroy(islandParent.transform.GetChild(i).gameObject);
    }

    private float RandomCondensedRange(int space, float percentCondensed)
    {
        float min = space * percentCondensed;
        return Random.Range(min, space - min);
    }

    public float GetBounds()
    {
        return (water.transform.localScale.x - islandDensity) / 2;
    }
}
