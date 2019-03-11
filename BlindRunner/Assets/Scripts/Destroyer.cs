using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ich muss ZERSTÖREEEEEEEEEEN  JA, JA, JA   Doch es darf nicht mir gehören
public class Destroyer : MonoBehaviour {

    private GameObject obsManager;
	
	void Start () {
        obsManager = GameObject.Find("ObstacleManager");
	}
	
	void Update () {
        		
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            Debug.Log("destroying obstacle");
            Destroy(other.gameObject);
        }
    }
}
