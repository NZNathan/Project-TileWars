using UnityEngine;
using System.Collections;

public abstract class Building : MonoBehaviour
{

	//Components
    private SpriteRenderer spriteRenderer;

    void Start(){

    	spriteRenderer = GetComponent<SpriteRenderer>();
    }


    #region Getters

    public Sprite getSprite(){
        return spriteRenderer.sprite;
    }

    #endregion

}