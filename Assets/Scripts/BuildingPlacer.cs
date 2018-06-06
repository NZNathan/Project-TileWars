using UnityEngine;
using System.Collections;

public class BuildingPlacer : MonoBehaviour
{

    //Components
    private SpriteRenderer spriteRenderer;

	//Variables
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

        if(validPlacement())
        {
            Building b = Instantiate(placement, transform.position, Quaternion.identity);

            //To preserve correct layering must change size instead of increasing y pos, as this changes the layering calue
            Vector2 pos = mouseToTile();

            float noise = WorldGen.instance.getNoiseHeight(pos);

            b.transform.position = new Vector2(pos.x, pos.y);
            b.GetComponent<SpriteRenderer>().size += new Vector2(0, noise);
            return true;
        }

        return false;
    }


    /// <summary>
    /// Assesses if the current tile can have a building placewd onto it
    /// </summary>
    private bool validPlacement(){

        return true;
    }

    /// <summary>
    /// Rounds the mouse Pos to the closest tile
    /// </summary>
    /// <returns></returns>
    private Vector2 mouseToTile()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float roundXPos = Mathf.Round(pos.x / (WorldGen.tileWidth * 2)) * (WorldGen.tileWidth * 2); //Times 2 to account for isometric view (otherwise snaps inbetween tiles)
        float roundYPos = Mathf.Round(pos.y / (WorldGen.tileHeight * 2)) * (WorldGen.tileHeight * 2);

        return new Vector2(roundXPos, roundYPos);
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
        //Update position of the placement to snap to a tile
        transform.position = roundToTile();

        //Call input every frame
        input();
    }
}