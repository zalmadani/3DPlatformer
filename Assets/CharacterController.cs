using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    
    public float normalSpeed = 10.0f;
    public float sprintSpeed = 20.0f;

    float maxSpeed = 5.0f;
    float rotation = 0.0f;
    float camRotation = 0.0f;
    GameObject cam;
    Rigidbody myRigidbody; 

    bool isOnGround;
    public GameObject groundChecker;
    public LayerMask groundLayer;
    public float jumpForce = 300.0f;

    float rotationSpeed = 2.0f;
    float camRotationSpeed = 1.5f;
    Animator myAnim;

    Vector3 respawnPoint = new Vector3(-9.52f, 2.17f, 51.83f);
    void Start()
    {
        myAnim = GetComponentInChildren<Animator>();
        cam = GameObject.Find("Main Camera");
        myRigidbody = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    

    void Update()
    {
       
        isOnGround = Physics.CheckSphere(groundChecker.transform.position, 0.1f, groundLayer);

        if (isOnGround == true && Input.GetKeyDown(KeyCode.Space))
        {
            myRigidbody.AddForce(transform.up * jumpForce);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            maxSpeed = sprintSpeed;
        } else
        {
            maxSpeed = normalSpeed;
        }
        
        Vector3 newVelocity = transform.forward * Input.GetAxis("Vertical") * maxSpeed;
        myRigidbody.velocity = new Vector3(newVelocity.x, myRigidbody.velocity.y, newVelocity.z);
        
        rotation = rotation + Input.GetAxis("Mouse X");
        transform.rotation = Quaternion.Euler(new Vector3(0.0f, rotation, 0.0f));
    
        camRotation = camRotation + Input.GetAxis("Mouse Y"); 
        cam.transform.localRotation = Quaternion.Euler(new Vector3(camRotation, 0.0f, 0.0f));

        myAnim.SetFloat("speed", newVelocity.magnitude);

        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Death box")
            {
                transform.position = respawnPoint;
            }
        }

    }
}
