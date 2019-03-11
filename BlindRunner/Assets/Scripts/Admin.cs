using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using UnityEngine.SceneManagement;

public class Admin : MonoBehaviour
{
    public static Admin instance;
    [HideInInspector]
    public int scoreControl,deathControl;
    [SerializeField] public Text scoreTx,deathTx;
    string actualScene = "";

    private void Awake()
    {
        
        
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
    }

    private void Update()
    {
		///Find the score text in the current scene. If the current scene is the menu, find and de-activate the canvas, in order load the data located in the canvas text
        if (actualScene != SceneManager.GetActiveScene().name)
        {
            try
            {
                scoreTx = GameObject.FindGameObjectWithTag("Score").GetComponent<UnityEngine.UI.Text>();
				if (SceneManager.GetActiveScene().name == "Menu")
                {
					deathTx =GameObject.FindGameObjectWithTag("Deaths").GetComponent<UnityEngine.UI.Text>();
                    LoadData();
                    scoreTx.transform.parent.gameObject.SetActive(false);
                }
            }
            catch
            {
                Debug.Log("not found");
            }

			///If we change the scene the update checks wheter we are in the menu or not
            actualScene = SceneManager.GetActiveScene().name;
        }
    }

    public void ResetData()
    {
        SaveScore data = new SaveScore();
        string path = Application.persistentDataPath + "/SaveScoreJson.dat";
        Debug.Log("I AM TRYING TO RESET");

        if (File.Exists(path)) // Check if the file for reset exists
        {
            data.highScore = 0;
            data.deaths = 0;
            StreamWriter sw = new StreamWriter(path);
            sw.WriteLine(JsonUtility.ToJson(data));
            sw.Close();

            print("Highscore is: " + data.highScore + ", deaths are " + data.deaths);
			LoadData ();

        }
        else
        {
            Debug.Log("I wasn't able to reset");
        }
    }

	/// SAVE PLAYER DATA  --> Called by the player (charMov's loseLife())
    public void SaveData()
    {
        int compareScore = 0;
        //Search for previous score data
		SaveScore data = new SaveScore();
        string path = Application.persistentDataPath + "/SaveScoreJson.dat";
        

        if (File.Exists(path)) // Check if the file for load exists
        {
            StreamReader sr = new StreamReader(path);
            data = JsonUtility.FromJson<SaveScore>(sr.ReadLine());
            compareScore = data.highScore;
			data.deaths++;
            sr.Close();
            print("Highscore until now: " + compareScore);
        }
        ////////////////////////

        // data = new SaveScore();
     
		print("Manager Controller score: " + (int)GameManager.instance.GetScore());
		scoreControl = (int)GameManager.instance.GetScore();
        if (scoreControl > compareScore)
        {
            data.highScore = scoreControl;
            print("New highscore: " + data.highScore);
			/*testing DEATH
            StreamWriter sw = new StreamWriter(path);
            sw.WriteLine(JsonUtility.ToJson(data));
            sw.Close();
			*/

        }
		StreamWriter sw = new StreamWriter(path);
		sw.WriteLine(JsonUtility.ToJson(data));
		sw.Close();
        
        print("Actual highscore is " + data.highScore);
    }


    /// LOAD PLAYER DATA
    public void LoadData()
    {
        string path = Application.persistentDataPath + "/SaveScoreJson.dat";

        if (File.Exists(path)) // Check if the file for load exists
        {
            StreamReader sr = new StreamReader(path);

            SaveScore data = new SaveScore();
            data = JsonUtility.FromJson<SaveScore>(sr.ReadLine());

           // sr.Close();


            scoreControl = data.highScore;
            scoreTx.text = scoreControl.ToString();
            //scoreTx.text = data.highScore.ToString();
			deathTx.text = data.deaths.ToString();
            print("File loaded from " + path);

            sr.Close();
        }

        // If the file does not exist
        else
        {
            print("File does not exist at " + Application.persistentDataPath);
        }
    }

}
[Serializable]
public class SaveScore
{
    public int highScore;
	public int deaths;
}
