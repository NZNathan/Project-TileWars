using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A Woodcutter Camp increases wood income
/// </summary>
public class WoodcutterCamp : Building {

	public int woodIncome = 0;

    [Tooltip("How far away this camp will travel to get income from forest tiles")]
    public int forestRange = 1;

	public override void built(){

		ResourceManager.instance.changeResourceBaseIncome(Resource.WOOD, WorldManager.instance.GetSurroundingTiles(MapTile.Type.FOREST, forestRange, transform.position));

	}

	public override void destroyBuilding(){

		ResourceManager.instance.changeResourceBaseIncome(Resource.WOOD, -woodIncome);

	}
}
