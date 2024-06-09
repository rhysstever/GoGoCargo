using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMovement : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed, bounceRange, bounceSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(GameManager.instance.CurrentMenuState == MenuState.Sailing)
        {
            BobAndRotate();
        }
    }

    private void BobAndRotate()
    {
        // Rotate the arrow
        gameObject.transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);

        // Move the arrow up and down
        // TODO
    }
}
