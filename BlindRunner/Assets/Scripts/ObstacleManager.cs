using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Handles the obstacles, chooses the obstacle or collectible, spawn rates,  lanes
public class ObstacleManager : MonoBehaviour
{

    [SerializeField]
    public GameObject[] spawnedObstacle;
    [SerializeField] private float minSpawnTime, maxSpawnTime;
    [HideInInspector]
    public int ableToInstantiate, instantiatorController;
    private int whichObj;
    private int whichObj_;

    //they are public because the cat needs it
    [HideInInspector]
    public float[] lanes = new float[3];
    public bool[] isOccupied = new bool[3];
    private int whichLane;
    private int whichLane_;

    float distanceControl;

    //int previousLane = 0;
    void Start()
    {

        lanes[0] = -2f;
        lanes[1] = 0f;
        lanes[2] = 2f;

        StartCoroutine("Instantiator");
    }

    //Creates obstacle, calls coroutine to decide the lane
    void CreateObstacle()
    {
        //If we are able to instantiate and the game hasn't ended yet
        if (ableToInstantiate == 0 && GameManager.instance.isDead == false && instantiatorController < 2)
        {
            //We are instantiating, so we are not able to instantiate
            ableToInstantiate++;

            GameObject obstacle = Instantiate(spawnedObstacle[whichObj], new Vector3(lanes[whichLane], 7, 0), Quaternion.identity);
            obstacle.GetComponent<ObstaclesMovement>().lane = whichLane;
            instantiatorController++;

        }
        StartCoroutine("Instantiator");

    }
    //decides lane, obstacle, spawn rate. If it has choosen an occupied lane, it calls itself again until it chooses a free lane
    IEnumerator Instantiator()
    {

        //how long should we wait for the next spawn
        float timeToSpawn = Random.Range(minSpawnTime, maxSpawnTime);
        whichObj = Random.Range(0, 2);
        whichLane = Random.Range(0, 3);

        if (!isOccupied[whichLane])
        {
            print("obstacle: " + Time.time);
            isOccupied[whichLane] = true;
            yield return new WaitForSeconds(timeToSpawn);
            //isOccupied[whichLane] = true;           
            ableToInstantiate = 0;

            CreateObstacle();

        }
        else
        {
            yield return null;
            StartCoroutine("Instantiator");
        }

    }


    public void CreateCollectable()
    {
        whichObj_ = Random.Range(2, 4);


        for (int i = 0; i < 3; i++)
        {
            if (!isOccupied[i])
            {
                isOccupied[i] = true;
                GameObject obstacle = Instantiate(spawnedObstacle[whichObj_], new Vector3(lanes[i], 7, 0), Quaternion.identity);
                obstacle.GetComponent<CollectibleHandler>().lane = i;
                Debug.Log("collectable: " + Time.time);
                break;
            }
            else
            {
                Debug.Log("oh NOOOOO i cant instancifjhgte");
            }

        }

        //StartCoroutine ("InstantiateCollectible");
    }
}
    /*
    // Creates collectible
    IEnumerator InstantiateCollectible()
    {
        yield return null;
        whichObj_ = Random.Range(2, 4);
        whichLane_ = Random.Range(0, 3);

        if (!isOccupied[whichLane_])
        {
            GameObject obstacle = Instantiate(spawnedObstacle[whichObj_], new Vector3(lanes[whichLane_], 7, 0), Quaternion.identity);
            isOccupied[whichLane_] = true;
            obstacle.GetComponent<CollectibleHandler>().lane = whichLane_;
            Debug.Log("instatia");
        }
        else
        {
            Debug.Log("I had to restart the coroutine");
            StartCoroutine("InstantiateCollectible");
        }
        //Debug.Log("INSTANTIATING COLLECTABLE "+spawnedObstacle[whichObj_]);
    }
/*
/*
   while(whichLane == previousLane)
   {
       whichLane = Random.Range(0, 3);
   }
   previousLane = whichLane;
  */

/*
    if (ableToInstantiate == 0 && ManagerController.instance.isDead == false)
    {
        ableToInstantiate++;
        Instantiate(spawnedObstacle[whichObj]);

        StartCoroutine("Instantiator");

    }
    */

/* private void Update()
{
   if(ManagerController.instance.distance >= 10f && distanceControl != ManagerController.instance.distance)
   {
       distanceControl = ManagerController.instance.distance;
       StartCoroutine("InstantiateCollectible");
   }
   else if(ManagerController.instance.distance >= distanceControl + 10f)
   {
       distanceControl = ManagerController.instance.distance;
       StartCoroutine("InstantiateCollectible");
   }
}*/


/* if (!isOccupied[whichLane_])
 {
     GameObject obstacle = Instantiate(spawnedObstacle[2], new Vector3(lanes[whichLane_], 7, 0), Quaternion.identity);
     obstacle.GetComponent<CollectibleHandler>().lane = whichLane;
     isOccupied[whichLane_] = true;
     Debug.Log("INSTANTIATING CAT");
 }
 else
 {
     CreateCollectable();
 }*/

/*
     if (isOccupied[whichLane] == true)
     {
         whichLane = Random.Range(0, 3);
     }
     */

//whichObj_ = Random.Range(2, 4);
//whichLane_ = Random.Range(0, 3);

////////////////////////////////////
/*
if (!isOccupied[whichLane_])
{
	GameObject obstacle = Instantiate(spawnedObstacle[whichObj_], new Vector3(lanes[whichLane_], 7, 0), Quaternion.identity);
	obstacle.GetComponent<CollectibleHandler>().lane = whichLane_;
	isOccupied[whichLane_] = true;
	Debug.Log("INSTANTIATING COLLECTABLE "+spawnedObstacle[whichObj_]);
}
else
{
	for (int i = 0; i <lanes.Length; i++) {
		Debug.Log ("i have choosen another lane");
		if (isOccupied [i] = false) {
			whichLane_ = i;
		}
	}
	StartCoroutine("InstantiateCollectible");
}
*/


///old instantiator
/// 
/*
if it somehow has choosen a free lane it goes straight to the coroutine
if (!isOccupied [whichLane_]) {
   //isOccupied[whichLane_] = true;

   StartCoroutine ("InstantiateCollectible");

} else {
   //else instead of trying leaving it to a random chance, it simply iterates through the lanes and chooses the first free one
   for (int i = 0; i <lanes.Length; i++) {
       Debug.Log ("i have choosen another lane");
       if (isOccupied [i] = false) {
           //isOccupied[whichLane_] = true;
           whichLane_ = i;
       }
   }

   StartCoroutine("InstantiateCollectible");
}
*/
