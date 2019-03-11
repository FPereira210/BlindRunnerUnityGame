using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//This is the Game Manager, handles--> distance,score, when we spawn collectibles, death and reset

public class GameManager: MonoBehaviour
{

	public Text distanceText;
	public Text bonusText;
	public Text scoreText;
	[SerializeField]
	private Button restartButton, mainButton;
	[HideInInspector]
	public float distance;
	[HideInInspector]
	public bool isDead,isPaused;
	private GameObject player;
	private GameObject spawner;
	private SpawnManager _spawner;

	public static GameManager instance;

	public float speedIncrease = 0.3f;
	public float timeDecrease;

	private float spawnControl = 10;
	private float reduceSpawnTimeControl=40;

	//the score is changed into a int in the end
	private float score;

	//the collectables give a bonus to the score
	public int collectableBonus;

	public float GetScore ()
	{
		return score;
	}

   

	void Start ()
	{
		Time.timeScale = 1;
		bonusText = GameObject.Find ("ExtraPoints").GetComponent<Text>();
		restartButton.gameObject.SetActive (false);
		mainButton.gameObject.SetActive (false);
		scoreText.enabled = false;
		spawner = GameObject.Find ("Spawner");
		_spawner = spawner.GetComponent<SpawnManager> ();
		instance = this;
       
	}


	void Update ()
	{
		CollectablesAndSpeed ();
		PauseMenu ();

	}

	void CollectablesAndSpeed(){
		//When the distance reaches the distance control we spawn a collectable, calling a function inside the spawn manager
		if (isDead == false) {
			distance += Time.deltaTime * 2;
			distanceText.text = "Distance: " + (int)distance;

			//If we hit the threshold we increase the threshold again, spawn a collectable
			if (distance >= spawnControl) {
				speedIncrease += 0.1f;
				float collectableRandomTime = Random.Range(15f, 25);
				spawnControl = distance + collectableRandomTime;
				_spawner.collectableTime = true;
			}

			//If we hit the threshold we increase the threshold again, and reduce the time needed to spawn obstacles using the spawner's ReduceSpawnTime()TM .Inc function
			if (distance >= reduceSpawnTimeControl) {
				reduceSpawnTimeControl = distance + 10f;
				_spawner.ReduceSpawnTime (timeDecrease);
				Debug.Log ("decrease");
			}
		}

	}

	///The button calls this function, which interacts with the pauseMenu() func
	public void PauseGame(){
		if (!isDead) {
			isPaused = !isPaused;
		}

	}

	///Pause Handler
	void PauseMenu(){
		if (isPaused&&!isDead) {
			restartButton.gameObject.SetActive (true);
			mainButton.gameObject.SetActive (true);
			Time.timeScale = 0;
		} else if(!isPaused && !isDead){
			restartButton.gameObject.SetActive (false);
			mainButton.gameObject.SetActive (false);
			Time.timeScale = 1;
		}

	}

	///Everytime a collectable is collected, it adds (or it might remove if a character grabs the incorrect collectable) a litte bonus
	public void ExtraBonus (int bonus)
	{
		collectableBonus += bonus;
	}

  

	///Game over, here's the score that the Admin grabs. The Player calls this function when he dies (loseLife()). 
	///It first adds the collectable bonus if there is any, converts the floaty score to an int and then to a String, since the admin needs a String to register the score 

	public void DeathHandler ()
	{
		isDead = true;
		score = distance * 1.25f;
		Debug.Log ("score before bounus " + (int)score);
		score += collectableBonus;
		Debug.Log ("score after bounus " + (int)score);
		int scoreDisplay = (int)score;
		scoreText.text = scoreDisplay.ToString ();
		scoreText.enabled = true;
		restartButton.gameObject.SetActive (true);
		mainButton.gameObject.SetActive (true);

       
	}

	///A canvas button calls this function
	public void ResetGame ()
	{
		SceneManager.LoadScene ("Game");

	}

	///This coroutine is called by the collectble, it displays through the alpha a text showing whether you're gaining or losing points;
	public IEnumerator TextCoroutine(int score){

		Color tempColor = new Color();
		if (score > 0) {
			
			tempColor.a = 1;
			tempColor.g = 1;
			bonusText.color = tempColor;
		} else {
			tempColor.a = 1;
			tempColor.r = 1;
			bonusText.color = tempColor;
		}
		bonusText.text = score.ToString ();
		yield return new WaitForSeconds (1f);
		tempColor.a=0;
		bonusText.color = tempColor;

	}
}




/*
        if (instance == null)
        {
            instance = this;
          //  DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        */


/*
//Game over, here's the score that the admin grabs. The player calls this function
public void DeathHandler()
{
  isDead = true;
  score = distance * 1.25f;
  int scoreInt = (int)score;
  Debug.Log("score befeor fbouns " + scoreInt);
  scoreInt += collectableBonus;
  Debug.Log("score aFtEr fbouns " + scoreInt);
  scoreText.text = scoreInt.ToString();
  scoreText.enabled = true;
  restartButton.gameObject.SetActive(true);
  mainButton.gameObject.SetActive(true);
}
*/
