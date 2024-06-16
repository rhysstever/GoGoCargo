using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRegion
{
    private Vector3 boundsStart, boundsEnd;
    private List<GameObject> islands, boats;
    private MapRegion[] neighbors;
    
    public Vector3 BoundsStart { get { return boundsStart; } }
    public Vector3 BoundsEnd { get { return boundsEnd;} }

    public List<GameObject> Islands { get { return islands; } }
    public List<GameObject> Boats { get { return boats; } }

    public MapRegion[] Neighbors { get { return neighbors; } }
    public MapRegion RightNeighbor { get { return neighbors[0]; } }
    public MapRegion DownNeighbor { get { return neighbors[1]; } }
    public MapRegion LeftNeighbor { get { return neighbors[2]; } }
    public MapRegion UpNeighbor { get { return neighbors[3]; } }

    public MapRegion(Vector3 startingBounds, Vector3 endingBounds)
    {
        boundsStart = startingBounds;
        boundsEnd = endingBounds;
        islands = new List<GameObject>();
        boats = new List<GameObject>();
        neighbors = new MapRegion[4];
    }

    public void AddIsland(GameObject island)
    {
        islands.Add(island);
    }

    public void ClearIslands()
    {
        islands.Clear();
    }

    public void AddBoat(GameObject boat)
    {
        boats.Add(boat);
    }

    public void RemoveBoat(GameObject boat)
    {
        if (islands.Contains(boat))
            islands.Remove(boat);
    }

    public void AddNeighbor(int index, MapRegion neighbor)
    {
        neighbors[index] = neighbor;
    }

    public void AddRightNeighbor(MapRegion neighbor)
    {
        AddNeighbor(0, neighbor);
    }

    public void AddDownNeighbor(MapRegion neighbor)
    {
        AddNeighbor(1, neighbor);
    }

    public void AddLeftNeighbor(MapRegion neighbor)
    {
        AddNeighbor(2, neighbor);
    }

    public void AddUpNeighbor(MapRegion neighbor)
    {
        AddNeighbor(3, neighbor);
    }
}
