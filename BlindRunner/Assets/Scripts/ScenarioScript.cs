using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioScript : MonoBehaviour {


	[SerializeField]
	private float ScenarioSpeed;


	void Start () {


	}
	

	void Update () {
		if (GameManager.instance.isDead == false) {
			transform.Translate (Vector3.down * ScenarioSpeed * Time.deltaTime);
		}

		if (transform.position.y < -9f) {
			transform.position = new Vector3 (transform.position.x, 10.88f, 0);
		}
		
	}
}
