using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///Handles the Doggo McPupperson and the Blind dude
public class CharMov : MonoBehaviour {

    private float movement;
    private float offset;

    public float speed;
    public bool isDoggoInControl;

	[HideInInspector]
    public GameObject visuallyImpaired;
    private LineRenderer _lineRenderer;

	private Animator doggoAnimator,blindAnimator;

    void Start () {
		


        isDoggoInControl = true;
        visuallyImpaired = GameObject.Find("BLIND");
        offset = Vector3.Distance(this.transform.position, visuallyImpaired.transform.position);


        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.startWidth = 0.2f;
        _lineRenderer.endWidth = 0.3f;

		doggoAnimator = GetComponent<Animator> ();
		blindAnimator = visuallyImpaired.GetComponent<Animator> ();
    }
	
	
	void Update () {


		//Character clamp, depending on who's guiding. Who's guiding depends on a public boolean, whom which is called by the glasses collectableHandler (isGuideSwapper = true)
        if (isDoggoInControl)
        {
			doggoAnimator.SetBool ("IsDoggoInControl", true);
			blindAnimator.SetBool ("IsDoggoInControl", true);
            if (transform.position.x < -2.40f)
            {
                transform.position = new Vector3(-2.40f, transform.position.y, 0f);

            }
            if (transform.position.x > 2.40f)
            {
                transform.position = new Vector3(2.40f, transform.position.y, 0f);

            }
        }
        else
        {
			doggoAnimator.SetBool ("IsDoggoInControl", false);
			blindAnimator.SetBool ("IsDoggoInControl", false);
            if (visuallyImpaired.transform.position.x < -2.40f)
            {
                visuallyImpaired.transform.position = new Vector3(-2.40f, visuallyImpaired.transform.position.y, 0f);

            }
            if (visuallyImpaired.transform.position.x > 2.40f)
            {
                visuallyImpaired.transform.position = new Vector3(2.40f, visuallyImpaired.transform.position.y, 0f);

            }
        }
       

        ///If the game hasn't ended we can move
		if (GameManager.instance.isDead == false) {
			if (isDoggoInControl) {
				///We use the 'movement' float to translate the character
				transform.position += new Vector3 (movement, 0f, 0f) * speed * Time.deltaTime;

				///the smaller the step the slower the blind follows, using v3.movetowards
				float step = 4 * Time.deltaTime;
				Vector3 blindPos = new Vector3 (this.transform.position.x, transform.position.y - offset, 0f);
				visuallyImpaired.transform.position = Vector3.MoveTowards (visuallyImpaired.transform.position, blindPos, step);

			} else {
				///We use the 'movement' float to translate the character (visually impaired)
				visuallyImpaired.transform.position += new Vector3 (movement, 0f, 0f) * speed * Time.deltaTime;

				///the smaller the step the slower the doggo follows, using v3.movetowards
				float step = 4 * Time.deltaTime;
				Vector3 doggoPos = new Vector3 (visuallyImpaired.transform.position.x, visuallyImpaired.transform.position.y + offset, 0f);
				transform.position = Vector3.MoveTowards (transform.position, doggoPos, step);

			}
           
		} else {
			///if the game has ended then we stop the animations
			GetComponent<Animator> ().enabled = false;
			GetComponent<SpriteRenderer> ().color = Color.grey;
			visuallyImpaired.GetComponent<SpriteRenderer> ().color = Color.grey;
			visuallyImpaired.GetComponent<Animator> ().enabled = false;
			GameObject blindArm = visuallyImpaired.transform.GetChild (0).gameObject;
			blindArm.GetComponent<SpriteRenderer> ().color = Color.grey;
		}

      	///Line handler and arm follow
		LineArmHandler ();
      

    }

	void LineArmHandler(){
        ///line renderer rendering lines, the arm looks at the doggo, the line render's end ends on the blind hand
		_lineRenderer.startWidth = 0.1f;
		_lineRenderer.endWidth = 0.1f;
		Vector3 lineStartPos = new Vector3 (transform.position.x, transform.position.y+0.5f, -1f);
		GameObject blindArm = visuallyImpaired.transform.GetChild (0).gameObject;
		blindArm.transform.up = lineStartPos - blindArm.transform.position;
        GameObject hand = blindArm.transform.GetChild(0).gameObject;
		Vector3 lineEndtPos = new Vector3 (hand.transform.position.x, hand.transform.position.y, 1f);
		_lineRenderer.SetPosition(0, lineStartPos);
		_lineRenderer.SetPosition (1, lineEndtPos);

	}

    ///These functions work with UI buttons
    public void GoLeft()
    {
        movement = -1;
    }
    public void GoRight()
    {
        movement = 1;
    }

    public void StopMoving()
    {
        movement = 0;
    }

    ///called from Obstaclesmovement class whenever the player hits an obstacle, saves the score, tells to the manager the game has ended
    public void LoseLife()
    {
		GameManager.instance.DeathHandler();
        Admin.instance.SaveData();
       
    }
}
