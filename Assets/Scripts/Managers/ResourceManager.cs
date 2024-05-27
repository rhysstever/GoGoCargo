using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Resource
{
    Fish,
    Meat,
    Stone,
    Wood
}

public class ResourceManager : MonoBehaviour
{
    #region Singleton Code
    // A public reference to this script
    public static ResourceManager instance = null;

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

    private Dictionary<Resource, int> resourcePrices;
    public Dictionary<Resource, int> ResourcePrices { get { return resourcePrices; } }

    // Start is called before the first frame update
    void Start()
    {
        SetupResourcePrices();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetupResourcePrices()
    {
        resourcePrices = new Dictionary<Resource, int>();
        resourcePrices.Add(Resource.Fish, 5);
        resourcePrices.Add(Resource.Meat, 7);
        resourcePrices.Add(Resource.Stone, 4);
        resourcePrices.Add(Resource.Wood, 3);
    }
}
