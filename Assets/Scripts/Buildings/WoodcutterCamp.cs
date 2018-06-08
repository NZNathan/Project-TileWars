using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A Woodcutter Camp increases wood income
/// </summary>
public class WoodcutterCamp : Building {

	public int woodIncome = 10;

	public override void built(){

		ResourceManager.instance.changeResourceBaseIncome(Resource.WOOD, woodIncome);

	}

	public override void destroyBuilding(){

		ResourceManager.instance.changeResourceBaseIncome(Resource.WOOD, -woodIncome);

	}
}
