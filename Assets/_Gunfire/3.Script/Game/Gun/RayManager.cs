using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayManager : MonoBehaviour
{
    public static RayManager Instance;

    bool targetGun;
    bool targetEnemy;
    LayerMask gunMask;
    public LayerMask targetMask;

    Camera mainCamera;

    [HideInInspector]
    public Vector3 ScreenCenter;
    [HideInInspector]
    public Vector3 centerPosition;

    public RaycastHit hit;
    public RaycastHit hitEnemy;

    private void Awake()
    {
        Instance = this;
        //todo : 차라리 이 스크립트를 rqy전용 스크립트로 바꾸고 레이가 필요한 스크립트에서 전부 참조
        // ㄴ 기존의 총을 줍기 위한 스크립트도 분리
        gunMask = (1 << LayerMask.NameToLayer("Gun"));
        //targetMask = (1 << LayerMask.NameToLayer("Enemy"));
    }

    private void Start()
    {
        mainCamera = Camera.main;
        ScreenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);

    }

    private void Update()
    {   
        Ray ray = mainCamera.ScreenPointToRay(ScreenCenter);
        centerPosition = ray.direction;

        targetGun = Physics.Raycast(ray, out hit, 30f, gunMask);

        targetEnemy = Physics.Raycast(ray, out hitEnemy, 30f, targetMask);
        //Debug.DrawRay(ray.origin, ray.direction * 30f, Color.red, 2f);
    }
}
