using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResourceUI : MonoBehaviour
{

	//Variables
    [Tooltip("FOOD, WOOD, GOLD, and POPULATION amounts")]
    public Text[] resoruceAmountTextboxes;

    [Tooltip("FOOD, WOOD, and GOLD incomes")]
    public Text[] resoruceIncomeTextboxes;

    void Start(){

    	//Do something on Start
    }

    /// <summary>
	/// Update the UI textboxes containing the resource amounts and incomes
	/// </summary>
    public void updateText(int[] amounts, int[] incomes, int[] upkeeps){

    	for(int i = 0; i < amounts.Length; i++){

            resoruceAmountTextboxes[i].text = "" + amounts[i];

            //Only update income for first 3 resources
            if(i < 3){

                int income = incomes[i] - upkeeps[i];
                resoruceIncomeTextboxes[i].text = "" + income;

                //Update Text color is losing resource
                if(income < 0)
                {
                    resoruceIncomeTextboxes[i].color = Color.red;
                }
                else
                {
                    resoruceIncomeTextboxes[i].color = Color.green;
                }
            }
        }
    }

}