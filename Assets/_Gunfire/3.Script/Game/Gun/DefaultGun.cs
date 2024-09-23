using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultGun : Gun
{
    public Sprite image;

    public override void InitSetting()
    {
        data.delay = 1f;
        data.maxBullet = 20;
        data.bulletCount = data.maxBullet;
        data.info = "�⺻ ����";
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

    public override void ReLoad()
    {
        base.ReLoad();
    }
}
