using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{

	private float speed = 3f;
	private float maxControlledSpeed = 5f;
	public static Rigidbody rb;
	private float distToFloor;
	public Vector3 direction;
	public Vector3 relativeDirection;
	public Vector3 actSped;
	public Camera playerCam;
	private float xPlayDirec, yPlayDirec;
	AudioSource playerSounds;
	public AudioClip playerJump;
	public AudioClip playerRoll;
	public Scene lastScene;

	//Assign th camera, rigidbody, sound, grounded variable, and saves current scene for end screen
	void Start()
	{
		playerCam = Camera.main;
		rb = GetComponent<Rigidbody>();
		distToFloor = GetComponent<Collider>().bounds.extents.y;
		playerSounds = GetComponent<AudioSource>();
		DataSaved.setLastSceneName(SceneManager.GetActiveScene());

	}

	// Update is called once per frame
	void Update()
	{

		//Players moves
		playerMovement();

		//Players jump and double jump. Possibly move the isGrounded check to second if statement so rays aren't casted on each frame
		if (Input.GetKeyDown(KeyCode.Space) && isPlayerGrounded())
		{

			rb.AddForce(0, 500, 0, ForceMode.Force);
			jumpSoundPlay();
		}


		/* Double jump in contruction
		else if (Input.GetKeyDown(KeyCode.Space) && !isPlayerGrounded() && (transform.InverseTransformDirection(rb.velocity).y > -1.0f && transform.InverseTransformDirection(rb.velocity).y < 1.0f) && !hasDoubleJumped)
		{
			hasDoubleJumped = true;
		}

		//Controls the double jump behavior
		if (hasDoubleJumped && isPlayerGrounded())
		{
			hasDoubleJumped = false;
			rb.AddForce(0, 600, 0, ForceMode.Force);
		}
		*/

	}

	//Cast a ray to check if the player is touching the ground with a .1f margin
	private bool isPlayerGrounded()
	{
		return Physics.Raycast(transform.position, -Vector3.up, distToFloor + 0.1f);
	}


	void playerMovement()
	{	//Takes the players "wasd" values
		xPlayDirec = Input.GetAxis("VerticalPlayer");

		yPlayDirec = Input.GetAxis("HorizontalPlayer");

		//Set direction to match players current inputs
		direction = new Vector3(yPlayDirec, 0f, xPlayDirec);

		//Make the direction relative to camera vector3 excluding the y and set the actspeed to the rigidbody velocity
		relativeDirection = playerCam.transform.TransformVector(direction);
		relativeDirection.y = 0f;
		actSped = rb.velocity;

		//If the absolute value of velocity is less than maxspeed, thenadd more torque to the player
		if (Mathf.Abs(actSped.magnitude) < maxControlledSpeed)
		{
			rb.AddTorque(relativeDirection * -speed, ForceMode.Force);
		}

		/**if (isPlayerGrounded() && Mathf.Abs(actSped.magnitude) > 0)
        {
			rollSoundPlay();
        }
        else
        {
			stopSound();
        }*/
	}

	//Play the jump sound
	void jumpSoundPlay()
    {
		playerSounds.loop = false;
		playerSounds.volume = 1f;
		playerSounds.clip = playerJump;
		playerSounds.Play();
    }

	//Plays the roll sound, not played because the audio was awful
	void rollSoundPlay()
    {
		playerSounds.loop = true;
		playerSounds.clip = playerRoll;
		playerSounds.Play();
    }

	//Ends sound
	void stopSound()
    {
		playerSounds.Stop();
    }
}