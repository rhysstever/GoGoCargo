using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradingPost : Island
{
    [SerializeField]
    private List<ResourceType> scarceResources, commonResources;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    public float GetIslandPrice(ResourceType resource, bool isBuying)
    {
        float price = ResourceManager.instance.Resources[resource].BasePrice;

        if(scarceResources.IndexOf(resource) != -1)
            price *= 3.0f;

        if(commonResources.IndexOf(resource) != -1)
            price /= 2.0f;

        if(isBuying)
            price *= 3.0f;

        return price;
    }
}
