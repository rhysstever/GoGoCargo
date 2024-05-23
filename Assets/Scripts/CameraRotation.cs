using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            // Rotate the camera around the player, along the Y-axis (circling horizontally), based on the mouse's horizontal movement
            transform.RotateAround(GameManager.instance.Player.transform.position, Vector3.up, rotationSpeed * Input.GetAxis("Mouse X"));
        }
    }
}
