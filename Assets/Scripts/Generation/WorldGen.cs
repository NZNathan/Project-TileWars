using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGen : MonoBehaviour {

    //Singleton
    public static WorldGen instance;

    [Header("Tiles")]
    public GameObject grassTile;
    public GameObject waterTile;

    [Header("Environment")]
    public GameObject forest;

    //World Size
    [Header("World Size")]
    public int width = 50;
    public int height = 50;

    //World Variables
    public float seed = 1;
    public float waterLevel = 0.3f;
    [Tooltip("Larger values reduce the height difference between tiles")]
    public int heightDamp = 10;
    public float forestLevel = 0.3f;

    //Tile Size
    public static float tileHalfWidth = 0.16f;
    public static float tileHalfHeight = -0.08f;

    // Use this for initialization
    void Start () {

        //Only one worldgen can exist
        if(instance != null){
            Destroy(instance);
        }

        instance = this;
        this.enabled = false;

        seed = Random.Range(0f, 100000f);
        generate();
    }

    /// <summary>
    /// Returns the noise for that pos minus water level so noise can be used for height (will be between 0 and 1 - waterLevel / 5)
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public float getNoiseHeight(Vector2 pos)
    {
        float noise = Mathf.PerlinNoise(seed + pos.x, seed + pos.y);

        return (noise - waterLevel) / heightDamp;
    }

    [ContextMenu("Generate")] //Can run the method by right clicking class during runtime
	public void generate()
    {
        //Destroy All children
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        //Get new map created to store the tiles
        WorldManager.instance.createNewMap(width,height);

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                float x = i * tileHalfWidth - j * tileHalfWidth;
                float y = i * tileHalfHeight + j * tileHalfHeight;

                Vector2 pos = new Vector2(x,y);
                //Vector2 pos = new Vector2((-i* tileHalfWidth) + (j* tileHalfWidth),0 + (i * tileHalfHeight) + (j * tileHalfHeight));

                float noise = Mathf.PerlinNoise(seed + pos.x, seed + pos.y);
                
                GameObject tile;

                if (noise > waterLevel)
                {
                    tile = Instantiate(grassTile, pos, Quaternion.identity, this.transform);

                    float bioVal = Mathf.PerlinNoise(seed + pos.x + 10000, seed + pos.y + 10000);

                    if (bioVal <= forestLevel)
                    {
                        GameObject forestSpot = Instantiate(forest, pos, Quaternion.identity, this.transform);
                        forestSpot.GetComponent<SpriteRenderer>().size += new Vector2(0, getNoiseHeight(pos));
                        //Add tile to map
                        WorldManager.instance.addTile(MapTile.Type.FOREST, i, j);
                    }
                    else{
                        //Add tile to map
                        WorldManager.instance.addTile(MapTile.Type.GRASS, i, j);
                    }
                }
                else
                {
                    tile = Instantiate(waterTile, pos, Quaternion.identity, this.transform);
                    //Add tile to map
                    WorldManager.instance.addTile(MapTile.Type.WATER, i, j);
                }


                //Adjust the height of the tile based on the noise height variable
                //NOTE: Won't affect water as the tile is not set to slice, so size doesn't affect it
                tile.GetComponent<SpriteRenderer>().size += new Vector2(0, getNoiseHeight(pos));

                //Set the sorting order of tile now as it won't ever move
                tile.GetComponent<SpriteRenderer>().sortingOrder = (int)((tile.transform.position.y * 100) * -1);
            }
        }
    }
}
