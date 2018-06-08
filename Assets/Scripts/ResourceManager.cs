using UnityEngine;
using System.Collections;

public enum Resource { FOOD, WOOD, GOLD, POPULATION };

public class ResourceManager : MonoBehaviour
{

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

    	resourceAmount = new int[4];
        resourceBaseIncome = new int[3];
        resourceUpkeep = new int[3];

        //Setup the process income function to run every tick
        InvokeRepeating("processIncome", tick, tick); //REPACE WITH COROUTINE
    }

    /// <summary>
    /// Calculates and adds the income of all resources to the amount available
    /// </summary>
    private void processIncome(){

        resourceAmount[0] += resourceBaseIncome[0] - resourceUpkeep[0]; //FOOD income
        resourceAmount[1] += resourceBaseIncome[1] - resourceUpkeep[1]; //WOOD income
        resourceAmount[2] += resourceBaseIncome[2] - resourceUpkeep[2]; //GOLD income

        //Update the UI
        resourceUI.updateText(resourceAmount, resourceBaseIncome, resourceUpkeep);
    }

    /// <summary>
    /// Returns true if there is enough resources to build th ebuilding passed in
    /// </summary>
    public bool canAfford(Building b){

        int woodCost = b.woodCost;
        int goldCost = b.goldCost;

        return resourceAmount[1] - woodCost >= 0 && resourceAmount[2] - goldCost >= 0;
    }

    #region Setters
    /// <summary>
    /// Takes a resource enum and adds/minus' the change to its total amount (can NOT go into negatives)
    /// </summary>
    public bool changeResourceAmount(Resource resource, int change){

        //Total amount can not be less than 0
        if(resourceAmount[(int)resource] + change < 0){
            return false;
        }

        resourceAmount[(int)resource] += change;
        return true;
    }

    /// <summary>
    /// Takes a resource enum and adds/minus' the change to its base income (can go into negatives)
    /// </summary>
    public void changeResourceBaseIncome(Resource resource, int change){
        resourceBaseIncome[(int)resource] += change;
    }

    /// <summary>
    /// Takes a resource enum and adds/minus' the change to its upkeep (can NOT go into negatives)
    /// </summary>
    public bool changeResourceUpkeep(Resource resource, int change){

        //Upkeep can not be less than 0
        if(resourceUpkeep[(int)resource] + change < 0){
            return false;
        }

        resourceUpkeep[(int)resource] += change;
        return true;
    }
    #endregion


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