using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A house increases population increasing productivity and possible army size
/// </summary>
public class House : Building {

	[Tooltip("Amount to increase population by when built")]
	public int capacity = 5;

	public int goldIncome = 5;

	public override void built(){

		ResourceManager.instance.changeResourceAmount(Resource.POPULATION, capacity);

		ResourceManager.instance.changeResourceBaseIncome(Resource.GOLD, goldIncome);

	}

	public override void destroyBuilding(){

		ResourceManager.instance.changeResourceAmount(Resource.POPULATION, capacity);

		ResourceManager.instance.changeResourceBaseIncome(Resource.GOLD, -goldIncome);

	}
}
