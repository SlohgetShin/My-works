using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{

    private Rigidbody playerBody;
    public float movementSpeed;
    public Camera playerCam;
    private Transform player;
    [SerializeField] private GameObject death;


    private Vector3 direction;
    private float xDirec, yDirec;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody>();
        player = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        move();
        strafe();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "explosion")
        {
            death.GetComponent<sceneManager>().load("LoseScreen");
        }
    }

    private void move()
    {
        xDirec = Input.GetAxisRaw("Horizontal");
        yDirec = Input.GetAxisRaw("Vertical");

        direction = new Vector3(xDirec, 0f, yDirec);

        transform.rotation = Quaternion.Euler(playerCam.GetComponent<cameraController>().getCurrX(), playerCam.GetComponent<cameraController>().getCurrY(), 0f );

        player.Translate(direction*movementSpeed*Time.deltaTime, Space.Self);

        



    }

    private void strafe()
    {
        if (Input.GetKey("q"))
        {
            player.Translate(Vector3.up * movementSpeed * Time.deltaTime, Space.Self);

        }else if (Input.GetKey("e"))
        {
            player.Translate(-Vector3.up * movementSpeed * Time.deltaTime, Space.Self);
        }
    }
}
