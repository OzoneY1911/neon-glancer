using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    internal static PlayerCam instance;

    [SerializeField] GameObject player;

    [SerializeField] Vector3 targetOffset;
    [SerializeField] float cameraSpeed;

    float cameraRotateSpeed = 3.5f;
    bool cameraIsRotating;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !cameraIsRotating)
        {
            StartCoroutine(RotateCam(-cameraRotateSpeed));
        }
        else if (Input.GetKeyDown(KeyCode.E) && !cameraIsRotating)
        {
            StartCoroutine(RotateCam(cameraRotateSpeed));
        }
    }

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, player.transform.position + targetOffset, cameraSpeed * Time.deltaTime);
    }

    IEnumerator RotateCam(float degree)
    {
        cameraIsRotating = true;

        for (int i = 0; i < 90 / Mathf.Abs(degree); i++)
        {
            transform.RotateAround(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z), Vector3.up, degree);
            targetOffset = Quaternion.AngleAxis(degree, Vector3.up) * targetOffset;

            yield return null;
        }

        cameraIsRotating = false;
    }
}
