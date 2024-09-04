using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform playerPosition;

    private void LateUpdate()
    {
        transform.position = playerPosition.position;
    }
}
