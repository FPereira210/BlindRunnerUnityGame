using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.IO;

public class Tests : MonoBehaviour
{


    [UnityTest]
    public IEnumerator TestAssetsAreNotNull()
    {
        SceneManager.LoadScene("Game");
        yield return null;
        GameObject player = GameObject.FindGameObjectWithTag("Doggo");
        Assert.IsNotNull(player);
        CharMov charMov = player.GetComponent<CharMov>();
        Assert.IsNotNull(charMov.visuallyImpaired);
    }


    [UnityTest]
    public IEnumerator TestAdminMenu()
    {
        SceneManager.LoadScene("Menu");
        yield return null;
        GameObject admin = GameObject.FindGameObjectWithTag("Admin");
        Admin adminScript = admin.GetComponent<Admin>();
        Assert.IsNotNull(adminScript.scoreTx);
        Assert.IsNotNull(adminScript.deathTx);

        SceneManager.LoadScene("Game");
        yield return null;
        Assert.IsNotNull(adminScript.scoreTx);

    }

    [UnityTest]
    public IEnumerator TestCollision()
    {
        SceneManager.LoadScene("Game");
        yield return null;

        GameObject player = GameObject.FindGameObjectWithTag("Doggo");
        GameObject manager = GameObject.Find("Manager");
        GameObject spawner = GameObject.Find("Spawner");
        GameObject enemy = Instantiate(spawner.GetComponent<SpawnManager>().spawnedObstacle[0], player.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        Assert.IsTrue(manager.GetComponent<GameManager>().isDead);
    }


    [UnityTest]
    public IEnumerator TestBoneDog()
    {
        SceneManager.LoadScene("Game");
        yield return null;
        GameObject player = GameObject.FindGameObjectWithTag("Doggo");
        GameObject manager = GameObject.Find("Manager");
        GameObject spawner = GameObject.Find("Spawner");

        GameObject bone = Instantiate(spawner.GetComponent<SpawnManager>().spawnedObstacle[2], player.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1);
        Assert.Greater(manager.GetComponent<GameManager>().collectableBonus, 0);


    }

    [UnityTest]
    public IEnumerator TestBoneBlind()
    {
        SceneManager.LoadScene("Game");
        yield return null;
        GameObject blind = GameObject.FindGameObjectWithTag("Blind");
        GameObject manager = GameObject.Find("Manager");
        GameObject spawner = GameObject.Find("Spawner");

        GameObject bone = Instantiate(spawner.GetComponent<SpawnManager>().spawnedObstacle[2], blind.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1);
        Assert.Less(manager.GetComponent<GameManager>().collectableBonus, 0);


    }

    [UnityTest]
    public IEnumerator TestCoinDog()
    {
        SceneManager.LoadScene("Game");
        yield return null;
        GameObject player = GameObject.FindGameObjectWithTag("Doggo");
        GameObject manager = GameObject.Find("Manager");
        GameObject spawner = GameObject.Find("Spawner");

        GameObject bone = Instantiate(spawner.GetComponent<SpawnManager>().spawnedObstacle[3], player.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1);
        Assert.Less(manager.GetComponent<GameManager>().collectableBonus, 0);


    }

    [UnityTest]
    public IEnumerator TestCoinBlind()
    {
        SceneManager.LoadScene("Game");
        yield return null;
        GameObject blind = GameObject.FindGameObjectWithTag("Blind");
        GameObject manager = GameObject.Find("Manager");
        GameObject spawner = GameObject.Find("Spawner");

        GameObject bone = Instantiate(spawner.GetComponent<SpawnManager>().spawnedObstacle[3], blind.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1);
        Assert.Greater(manager.GetComponent<GameManager>().collectableBonus, 0);
    }

    [UnityTest]
    public IEnumerator TestCoinSwapperBlind()
    {
        SceneManager.LoadScene("Game");
        yield return null;
        GameObject blind = GameObject.FindGameObjectWithTag("Blind");
        GameObject player = GameObject.FindGameObjectWithTag("Doggo");
        GameObject manager = GameObject.Find("Manager");
        GameObject spawner = GameObject.Find("Spawner");

        GameObject coin = Instantiate(spawner.GetComponent<SpawnManager>().spawnedObstacle[4], blind.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1);
        Assert.IsFalse(player.GetComponent<CharMov>().isDoggoInControl);
        Assert.Greater(manager.GetComponent<GameManager>().collectableBonus, 0);
    }
    [UnityTest]
    public IEnumerator TestCoinSwapperDoggo()
    {
        SceneManager.LoadScene("Game");
        yield return null;
        GameObject blind = GameObject.FindGameObjectWithTag("Blind");
        GameObject player = GameObject.FindGameObjectWithTag("Doggo");
        GameObject manager = GameObject.Find("Manager");
        GameObject spawner = GameObject.Find("Spawner");

        GameObject coin = Instantiate(spawner.GetComponent<SpawnManager>().spawnedObstacle[4], player.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        Assert.IsFalse(player.GetComponent<CharMov>().isDoggoInControl);
        Assert.Greater(manager.GetComponent<GameManager>().collectableBonus, 0);
    }

	[UnityTest]
	public IEnumerator TestCollectibleText(){
		SceneManager.LoadScene("Game");
		yield return null;
		GameObject player = GameObject.FindGameObjectWithTag("Doggo");
		GameObject manager = GameObject.Find("Manager");
		yield return null;
		Assert.IsNotNull(manager.GetComponent<GameManager>().bonusText);

	}

    [UnityTest]
    public IEnumerator TestLoad()
    {
        SceneManager.LoadScene("Menu");
        yield return null;
        GameObject admin = GameObject.Find("Admin");

        SaveScore data = new SaveScore();
        string path = Application.persistentDataPath + "/SaveScoreJson.dat";
        StreamReader sr = new StreamReader(path);
        data = JsonUtility.FromJson<SaveScore>(sr.ReadLine());
        sr.Close();

        admin.GetComponent<Admin>().LoadData();
        int score = admin.GetComponent<Admin>().scoreControl;

        Assert.AreEqual(data.highScore, score);
    }

    [UnityTest]
    public IEnumerator TestSave()
    {
        SceneManager.LoadScene("Game");
        yield return null;
        GameObject admin = GameObject.Find("Admin");


        SaveScore data = new SaveScore();
        string path = Application.persistentDataPath + "/SaveScoreJson.dat";
        StreamReader sr = new StreamReader(path);
        data = JsonUtility.FromJson<SaveScore>(sr.ReadLine());
        sr.Close();

        Debug.Log("before: " + data.deaths);
        int deathCompare = data.deaths;


        admin.GetComponent<Admin>().SaveData();

        sr = new StreamReader(path);
        data = JsonUtility.FromJson<SaveScore>(sr.ReadLine());
        sr.Close();
        Debug.Log("after: " + data.deaths);


        Assert.Less(deathCompare, data.deaths);
    }


    [UnityTest]
    public IEnumerator TestCat()
    {
        SceneManager.LoadScene("Game");
        yield return null;
        GameObject cat = GameObject.Find("fleed");
        GameObject spawner = GameObject.Find("Spawner");

        spawner.GetComponent<SpawnManager>().isOccupied[1] = true;

        yield return new WaitForSeconds(0.5f);
        Assert.AreNotEqual(1, cat.GetComponent<CatFleed>().lane);

    }

    }

    /*
    [UnityTest]
    public IEnumerator TestCollision()
    {
        SceneManager.LoadScene("Game");
        yield return null;

        GameObject player = GameObject.FindGameObjectWithTag("Doggo");
        GameObject manager = GameObject.Find("Manager");
		GameObject spawner = GameObject.Find("Spawner");
		yield return new WaitForSeconds(spawner.GetComponent<SpawnManager>().maxSpawnTime);
        GameObject enemy = GameObject.FindGameObjectWithTag("Obstacle");
        enemy.transform.position = player.transform.position + player.transform.position;
        yield return new WaitForSeconds(0.5f);
        Assert.IsTrue(manager.GetComponent<GameManager>().isDead);
      

    }
    */

    /*
	[UnityTest]
	public IEnumerator TestCollectable()
	{
		SceneManager.LoadScene("Game");
		yield return null;
        GameObject player = GameObject.FindGameObjectWithTag("Doggo");
        GameObject manager = GameObject.Find("Manager");
        GameObject spawner = GameObject.Find("Spawner");
        spawner.GetComponent<SpawnManager>().collectableTime = true;
        yield return new WaitForSeconds(spawner.GetComponent<SpawnManager>().maxSpawnTime);
        GameObject collectable = GameObject.FindGameObjectWithTag("Collectable");
        collectable.transform.position = player.transform.position + player.transform.position;
        yield return new WaitForSeconds(0.5f);
        Assert.AreNotEqual(0, manager.GetComponent<GameManager>().collectableBonus);
    }
    */
    //TODO -Colisões, -Pontos, -JSon, -Assets nao null
    /*
	[UnityTest]
	public IEnumerator TestPlayerPos(){
		SceneManager.LoadScene ("Game");
		yield return null;
		GameObject player = GameObject.FindGameObjectWithTag ("Doggo");
		CharMov charMov = player.GetComponent<CharMov> ();

		charMov.GoLeft ();
		yield return new WaitWhile(()=>player.transform.position.x==-2.40f);
		charMov.GoRight ();
		yield return new WaitWhile(()=>player.transform.position.x==2.40f);
	}
	*/

