using UnityEngine;
using System.Collections;

public abstract class Building : MonoBehaviour
{

	//Components
    protected SpriteRenderer spriteRenderer;

    //Cost Variables
    public int woodCost = 0;
    public int goldCost = 0;

    //Stats
    public int maxHealth;
    public int currentHealth;

    void Start(){

        //Get Components
    	spriteRenderer = GetComponent<SpriteRenderer>();

        //Set up Stats
        currentHealth = maxHealth;
    }

    /// <summary>
    /// Actions to be taken once the building has been built
    /// </summary>
    public abstract void built();

    /// <summary>
    /// Actions to be taken before destroying the building
    /// </summary>
    public abstract void destroyBuilding();


    #region Getters

    public Sprite getSprite(){

        if(spriteRenderer == null)
        {
            Start();
        }
        return spriteRenderer.sprite;
    }

    #endregion

}