using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGun : Gun
{
    public Sprite image;

    public override void InitSetting()
    {
        data.delay = 0.8f;
        data.maxBullet = 25;
        data.bulletCount = data.maxBullet;
        data.info = "�ڵ��1";
        //data.soundEffect = ;
        data.bullet = Resources.Load<GameObject>("DefultBullet");
        data.image = image;
    }

    public override void InitPool()
    {
        base.InitPool();
    }

    public override void Using(Transform sPos)
    {
        base.Using(sPos);
    }
}
