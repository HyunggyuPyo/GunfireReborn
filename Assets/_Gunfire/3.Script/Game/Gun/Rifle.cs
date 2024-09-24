using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Gun
{
    public Sprite image;

    public override void InitSetting()
    {
        data.level = Random.Range(0, 4);
        data.damage = 10;
        data.delay = 0.5f;
        data.maxBullet = 30;
        data.bulletCount = data.maxBullet;
        data.info = "������ 1";
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
