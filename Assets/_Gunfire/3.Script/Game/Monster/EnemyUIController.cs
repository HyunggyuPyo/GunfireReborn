using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemyUIController : MonoBehaviour
{
    public static EnemyUIController Instance;
    public GameObject uiPrefab;
    int pointer;

    //[HideInInspector]
    //public bool inConnect = false;

    List<GameObject> uiList = new List<GameObject>();

    public Dictionary<GameObject, GameObject> uiDic = new Dictionary<GameObject, GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject uiBar = Instantiate(uiPrefab, transform);
            uiBar.SetActive(false);
            uiList.Add(uiBar);
        }

        pointer = 0;
    }

    private void Update()
    {
        if(GameManager.Instance.isConnect && RayController.Instance.targetEnemy)
        {
            print($"{RayController.Instance.hitEnemy.collider.gameObject}°¡ ¸ÂÀ½");

            if (!uiDic.ContainsKey(RayController.Instance.hitEnemy.collider.gameObject))
            {
                if (pointer != uiList.Count)
                {
                    uiDic.Add(RayController.Instance.hitEnemy.collider.gameObject, uiList[pointer]);
                    uiList[pointer].GetComponent<EnemyUI>().targetObj = RayController.Instance.hitEnemy.collider.gameObject;
                    uiList[pointer].SetActive(true);
                    pointer++;
                }
                else
                {
                    pointer = 0;
                    uiDic.Add(RayController.Instance.hitEnemy.collider.gameObject, uiList[pointer]);
                    uiList[pointer].GetComponent<EnemyUI>().targetObj = RayController.Instance.hitEnemy.collider.gameObject;
                    uiList[pointer].SetActive(true);
                    pointer++;
                }
            }
        }
    }

    private void LateUpdate()
    {
        foreach (var hpBar in uiDic)
        {
            if (hpBar.Value.activeSelf)
            {
                float height = hpBar.Key.GetComponent<Enemy>().MonsterData.uiHeight;
                hpBar.Value.transform.position = RayController.Instance.mainCamera.WorldToScreenPoint(hpBar.Key.transform.position + new Vector3(0, height, 0));
            }
        }
    }

}
