using UnityEngine;
using System.Collections;

public class ResourceManager : MonoBehaviour
{

    public enum Resource {FOOD, WOOD, GOLD};

    //Singleton
    public static ResourceManager instance;

    //Components
    private ResourceUI resourceUI;

	//Resource Arrays
    public int[] resourceAmount;
    private int[] resourceBaseIncome;
    private int[] resourceUpkeep;

    //Tick variables
    private float tick = 1f;

    void Start(){

        //Only one resource manager can exist
        if(instance != null){
            Destroy(instance);
        }

        instance = this;

        resourceUI = GetComponent<ResourceUI>();

    	resourceAmount = new int[3];
        resourceBaseIncome = new int[3];
        resourceUpkeep = new int[3];

        //Setup the process income function to run every tick
        InvokeRepeating("processIncome", tick, tick);
    }

    /// <summary>
    /// Calculates and adds the income of all resources to the amount available
    /// </summary>
    private void processIncome(){

        resourceAmount[0] += resourceBaseIncome[0] - resourceUpkeep[0]; //FOOD income
        resourceAmount[1] += resourceBaseIncome[1] - resourceUpkeep[1]; //WOOD income
        resourceAmount[2] += resourceBaseIncome[2] - resourceUpkeep[2]; //GOLD income

        resourceUI.updateText(resourceAmount, resourceBaseIncome, resourceUpkeep);
    }


    #region Getters
    /// <summary>
	/// Takes a resource enum and returns the amount available of the resource.
	/// </summary>
    public int getResourceAmount(Resource resource){
        return resourceAmount[(int) resource];
    }

    /// <summary>
    /// Takes a resource enum and returns the base income of the resource.
    /// </summary>
    public int getResourceBaseIncome(Resource resource){
        return resourceBaseIncome[(int)resource];
    }

    /// <summary>
    /// Takes a resource enum and returns the income of the resource after taking into account the upkeep.
    /// </summary>
    public int getresourceIncome(Resource resource){
        return resourceBaseIncome[(int)resource] - resourceUpkeep[(int)resource];
    }

    /// <summary>
    /// Takes a resource enum and returns the upkeep of the resource.
    /// </summary>
    public int getResourceUpkeep(Resource resource){
        return resourceUpkeep[(int)resource];
    }
    #endregion

}