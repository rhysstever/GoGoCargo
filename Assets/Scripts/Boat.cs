using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    private Dictionary<Resource, int> cargo;

    // Start is called before the first frame update
    void Start()
    {
        cargo = new Dictionary<Resource, int>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
