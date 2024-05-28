using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Island : MonoBehaviour
{
    [SerializeField]
    private List<ResourceType> scarceResources, commonResources;
    [SerializeField]
    private float interactDistance;
    private bool isClose;
    public bool IsClose { get { return isClose; } }

    // Start is called before the first frame update
    void Start()
    {
        scarceResources = new List<ResourceType>();
        commonResources = new List<ResourceType>();
    }

    // Update is called once per frame
    void Update()
    {
        isClose = IsInteractable();
    }

    private void FixedUpdate()
    {
        
    }

    public bool IsInteractable()
    {
        float distToPlayer = Vector3.Distance(
            gameObject.transform.position,
            GameManager.instance.Player.transform.position);
        gameObject.transform.GetChild(0).gameObject.SetActive(distToPlayer < interactDistance);
        return distToPlayer < interactDistance;
    }

    public float GetIslandPrice(ResourceType resource, bool isBuying)
    {
        float price = ResourceManager.instance.Resources[resource].BasePrice;

        if(scarceResources.IndexOf(resource) != -1)
            price *= 2.0f;

        if(commonResources.IndexOf(resource) != -1)
            price /= 2.0f;

        if(isBuying)
            price *= 3.0f;

        return price;
    }
}
