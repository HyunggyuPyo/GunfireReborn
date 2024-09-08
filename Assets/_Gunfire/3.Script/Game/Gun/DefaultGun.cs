using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultGun : Gun
{
    public override void InitSetting()
    {
        data.delay = 1f;
        data.maxBullet = 20;
        data.info = "�⺻ ����";
        //data.soundEffect = ;
        data.bullet = Resources.Load<GameObject>("DefultBullet");
    }

    public override void Using(Transform sPos)
    {
        base.Using(sPos);
    }
}