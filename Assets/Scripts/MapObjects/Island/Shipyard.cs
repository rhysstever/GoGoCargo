using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shipyard : Island
{
    [SerializeField]
    private List<GameObject> boats;

    // Start is called before the first frame update
    private new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    private new void Update()
    {
        base.Update();
    }
}
