using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public float rotationSpeed = 1;
	public Transform Player, Focus, LookThrough;
	float xDirec, yDirec;
    float zoom = 4f;
    private Rigidbody camrb;
    Quaternion rotation; //fixes the child object otation


    void awake() //''child object rotation
    {
        rotation = transform.rotation;
    }

    // Assigns the camera lookthrough and gets camera rigidbody
    void Start()
    {
        camrb = GetComponent<Rigidbody>();
        LookThrough = Focus;
      

    }

    // LateUpdate is called after update
    void LateUpdate()
    {
        cameraMovement();
        thingInWay();
        transform.rotation = rotation;//''child object rotation
      

	}

    void cameraMovement()
    {
        //Get Users arrow key values, and clamp the y rotation
        xDirec += Input.GetAxis("Horizontal") * rotationSpeed;
        yDirec -= Input.GetAxis("Vertical") * rotationSpeed;
        yDirec = Mathf.Clamp(yDirec, -30f, 0.25f);
        
        //Point the camera towar the focus
        transform.LookAt(Focus);

        //Rotate the focus along the x and y axis
        Focus.rotation = Quaternion.Euler(-yDirec, -xDirec, 0);

    }



    void thingInWay()
    {
        RaycastHit hit;

        //Cast a ray from the camera toward the focus
        if(Physics.Raycast(transform.position, Focus.position - transform.position, out hit, 3f))
        {
            //If an object is hit that is a static part of the environment, then turn that objects mesh to cast shadows only
            if(hit.collider.gameObject.tag != "Player" && hit.collider.gameObject.tag != "Finish" && hit.collider.tag != "MovingPlatform")
            {
                LookThrough = hit.transform;
                LookThrough.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
                //If the distance from the environment to the camera is >= 2.5, and the distance from focus to camera is >=1, then move the camera forward
                if(Vector3.Distance(LookThrough.position, transform.position) >= 2.5f && Vector3.Distance(Focus.position, transform.position)>= 1f)
                {
                    transform.Translate(Vector3.forward * zoom * Time.deltaTime);
                }
            }
            else
            {
                //If nothing is hit, then set the environments mesh back on
                LookThrough.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                //If the camera distance from the player is <3f, then get the camera back to 3f
                if(Vector3.Distance(transform.position, Focus.position) < 3f)
                {
                    transform.Translate(Vector3.back * zoom * Time.deltaTime);
                }

            }
        }
    }
}
