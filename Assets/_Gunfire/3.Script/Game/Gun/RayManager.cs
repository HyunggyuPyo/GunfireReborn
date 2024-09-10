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
        //todo : ���� �� ��ũ��Ʈ�� rqy���� ��ũ��Ʈ�� �ٲٰ� ���̰� �ʿ��� ��ũ��Ʈ���� ���� ����
        // �� ������ ���� �ݱ� ���� ��ũ��Ʈ�� �и�
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
