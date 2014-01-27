﻿using UnityEngine;
using System.Collections;

public class TooltipTexts:MonoBehaviour {
	//Text Being used in the Tooltips.

	private static TooltipTexts instance = null;

	public string build_tower_default = "You can build\ntowers from this menu";

	public string build_tower_normal = "Build a normal\ntower";

	public string build_tower_ice = "Build an ice tower";

	public string build_tower_fire = "Build a fire tower";

	public string build_mine = "Build a mine";

	public string build_lumbermill = "Build a lumber mill";

	public string build_bridge = "Build a bridge";

	public string build_not_available = "Insufficient resources";

	public string build_not_available_island = "This building\ncan't be placed here";

	public static TooltipTexts Instance {
		get {
			if(instance == null) {
				instance = FindObjectOfType(typeof(TooltipTexts)) as TooltipTexts;
			}
			
			if(instance == null) {
				GameObject go = new GameObject("TooltipTexts");
				instance = go.AddComponent(typeof(TooltipTexts)) as TooltipTexts;
			}
			
			return instance;
		}
	}
	
	void OnApplicationQuit() {
		instance = null;
	}
}
