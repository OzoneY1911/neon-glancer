using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class PlayerCam : MonoBehaviour
{
    internal static PlayerCam instance;

    [SerializeField] GameObject player;

    [SerializeField] Vector3 targetOffset;
    [SerializeField] float cameraSpeed;

    void Awake()
    {
        instance = this;
    }

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, player.transform.position + targetOffset, cameraSpeed * Time.deltaTime);
    }
}
