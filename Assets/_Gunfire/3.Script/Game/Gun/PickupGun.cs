using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupGun : MonoBehaviour
{
    bool target;
    LayerMask gunMask;

    Camera mainCamera;
    Vector3 ScreenCenter;

    private void Awake()
    {
        gunMask = (1 << LayerMask.NameToLayer("Gun"));
    }

    private void Start()
    {
        mainCamera = Camera.main;
        ScreenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);

    }

    private void Update()
    {   
        Ray ray = mainCamera.ScreenPointToRay(ScreenCenter);
        RaycastHit hit;

        target = Physics.Raycast(ray, out hit, 1.5f, gunMask);
        //Debug.DrawRay(ray.origin, ray.direction * 1.5f, Color.red, 2f);
    }
}
