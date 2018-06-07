using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGen : MonoBehaviour {

    //Singleton
    public static WorldGen instance;

    //Tiles
    [Header("Tiles")]
    public GameObject grassTile;
    public GameObject waterTile;

    //World Size
    [Header("World Size")]
    public int width = 50;
    public int height = 50;

    //World Variables
    public float seed = 1;
    public float waterLevel = 0.3f;
    [Tooltip("Larger values reduce the height difference between tiles")]
    public int heightDamp = 10;

    //Tile Size
    public static float tileWidth = 0.16f;
    public static float tileHeight = -0.08f;

    // Use this for initialization
    void Start () {

        instance = this;

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

    [ContextMenu("Generate")]
	public void generate()
    {
        //Destroy All children
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector2 pos = new Vector2((-i* tileWidth) + (j* tileWidth),0 + (i * tileHeight) + (j * tileHeight));
                float noise = Mathf.PerlinNoise(seed + pos.x, seed + pos.y);

                GameObject tile;

                if (noise > waterLevel)
                {
                    tile = Instantiate(grassTile, pos, Quaternion.identity, this.transform);
                }
                else
                {
                    tile = Instantiate(waterTile, pos, Quaternion.identity, this.transform);
                }


                //Debug.Log("Noise: " + noise);
                tile.GetComponent<SpriteRenderer>().size += new Vector2(0, getNoiseHeight(pos));
            }
        }
    }
}
