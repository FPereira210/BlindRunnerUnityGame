/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Dog McDoggo and visually impaired movement

[RequireComponent(typeof(SpriteRenderer))]
public class CharacterMovement : MonoBehaviour
{

    private LineRenderer _lineRenderer;
    private Rigidbody2D _rBody;
    private float movement;
    [SerializeField]
    private GameObject blind;
    private float offset;
    private Vector3 playerStartPos, blindStartPos;
    [SerializeField]
    private float respawnTime = 2f;
    private bool isJumping, isRespawning;
    

    private bool isBlind;

    public float doggoSpeed = 1;
    public float delayTime = 0.5f;

    void Start()
    {
        
        playerStartPos = transform.position;
        blindStartPos = blind.transform.position;
        _rBody = GetComponent<Rigidbody2D>();
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.startWidth = 0.2f;
        _lineRenderer.endWidth = 0.3f;
        offset = Vector3.Distance(this.transform.position, blind.transform.position);
        isBlind = true;
    }


    void Update()
    {
        //If we are not dead we can move
		if (GameManager.instance.isDead == false)
        {
            if (transform.position.x < -2.56f)
            {
                transform.position = new Vector3(-2.56f, transform.position.y, 0f);
            }
            if (transform.position.x > 2.56f)
            {
                transform.position = new Vector3(2.56f, transform.position.y, 0f);
            }

            //line renderer 0 --> player / line renderer 1 -->  visually impaired
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, blind.transform.position);

            //movement = Input.GetAxis ("Horizontal");
            transform.position += new Vector3(movement, 0f, 0f) * doggoSpeed;

            if (isBlind)
            {
                Vector3 blindPos = new Vector3(this.transform.position.x, transform.position.y - offset, 0f);
                blind.transform.position = Vector3.MoveTowards(blind.transform.position, blindPos, delayTime);
            }

        }
        else
        {
            StopAllCoroutines();
        }

        /*
        if (isJumping)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, transform.localScale + new Vector3(0.7f, 0.7f, 0),0.3f);
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale,startScale, 0.1f);
        }
        */
        /*
    }
    
    // Go<--, Go-->, stop and jump 
    public void GoLeft()
    {
        if (!isJumping)
        {

            StartCoroutine(FollowCoroutine());
            movement = -1;
        }

    }
    public void GoRight()
    {
        if (!isJumping)
        {
            //stahp al coroutines
            StartCoroutine(FollowCoroutine());
            movement = 1;
        }

    }
    public void StopMoving()
    {
        movement = 0;
        isBlind = false;
    }

    public void Jump()
    {
        if (!isJumping)
        {
            // StartCoroutine("JumpCoroutine");
        }

    }

    IEnumerator FollowCoroutine()
    {
        isBlind = false;
        //yield return new WaitWhile(() => isBlind == false);
        yield return new WaitForSeconds(delayTime);
        //Vector3 blindPos = new Vector3 (this.transform.position.x,transform.position.y- offset, 0f);
        //blind.transform.position= blindPos;
        //blind.transform.position = Vector3.MoveTowards(blind.transform.position,blindPos,delayTime);
        isBlind = true;
    }
    /*
	IEnumerator RespawnCoroutine(){
        isJumping = false;
		GetComponent<BoxCollider2D> ().enabled = false;
		blind.GetComponent<BoxCollider2D> ().enabled = false;
		GetComponent<SpriteRenderer> ().color = Color.red;
		blind.GetComponent<SpriteRenderer> ().color = Color.red;
		yield return new WaitForSeconds (respawnTime);
		GetComponent<BoxCollider2D> ().enabled = true;
		blind.GetComponent<BoxCollider2D> ().enabled = true;
		GetComponent<SpriteRenderer> ().color = Color.white;
		blind.GetComponent<SpriteRenderer> ().color = Color.white;

	}
    
    IEnumerator JumpCoroutine()
    {

        isJumping = true;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().color = Color.green;


        yield return new WaitForSeconds(1f);
        isJumping = false;

        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<SpriteRenderer>().color = Color.white;


        blind.GetComponent<BoxCollider2D>().enabled = false;
        blind.GetComponent<SpriteRenderer>().color = Color.green;

        yield return new WaitForSeconds(1f);

        blind.GetComponent<BoxCollider2D>().enabled = true;
        blind.GetComponent<SpriteRenderer>().color = Color.white;
    }
    /*
	public void LoseLife(){
        ManagerController.instance.ResetGame();
		transform.position = playerStartPos;
		blind.transform.position = blindStartPos;
        StopAllCoroutines();
		StartCoroutine ("RespawnCoroutine");
	}
    

    public void LoseLife()
    {
		GameManager.instance.DeathHandler();

    }
}
*/