using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    internal static PlayerMovement instance;

    public float movementSpeed;

    public bool canRotate = true;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
        GetComponent<Rigidbody>().freezeRotation = true;
    }

    void Update()
    {
        MovementInput();
        RotationInput();
    }

    void MovementInput()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput);

        Vector3 targetDir = PlayerCam.instance.transform.TransformDirection(movement);
        targetDir.y = 0;

        transform.Translate(movementSpeed * Time.deltaTime * targetDir.normalized, Space.World);
    }

    void RotationInput()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (canRotate && !PauseMenuController.gamePaused && Physics.Raycast(ray, out hit))
        {
            transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
        }
    }
}
