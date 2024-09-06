using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Transform playerPosition;
    Transform lookTarget;

    //private IEnumerator Start()
    //{
    //    yield return new WaitUntil(() => GameObject.Find("LocalFox(Clone)"));

    //    playerPosition = GameObject.Find("CameraPoint").transform;
    //    lookTarget = GameObject.Find("CameraLookTarget").transform;
    //}

    private void Start()
    {
        playerPosition = GameObject.Find("CameraPoint").transform;
        lookTarget = GameObject.Find("CameraLookTarget").transform;
    }

    private void LateUpdate()
    {
        transform.position = playerPosition.position;
        transform.LookAt(lookTarget);
    }
}
