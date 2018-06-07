using UnityEngine;
using System.Collections;

public abstract class Building : MonoBehaviour
{

	//Components
    protected SpriteRenderer spriteRenderer;

    //Cost Variables
    public int woodCost = 0;
    public int goldCost = 0;

    void Start(){

    	spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Actions to be taken once the building has been built
    /// </summary>
    public abstract void built();


    #region Getters

    public Sprite getSprite(){
        return spriteRenderer.sprite;
    }

    #endregion

}