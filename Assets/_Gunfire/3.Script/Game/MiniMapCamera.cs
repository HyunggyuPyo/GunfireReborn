using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCamera : MonoBehaviour
{
    Transform playerPosition;

    private void Start()
    {
        playerPosition = GameObject.Find("CameraPoint").transform;
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(playerPosition.position.x, playerPosition.position.y + 9f, playerPosition.position.z);
    }
}
