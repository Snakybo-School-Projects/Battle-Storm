﻿using UnityEngine;
using System.Collections;

public class GameManager:MonoBehaviour {
	public static float nativeWidth = 1280.0f;
	public static float nativeHeight = 720.0f;
	
	private MusicFlow musicFlow;

	void Start() {
		musicFlow = GetComponent<MusicFlow>();
	}
	
	void FixedUpdate() {
		if(WaveManager.spawningEnemies) {
			musicFlow.Wave();
		} else {
			musicFlow.Wait();
		}
	}
}