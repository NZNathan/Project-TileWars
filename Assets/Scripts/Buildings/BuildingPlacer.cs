using UnityEngine;
using System.Collections;

public class BuildingPlacer : MonoBehaviour
{

    //Components
    private SpriteRenderer spriteRenderer;

    //Variables
    private Building placement;
    public Building[] buildings;

    void Start()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();

        this.enabled = false;
    }

    /// <summary>
	/// Checks for user input and responds accordingly
	/// </summary>
    void input()
    {

        if (Input.GetMouseButtonDown(0))
        {
            placeBuilding();
        }
    }

    /// <summary>
    /// Update the placement and sprite to be placed.
    /// </summary>
    public void setPlacement(int i)
    {
        placement = buildings[i];
        spriteRenderer.sprite = buildings[i].getSprite();

        this.enabled = true;
    }

    public void cancelPlacement()
    {
        placement = null;
        spriteRenderer.sprite = null;
        this.enabled = false;
    }


    /// <summary>
    /// Places the currently selected building for placement. Returns true if successful or false if not.
    /// </summary>
    private bool placeBuilding()
    {

        //If the tile is valid for placement and enough resources to build, then place the building
        if (validPlacement() && ResourceManager.instance.canAfford(placement) && WorldManager.instance.onMap(mouseToTile()))
        {
            Building b = Instantiate(placement, transform.position, Quaternion.identity);

            //Subtract costs
            Resource r = Resource.WOOD;
            ResourceManager.instance.changeResourceAmount(r, -b.woodCost);
            r = Resource.GOLD;
            ResourceManager.instance.changeResourceAmount(r, -b.goldCost);

            //To preserve correct layering must change size instead of increasing y pos, as this changes the layering calue
            Vector2 pos = mouseToTile();

            float noise = WorldGen.instance.getNoiseHeight(pos);

            //Update building
            b.transform.position = new Vector2(pos.x, pos.y);
            b.built();
            b.GetComponent<SpriteRenderer>().size += new Vector2(0, noise);

            //Update Tile
            WorldManager.instance.getTile(transform.position.x, transform.position.y).buildOnTile(b);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Assesses if the current tile can have a building placed onto it
    /// </summary>
    private bool validPlacement()
    {

        return WorldManager.instance.getTile(transform.position.x, transform.position.y).isBuildable();
    }

    /// <summary>
    /// Rounds the mouse Pos to the closest tile
    /// </summary>
    /// <returns></returns>
    public static Vector2 mouseToTile()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        pos.y += WorldGen.tileHalfHeight * 3;

        //Convert real world point to an index
        float roundXPos = ((pos.x / WorldGen.tileHalfWidth + pos.y / WorldGen.tileHalfHeight) / 2);
        float roundYPos = ((pos.y / WorldGen.tileHalfHeight - (pos.x / WorldGen.tileHalfWidth)) / 2);

        //Round to closest tile
        Vector2 newPos = new Vector2(Mathf.Round(roundXPos), Mathf.Round(roundYPos));

        //Convert back to real world point
        float x = newPos.x * WorldGen.tileHalfWidth - newPos.y * WorldGen.tileHalfWidth;
        float y = newPos.x * WorldGen.tileHalfHeight + newPos.y * WorldGen.tileHalfHeight;

        return new Vector2(x, y);
    }

    /// <summary>
    /// Takes the mouse pos and rounds to nearest tile, taking into account noise height
    /// </summary>
    private Vector2 roundToTile()
    {

        Vector2 pos = mouseToTile();

        float noise = WorldGen.instance.getNoiseHeight(pos);

        pos.y += noise;

        return pos;
    }


    void Update()
    {
        //Update position of the placement to snap to a tile
        if (WorldManager.instance.onMap(mouseToTile()))
        {
            transform.position = roundToTile();
        }

        //Call input every frame
        input();
    }
}