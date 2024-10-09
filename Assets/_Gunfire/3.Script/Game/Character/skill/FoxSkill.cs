using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxSkill : CharacterSkill
{
    public static FoxSkill instance;
    public Transform startPoint;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        UseSkill();    
    }

    public override void MainSkill()
    {
        GameObject orb = Instantiate(data.mainSkillPrefab, transform);
        orb.transform.position = startPoint.position;
    }

    public override void SubSkill()
    {
        GameObject orb = Instantiate(data.subSkillPrefab, transform);
        orb.transform.position = startPoint.position;
        orb.transform.rotation = startPoint.rotation;
    }
}
