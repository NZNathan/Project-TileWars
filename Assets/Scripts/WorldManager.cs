using UnityEngine;
using System.Collections;

public class WorldManager : MonoBehaviour
{

    public static WorldManager instance;

	//Tile Array
    public MapTile[,] map;
    private int width;
    private int height;

    void Start(){

        //Only one worldgen can exist
        if(instance != null){
            Destroy(instance);
        }

        instance = this;

        //Don't need to run update method so no need to keep enabled
        //this.enabled = false;
    }

    public void createNewMap(int w, int h){

        width = w;
        height = h;

        map = new MapTile[width, height];

    }

    public bool onMap(Vector2 pos)
    {
        Vector2 index = worldPosToIndex(pos.x, pos.y);

        return index.x >= 0 && index.x < width && index.y >= 0 && index.y < height;
    }

    public void addTile(MapTile.Type type, int x, int y){

        MapTile tile = new MapTile(type);

        map[x,y] = tile;
    }

    /// <summary>
    /// Returns the tile at x and y real world coords
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public MapTile getTile(float x, float y){

        Vector2 index = worldPosToIndex(x, y);

        int i1 = (int) index.x;
        int i2 = (int) index.y;

        if (i1 >= 0 && i2 >= 0 && i1 < map.GetLength(0) && i2 < map.GetLength(1)) {
            return map[(int)index.x, (int)index.y];
        }
        else
        {
            return null;
        }
    }

    private Vector2 worldPosToIndex(float x, float y)
    {
        float x2 = ((x / WorldGen.tileHalfWidth + y / WorldGen.tileHalfHeight) / 2);
        float y2 = ((y / WorldGen.tileHalfHeight - (x / WorldGen.tileHalfWidth)) / 2);

        return new Vector2(Mathf.Round(x2), Mathf.Round(y2));
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = BuildingPlacer.mouseToTile();
            //Debug.Log("Tile at: (" + pos.x + "," + pos.y + ") Biome: " + getTile(pos.x, pos.y).getBiome());
        }
    }

}