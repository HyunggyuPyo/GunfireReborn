using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxSkill : CharacterSkill
{
    public Transform startPoint;

    private void Start()
    {
        startPoint = GetComponent<WeaponController>().startPosition;
    }

    private void Update()
    {
        UseSkill();    
    }

    public override void MainSkill()
    {
        // ������ ���� Ű�� �������� ray�� ������ ���� �� �����
        GameObject orb = Instantiate(data.mainSkillPrefab, transform);
        orb.transform.position = startPoint.position;
        print("����");
    }

    public override void SubSkill()
    {
        
    }
}
