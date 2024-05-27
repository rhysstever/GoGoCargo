using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Island : MonoBehaviour
{
    [SerializeField]
    private List<Resource> scarceResources, commonResources;
    [SerializeField]
    private float interactDistance;
    [SerializeField]
    private float distToPlayer;

    // Start is called before the first frame update
    void Start()
    {
        scarceResources = new List<Resource>();
        commonResources = new List<Resource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    public bool IsInteractable()
    {
        distToPlayer = Vector2.Distance(
            gameObject.transform.position,
            GameManager.instance.Player.transform.position);
        return Vector2.Distance(
            gameObject.transform.position, 
            GameManager.instance.Player.transform.position) < interactDistance;
    }

    public int GetIslandPrice(Resource resource)
    {
        if(scarceResources.IndexOf(resource) != -1)
            return ResourceManager.instance.ResourcePrices[resource] * 2;

        if(commonResources.IndexOf(resource) != -1)
            return ResourceManager.instance.ResourcePrices[resource] / 2;

        return ResourceManager.instance.ResourcePrices[resource];
    }
}
