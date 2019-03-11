using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///Creates obstacles and collectables

public class SpawnManager : MonoBehaviour
{


    public GameObject[] spawnedObstacle;
    public bool collectableTime;

	///For presentation only
	public bool hasSpawned;


    public float minSpawnTime, maxSpawnTime;
    private float spawnTime;


    private float counter;
    [HideInInspector]
    public float[] lanes = new float[3];
    public bool[] isOccupied = new bool[3];

    private int laneControl = -1;
    private int countLane;


    private int obstacleControl=-1;
    private int countObstacle;

    void Start()
    {
        lanes[0] = -2f;
        lanes[1] = 0f;
        lanes[2] = 2f;
        spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        counter = spawnTime;
    }


    void Update()
    {
		if (!GameManager.instance.isDead) {
			SpawnFunc();
		}
		SpawnGlasses ();
    }

	//FOR PRESENTATION ONLY
	void SpawnGlasses(){
		if (!hasSpawned) {
			if (Input.GetKey (KeyCode.Alpha1)) {
				GameObject obstacle = Instantiate (spawnedObstacle [4], new Vector3 (lanes [0], 7, 0), Quaternion.identity);
				hasSpawned = true;
			} else if (Input.GetKey (KeyCode.Alpha2)) {
				GameObject obstacle = Instantiate (spawnedObstacle [4], new Vector3 (lanes [1], 7, 0), Quaternion.identity);
				hasSpawned = true;
			} else if (Input.GetKey (KeyCode.Alpha3)) {
				GameObject obstacle = Instantiate (spawnedObstacle [4], new Vector3 (lanes [2], 7, 0), Quaternion.identity);
				hasSpawned = true;

			}
		}


	}


    void SpawnFunc()
	{
		///The counter is equal to the spawn time when it reaches zero. When it reaches zero we spawn
        spawnTime = Random.Range(minSpawnTime, maxSpawnTime);

        if (counter <= 0 && collectableTime == false)
        {
   	        int whichObj = Random.Range(0, 2);
            int whichLane = Random.Range(0, 3);
            
			/// If the randomizer chooses the same obstacle more than twice, we choose another obstacle
            if (whichObj == obstacleControl)
            {
                if (countObstacle < 1)
                {
                    countObstacle++;
                }
                else
                {
                    Debug.Log("choosing another obj");
                    if (whichObj == 0)
                    {
                        whichObj = 1;
                    }
                    else
                    {
                        whichObj = 0;
                    }
                    countObstacle = 0;
                }
            }
            obstacleControl = whichObj;

            /// If the randomizer chooses the same lane more than twice, we choose another lane
            if (whichLane == laneControl)
            {
                if (countLane < 1)
                {
                    countLane++;
                }
                else
                {
                    Debug.Log("Choosing other lane");
                    if (whichLane == 2)
                    {
                        whichLane = 0;
                    }
                    else
                    {
                        whichLane++;
                    }
                    countLane = 0;
                }
            }
            laneControl = whichLane;
            isOccupied[whichLane] = true;
            GameObject obstacle = Instantiate(spawnedObstacle[whichObj], new Vector3(lanes[whichLane], 8, 0), Quaternion.identity);
            obstacle.GetComponent<ObstaclesMovement>().lane = whichLane;
            counter = spawnTime;
        }
        else if (counter <= 0 && collectableTime == true)
        {


            int spawnGlasses = Random.Range(0, 5);
            int whichObj;

            ///For the colectables we want to know if it's gonna pick a normal collectable or the glasses
            if (spawnGlasses == 0)
            {
                Debug.Log("Spawning Glasses");
                whichObj = 4;
            }
            else
            {
                whichObj = Random.Range(2, 4);
            }

            

            int whichLane = Random.Range(0, 3);


            if (whichLane == laneControl)
            {
                if (countLane < 1)
                {
                    countLane++;
                }
                else
                {
                    if (whichLane == 2)
                    {
                        whichLane = 0;
                    }
                    else
                    {
                        whichLane++;
                    }
                    countLane = 0;
                }
            }
            laneControl = whichLane;


            GameObject obstacle = Instantiate(spawnedObstacle[whichObj], new Vector3(lanes[whichLane], 7, 0), Quaternion.identity);
            //obstacle.GetComponent<CollectibleHandler>().lane = whichLane;
            //isOccupied[whichLane] = true;
            counter = spawnTime;
            // Debug.Log("colectable");
            collectableTime = false;
            // Debug.Log("Okay spawn");
        }
        else
        {
            counter -= Time.deltaTime;
        }
    }

	///If the GameManager hits a threshold we reduce the time needed to spawn obstacles via parameters
    public void ReduceSpawnTime(float value)
    {
        minSpawnTime -= value;
        maxSpawnTime -= value;
    }
}
