using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxSkill : CharacterSkill
{
    private void Update()
    {
        UseSkill();    
    }

    public override void MainSkill()
    {
        // 던지지 말고 키를 눌렀을때 ray에 맞으면 봉인 및 대미지
    }

    public override void SubSkill()
    {
        
    }
}
