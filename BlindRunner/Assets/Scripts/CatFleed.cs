using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatFleed : MonoBehaviour {

    [SerializeField]
	public int lane;
    private GameObject spawner;
    private SpawnManager _spawner;

    void Start(){
        spawner = GameObject.Find("Spawner");
        _spawner = spawner.GetComponent<SpawnManager>();
        lane = 1;
	}


	void Update () {

		if (!GameManager.instance.isDead) {
			PosHandler ();
			///if the lane is occ		upied we call the function
			if (_spawner.isOccupied [lane]) {
				LaneHandler ();
			}
		} else {

			transform.Translate (Vector3.up * Time.deltaTime);
		}
			

		
	}

	void PosHandler(){
	
        ///The lane number dictates where the cat should go
		if (lane == 0) {
			transform.position = Vector3.MoveTowards (transform.position, new Vector3 (_spawner.lanes [lane], transform.position.y, transform.position.z), 4 * Time.deltaTime);
		} else if (lane == 1) {
			transform.position =  Vector3.MoveTowards (transform.position, new Vector3 (_spawner.lanes [lane], transform.position.y, transform.position.z), 4 * Time.deltaTime);
		} else if (lane == 2) {
			transform.position =  Vector3.MoveTowards (transform.position, new Vector3 (_spawner.lanes [lane], transform.position.y, transform.position.z), 4 * Time.deltaTime);
		}
        
	}


    ///Depending on which lane is not occupied, the cat goes for it
    void LaneHandler(){
		for(int i=0; i<3;i++){
			if(_spawner.isOccupied[i]==false){
				lane = i;
			}
		}
	}
}

/*
    if (lane == 0) {
        transform.position = new Vector3 (_obsManager.lanes [lane], transform.position.y, transform.position.z);
    } else if (lane == 1) {
        transform.position = new Vector3 (_obsManager.lanes [lane], transform.position.y, transform.position.z);
    } else if (lane == 2) {
        transform.position = new Vector3 (_obsManager.lanes [lane], transform.position.y, transform.position.z);
    }
    */
