using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    [SerializeField]
    private Transform traderParent, pirateParent;

    #region Singleton Code
    // A public reference to this script
    public static NPCManager instance = null;

    // Awake is called even before start
    private void Awake()
    {
        // If the reference for this script is null, assign it this script
        if(instance == null)
            instance = this;
        // If the reference is to something else (it already exists)
        // than this is not needed, thus destroy it
        else if(instance != this)
            Destroy(gameObject);
    }
    #endregion Singleton Code

    // Start is called before the first frame update
    void Start()
    {
        SpawnTrader(ResourceManager.instance.Boats[2], new Vector2(15.0f, 5.0f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnTrader(
        GameObject boat,
        Vector2 position)
    {
        Dictionary<ResourceType, int> newCargoHold = new Dictionary<ResourceType, int>();
        SpawnTrader(boat, position, newCargoHold);
    }

    public void SpawnTrader(
    GameObject boat,
    Vector2 position,
    Dictionary<ResourceType, int> currentCargo)
    {
        GameObject newBoat = Instantiate(
            boat, 
            new Vector3(position.x, boat.transform.position.y, position.y), 
            Quaternion.identity, 
            traderParent);
        newBoat.tag = "Trader";
        newBoat.GetComponent<Boat>().AddCargo(currentCargo);
        Destroy(newBoat.GetComponent<BoatMovement>());
        newBoat.AddComponent<TraderMovement>();
        newBoat.GetComponent<TraderMovement>().FindNewDestination();
    }

    public void SpawnPirate(
        GameObject boat,
        Vector2 position)
    {
        GameObject newBoat = Instantiate(
            boat,
            new Vector3(position.x, boat.transform.position.y, position.y),
            Quaternion.identity,
            pirateParent);
        newBoat.tag = "Pirate";
        Destroy(newBoat.GetComponent<BoatMovement>());
        newBoat.AddComponent<PirateMovement>();
    }
}
