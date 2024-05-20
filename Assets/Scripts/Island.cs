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

public class Island : MonoBehaviour
{
    [SerializeField]
    private List<Resource> scarceResources, commonResources;

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
}
