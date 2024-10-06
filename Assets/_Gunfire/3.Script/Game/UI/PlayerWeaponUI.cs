using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerWeaponUI : MonoBehaviour
{
    public static PlayerWeaponUI Instance;

    public Image[] numImage;
    public Image gunImage;
    public TMP_Text bulletCount;

    public Image dushCool;
    public TMP_Text count;

    private void Awake()
    {
        Instance = this;
        
    }

    void HoldGunsMark()
    {
        numImage[0].gameObject.SetActive(true);
    }

    public void ImageChange(Sprite img) 
    {
        print("이게 된다고333?");
        gunImage.sprite = img;
    }


    public IEnumerator CoolTime()
    {
        dushCool.gameObject.SetActive(true);
        float cool = 2;
        dushCool.fillAmount = 1;

        while(cool >= 0)
        {
            cool -= Time.deltaTime;
            dushCool.fillAmount = cool / 2;
            count.text = ((int)cool + 1).ToString();
            yield return new WaitForFixedUpdate();
        }

        dushCool.gameObject.SetActive(false);
    }
}

/*
   재능스킬 페이지   -> 0
 끝나고 돈이랑 영혼석 획득 화면 (게임 정산화면)  -> 0
 게임 오버 없음    -> 0
 플레이어 실드 회복도 없음 -> 0
 넥네임 중복 검사
 검은 화면으로 시작하고 캐릭터 생성되면 콜백으로 화면 내리기(로딩창 처럼 보이기) -> 0
 에임 이미지 그라데이션 넣어주기 투명) -> 0
 대시 방향 늘리기 -> 0 
 헤드샷 효과 추가  -> 0
 pause 만들기 
 보스 체력바
 */