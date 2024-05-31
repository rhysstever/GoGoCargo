using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Island : MonoBehaviour
{
    [SerializeField]
    private GameObject indicator;
    [SerializeField]
    private float interactDistance;
    private bool isClose;
    public bool IsClose { get { return isClose; } }

    // Start is called before the first frame update
    internal void Start()
    {

    }

    // Update is called once per frame
    internal void Update()
    {
        isClose = IsInteractable();
    }

    internal void FixedUpdate()
    {
        
    }

    public bool IsInteractable()
    {
        float distToPlayer = Vector3.Distance(
            gameObject.transform.position,
            GameManager.instance.Player.transform.position);
        indicator.SetActive(distToPlayer < interactDistance);
        return distToPlayer < interactDistance;
    }
}
