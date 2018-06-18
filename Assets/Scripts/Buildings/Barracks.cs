using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A Barracks can produce military units for combat
/// </summary>
public class Barracks : Building {

	[Tooltip("Amount to increase population by when built")]
	public Unit[] units;

	public override void built(){

	}

	public override void destroyBuilding(){	

	}
}
