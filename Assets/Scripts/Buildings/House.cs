using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A house increases population increasing productivity and possible army size
/// </summary>
public class House : Building {

	[Tooltip("Amount to increase population by when built")]
	public int capacity = 5;

	public override void built(){

		ResourceManager.instance.changeResourceAmount(ResourceManager.Resource.POPULATION, capacity);

	}
}
