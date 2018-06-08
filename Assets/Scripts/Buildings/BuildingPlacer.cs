using UnityEngine;
using System.Collections;

public class BuildingPlacer : MonoBehaviour
{

    //Components
    private SpriteRenderer spriteRenderer;

	//Variables
    public static bool placing = true;
    public Building placement;

    void Start(){

    	spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
	/// Checks for user input and responds accordingly
	/// </summary>
    void input(){

    	if(Input.GetMouseButtonDown(0)){
            placeBuilding();
        }
    }

    /// <summary>
    /// Update the placement and sprite to be placed.
    /// </summary>
    public void setPlacement(Building building){
        placement = building;
        spriteRenderer.sprite = building.getSprite();
    }


    /// <summary>
    /// Places the currently selected building for placement. Returns true if successful or false if not.
    /// </summary>
    private bool placeBuilding(){

        //If the tile is valid for placement and enough resources to build, then place the building
        if(validPlacement() && ResourceManager.instance.canAfford(placement))
        {
            Building b = Instantiate(placement, transform.position, Quaternion.identity);

            //Subtract costs
            Resource r = Resource.WOOD;
            ResourceManager.instance.changeResourceAmount(r, b.woodCost);
            r = Resource.GOLD;
            ResourceManager.instance.changeResourceAmount(r, b.goldCost);

            //To preserve correct layering must change size instead of increasing y pos, as this changes the layering calue
            Vector2 pos = mouseToTile();

            float noise = WorldGen.instance.getNoiseHeight(pos);

            b.built();
            b.transform.position = new Vector2(pos.x, pos.y);
            b.GetComponent<SpriteRenderer>().size += new Vector2(0, noise);
            return true;
        }

        return false;
    }


    /// <summary>
    /// Assesses if the current tile can have a building placed onto it
    /// </summary>
    private bool validPlacement(){

        return true;
    }

    /// <summary>
    /// Rounds the mouse Pos to the closest tile
    /// </summary>
    /// <returns></returns>
    public static Vector2 mouseToTile()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        pos.y += WorldGen.tileHalfHeight*3;

        float roundXPos = Mathf.Round(pos.x / (WorldGen.tileHalfWidth * 1)) * (WorldGen.tileHalfWidth * 1); //Times 2 to account for isometric view (otherwise snaps inbetween tiles)
        float roundYPos = Mathf.Round(pos.y / (WorldGen.tileHalfHeight * 1)) * (WorldGen.tileHalfHeight * 1);
        //Debug.Log(roundYPos % 0.16);
        if ((Mathf.Abs(roundYPos % 0.16f) > 0.078 && Mathf.Abs(roundYPos % 0.16f) < 0.081) && !(Mathf.Abs(roundXPos % 0.32f) > 0.158 && Mathf.Abs(roundXPos % 0.32f) < 0.161))
        {
            roundYPos += 0.08f;
        }
        else if (!(Mathf.Abs(roundYPos % 0.16f) > 0.078 && Mathf.Abs(roundYPos % 0.16f) < 0.081) && (Mathf.Abs(roundXPos % 0.32f) > 0.158 && Mathf.Abs(roundXPos % 0.32f) < 0.161))
        {
            roundYPos += 0.08f;
        }

        return new Vector2(roundXPos, roundYPos);
        //return new Vector2(pos.x, pos.y);
    }

    /// <summary>
    /// Takes the mouse pos and rounds to nearest tile, taking into account noise height
    /// </summary>
    private Vector2 roundToTile(){

        Vector2 pos = mouseToTile();

        float noise = WorldGen.instance.getNoiseHeight(pos);

        pos.y += noise;

        return pos;
    }


    void Update ()
    {
        if(placing)
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
}