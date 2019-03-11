using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

///Collectibles -->Movement and collisions
public class CollectibleHandler : MonoBehaviour
{

    public float yPos, minObjectSpeed, maxObjectSpeed;
    private float objSpeed;
    private GameObject spawner;
    private SpawnManager _spawner;
    private GameObject gameManager;
    private GameManager _gameManager;
    private GameObject player;
    private GameObject blind;

    public bool isDoggoCollectable;
    public bool isGuideSwapper;
    public int bonusValue;

    public int lane;
    void Start()
    {
        player = GameObject.Find("Doggo");
        blind = GameObject.Find("BLIND");
        spawner = GameObject.Find("Spawner");
        _spawner = spawner.GetComponent<SpawnManager>();
        gameManager = GameObject.Find("Manager");
        _gameManager = gameManager.GetComponent<GameManager>();

        PositionAndSpeed();

    }

    void Update()
    {
        float movement = objSpeed * Time.deltaTime;
        transform.Translate(Vector3.down * movement, Space.World);

    }
    void PositionAndSpeed()
    {
        objSpeed = Random.Range(minObjectSpeed, maxObjectSpeed);
    }

	///In the collision we check whether it is the glasses or a normal collectable using tags and booleans. When we collect it the we pass to the gameManager a bonus score, or  we remove it if a character grabs the incorrect collectable
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isGuideSwapper)
        {
            if (other.gameObject.tag == "Doggo" && isDoggoCollectable)
            {
                Debug.Log("Collected");
                // _obsManager.isOccupied[lane] = false;
                // _obsManager.instantiatorController--;

                _gameManager.ExtraBonus(bonusValue);
				_gameManager.StartCoroutine(_gameManager.TextCoroutine(bonusValue));
                Destroy(gameObject);
            }
            else if (other.gameObject.tag == "Doggo" && !isDoggoCollectable)
            {

                _gameManager.ExtraBonus(-bonusValue);
				_gameManager.StartCoroutine(_gameManager.TextCoroutine(-bonusValue));
                Destroy(gameObject);
            }

            if (other.gameObject.tag == "Blind" && !isDoggoCollectable)
            {
              
                _gameManager.ExtraBonus(bonusValue);
				_gameManager.StartCoroutine(_gameManager.TextCoroutine(bonusValue));
                Destroy(gameObject);
            }
            else if (other.gameObject.tag == "Blind" && isDoggoCollectable)
            {
                _gameManager.ExtraBonus(-bonusValue);
				_gameManager.StartCoroutine(_gameManager.TextCoroutine(-bonusValue));
                Destroy(gameObject);
            }

            if (other.gameObject.tag == "Finish")
            {
                // _obsManager.isOccupied[lane] = false;

                //  _obsManager.instantiatorController--;

                /*
                if (_obsManager.instantiatorController <= 0)
                {
                    _obsManager.instantiatorController = 0;
                }
                */
                Destroy(this.gameObject);
            }
        }
        else if (isGuideSwapper)
        {
            if (other.gameObject.tag == "Doggo" || other.gameObject.tag == "Blind")
            {
                _gameManager.ExtraBonus(bonusValue);
				_gameManager.StartCoroutine(_gameManager.TextCoroutine(bonusValue));
                player.GetComponent<CharMov>().isDoggoInControl = !player.GetComponent<CharMov>().isDoggoInControl;
                Destroy(gameObject);
            }

            if (other.gameObject.tag == "Finish")
            {
                Destroy(gameObject);
            }

        }

    }



}
