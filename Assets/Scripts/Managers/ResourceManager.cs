using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ResourceType
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

    private Dictionary<ResourceType, Resource> resources;
    public Dictionary<ResourceType, Resource> Resources { get { return resources; } }

    [SerializeField]
    private Texture fishImage, meatImage, stoneImage, woodImage;

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
        resources = new Dictionary<ResourceType, Resource>();
        resources.Add(ResourceType.Fish, new Resource("Fish", ResourceType.Fish, 5, fishImage));
        resources.Add(ResourceType.Meat, new Resource("Meat", ResourceType.Meat, 7, meatImage));
        resources.Add(ResourceType.Stone, new Resource("Stone", ResourceType.Stone, 4, stoneImage));
        resources.Add(ResourceType.Wood, new Resource("Wood", ResourceType.Wood, 3, woodImage));
    }
}
