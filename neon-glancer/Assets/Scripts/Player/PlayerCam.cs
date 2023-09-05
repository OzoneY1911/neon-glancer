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

    bool cameraIsRotating;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !cameraIsRotating)
        {
            targetOffset = Quaternion.AngleAxis(-90f, Vector3.up) * targetOffset;
            StartCoroutine(RotateCam(-1f));
        }
        else if (Input.GetKeyDown(KeyCode.E) && !cameraIsRotating)
        {
            targetOffset = Quaternion.AngleAxis(90f, Vector3.up) * targetOffset;
            StartCoroutine(RotateCam(1f));
        }
    }

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, player.transform.position + targetOffset, cameraSpeed * Time.deltaTime);
    }

    IEnumerator RotateCam(float degree)
    {
        cameraIsRotating = true;

        for (int i = 0; i < 90; i++)
        {
            transform.RotateAround(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z), Vector3.up, degree);
            yield return null;
        }

        cameraIsRotating = false;
    }
}
