using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///Obstacles --> Movement and collisions
public class ObstaclesMovement : MonoBehaviour
{

    public float yPos, minObjectSpeed, maxObjectSpeed;
    private float xPos, objSpeed;
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject spawner;
    private SpawnManager _spawner;

    public int lane;

    ///every time an obj spawns it gets an speed increase
    void Start()
    {
        player = GameObject.Find("Doggo");
        spawner = GameObject.Find("Spawner");
        _spawner = spawner.GetComponent<SpawnManager>();
        GameManager _managerController = FindObjectOfType<GameManager>();
        minObjectSpeed += _managerController.speedIncrease;
        maxObjectSpeed += _managerController.speedIncrease;

        PositionAndSpeed();

        //Debug.Log("hello i am " + gameObject.name + " my min speed is: " + minObjectSpeed);
    }


    void Update()
    {
        float movement = objSpeed * Time.deltaTime;
        transform.Translate(Vector3.down * movement, Space.World);
        if (transform.position.y <= 6f)
        {
            _spawner.isOccupied[lane] = false;
        }
    }
    void PositionAndSpeed()
    {
        objSpeed = Random.Range(minObjectSpeed, maxObjectSpeed);
    }

    ///Calls the player function, which in turn calls the managercontroller function
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Doggo" || other.gameObject.tag == "Blind")
        {
            player.GetComponent<CharMov>().LoseLife();
        }

        if (other.gameObject.tag == "Finish")
        {
           


            Destroy(this.gameObject);
        }
    }
}

//_obsManager.isOccupied[lane] = false;

//_obsManager.instantiatorController--;

/*
            if (_obsManager.instantiatorController <= 0)
            {
                _obsManager.instantiatorController = 0;
            }
*/