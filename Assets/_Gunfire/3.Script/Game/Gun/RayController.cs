using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayController : MonoBehaviour
{
    public static RayController Instance;

    bool targetGun;
    bool shotObj;
    public bool targetEnemy;

    LayerMask gunMask;
    LayerMask EnemyMask;
    public LayerMask targetMask;

    public 
        Camera mainCamera;

    [HideInInspector]
    public Vector3 ScreenCenter;
    [HideInInspector]
    public Vector3 centerPosition;

    public RaycastHit hit;
    public RaycastHit hitObj;
    public RaycastHit hitEnemy;

    private void Awake()
    {
        Instance = this;
        //todo : ���� �� ��ũ��Ʈ�� rqy���� ��ũ��Ʈ�� �ٲٰ� ���̰� �ʿ��� ��ũ��Ʈ���� ���� ����
        // �� ������ ���� �ݱ� ���� ��ũ��Ʈ�� �и�
        gunMask = (1 << LayerMask.NameToLayer("Gun"));
        EnemyMask = (1 << LayerMask.NameToLayer("Enemy"));
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
        targetEnemy = Physics.Raycast(ray, out hitEnemy, 30f, EnemyMask);
        shotObj = Physics.Raycast(ray, out hitObj, 30f, targetMask);
        //Debug.DrawRay(ray.origin, ray.direction * 30f, Color.red, 2f);
    }
}
