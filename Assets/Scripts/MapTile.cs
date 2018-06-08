using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTile
{

    public enum Type { WATER, GRASS, FOREST };

    //Variables
    private bool buildable = false;
    private Type biome;
    private Building buildingOnTile = null;

    // Use this for initialization
    public MapTile(Type biome)
    {
        this.biome = biome;
        buildable = (biome == Type.GRASS) ? true : false;
    }

    public void buildOnTile(Building building)
    {
        buildingOnTile = building;
        buildable = false;
    }

    /// <summary>
    /// Empties the tile, destroying any buildings on it, or removing any biome on it so it becomes a grass tile. It then becomes buildable
    /// </summary>
    public void emptyTile()
    {

        if (buildingOnTile != null)
        {
            buildingOnTile.destroyBuilding();
            buildingOnTile = null;
        }
        else if (biome == Type.FOREST)
        {
            biome = Type.GRASS;
        }

        buildable = true;
    }

    public bool isBuildable()
    {
        return buildable;
    }

    public Type getBiome()
    {
        return biome;
    }
}
