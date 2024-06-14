using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : MonoBehaviour
{
    public GameObject source;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.position.y < -10.0f)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject != null
            && collision.gameObject != source)
        {
            Boat boat = collision.gameObject.GetComponent<Boat>();
            if(boat != null)
                boat.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
