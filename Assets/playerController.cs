using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class playerController : MonoBehaviour
{
   public float speed = 10;
    float jumpPower = 6;
   public Camera c;
    public float mouseSensitivity = 90.0f;
    public float clampAngle = 80.0f;

    private float rotY = 0.0f; // rotation around the up/y axis
    private float rotX = 0.0f; // rotation around the right/x axis
    public TextMeshProUGUI t;
    Vector3 velocoty = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 rot = c.transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
    }


    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");

        rotY += mouseX * mouseSensitivity * Time.deltaTime;
        rotX += mouseY * mouseSensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        c.transform.rotation = localRotation;

        velocoty = Vector3.zero;
        
        if (Input.GetKey(KeyCode.D))
        {
            velocoty.x = 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            velocoty.x = -1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            velocoty.z = -1;
        }
        if (Input.GetKey(KeyCode.W))
        {
            velocoty.z = 1;
        }

        bool isOnGround = Physics.Raycast(transform.position, Vector3.up * -1, 2.5f);
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        
        
        //transform.position += (velocoty * Time.deltaTime * speed);
        //Vector3 addPos = new Vector3(velocoty.x * Time.deltaTime * speed, 0, (velocoty.y * Time.deltaTime * speed));
        Vector3 forwardVec = c.transform.forward * velocoty.z;
        Vector3 rightVec = c.transform.right * velocoty.x;
        GetComponent<Rigidbody>().AddForce(forwardVec*speed, ForceMode.Force);
        GetComponent<Rigidbody>().AddForce(rightVec * speed, ForceMode.Force);
       // t.text = velocoty.ToString() + " " + (forwardVec * speed) + " " + (rightVec * speed);
        // GetComponent<Rigidbody>().(transform.position + addPos);

        
    }
}
