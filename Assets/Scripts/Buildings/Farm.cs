using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A farm increases food income
/// </summary>
public class Farm : Building {

	public int foodIncome = 10;

	public override void built(){

		ResourceManager.instance.changeResourceBaseIncome(Resource.FOOD, foodIncome);

	}

	public override void destroyBuilding(){

		ResourceManager.instance.changeResourceBaseIncome(Resource.FOOD, -foodIncome);

	}
}
