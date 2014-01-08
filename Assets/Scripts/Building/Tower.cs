﻿using UnityEngine;
using System.Collections;

public class Tower:Building {
	public enum TowerType {
		Normal = 1,
		Ice = 2
	};

	private enum GoldCost {
		Level1 = 50,
		Level2 = 100,
		Level3 = 250,
		Level4 = 500,
		Level5 = 1000
	};

	public enum StoneCost {
		Level1 = 0,
		Level2 = 250,
		Level3 = 600,
		Level4 = 1200,
		Level5 = 3000
	};
	
	private enum WoodCost {
		Level1 = 0,
		Level2 = 150,
		Level3 = 300,
		Level4 = 500,
		Level5 = 3000
	};

	private enum GoldSell {
		Level1 = 50,
		Level2 = 100,
		Level3 = 250,
		Level4 = 500,
		Level5 = 1000
	};

	private enum StoneSell {
		Level1 = 0,
		Level2 = 10,
		Level3 = 10,
		Level4 = 210,
		Level5 = 500
	};
	
	private enum WoodSell {
		Level1 = 0,
		Level2 = 10,
		Level3 = 10,
		Level4 = 210,
		Level5 = 500
	};

	public float damage;
	public float maxRange;
	public TowerType towerType; 

	private GameObject target;
	private Transform top;

	public AudioClip shotSound;
	private Vector3 arrowPosition;

	void Start() {
		currentLevel = Upgrade.Level1;

		stoneCost = (int)StoneCost.Level2;
		stoneSell = (int)StoneSell.Level1;

		goldSell = (int)GoldSell.Level1;
		woodCost = (int)WoodCost.Level2;
		woodSell = (int)WoodSell.Level1;

		UpdateArt();
		top = transform.FindChild("Art").transform.FindChild("Pivot");
		arrowPosition = transform.FindChild("Art").transform.FindChild("Pivot").transform.FindChild("ArrowPosition").transform.position;
		StartCoroutine("Tick");
	}

	void Update() {
		if(target != null)
			Debug.DrawLine(transform.position, target.transform.position);
	}

	void FixedUpdate(){
		if(target == null || target.GetComponent<Enemy>().isDead)
			SearchForNewTarget();

		if(target != null)
			top.transform.LookAt(target.transform.position);
	}

	protected override IEnumerator Tick() {
		while(true) {
			yield return new WaitForSeconds(tickDelay);

			if(target != null) {
				if(Vector3.Distance(transform.position, target.transform.position) > maxRange)
					SearchForNewTarget();
				
				Fire();
			}
		}
	}

	void SearchForNewTarget() {
		GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");
		GameObject closest = null;
		float distance = 0;
		
		foreach(GameObject go in targets) {
			if(Vector3.Distance(go.transform.position, transform.position) <= maxRange) {
				if(closest == null) {
					closest = go;
					distance = Vector3.Distance(go.transform.position, transform.position);
				}
				
				if(Vector3.Distance(go.transform.position, transform.position) < distance) {
					closest = go;
					distance = Vector3.Distance(go.transform.position, transform.position);
				}
			}
		}
		
		target = closest;
	}

	void Fire() {
		if(target != null || !target.GetComponent<Enemy>().isDead) {
			GameObject projectile = Instantiate(Resources.Load("Prefabs/Projectile/Projectile"), arrowPosition, Quaternion.identity) as GameObject;
			Projectile proj = projectile.GetComponent<Projectile>();

			proj.target = target.transform;
			proj.damage = damage;
			proj.targetScript = target.gameObject.GetComponent<Enemy>();

			audio.PlayOneShot(shotSound);

			if(towerType == TowerType.Ice)
				target.GetComponent<Enemy>().Slowdown();
		}
	}
	
	public override void SwitchLevel(Upgrade newLevel) {
		if(newLevel > maxLevel)
			return;
		
		currentLevel = newLevel;
		
		stoneCost++;
		stoneSell++;
		
		woodCost++;
		woodSell++;
		
		UpdateArt();
	}
}
