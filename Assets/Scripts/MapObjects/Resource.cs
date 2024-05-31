using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resource
{
    private string name;
    private ResourceType type;
    private int basePrice;
    private Texture image;

    public string Name { get { return name; } }
    public ResourceType Type { get { return type; } }
    public int BasePrice { get { return basePrice; } }
    public Texture Image { get { return image; } }

    public Resource(string name, ResourceType type, int basePrice, Texture image)
    {
        this.name = name;
        this.type = type;
        this.basePrice = basePrice;
        this.image = image;
    }
}
