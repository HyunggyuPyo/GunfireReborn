using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGun : Gun
{
    public Sprite image;

    public override void InitSetting()
    {
        data.level = Random.Range(0, 4);
        data.damage = 14;
        data.delay = 0.7f;
        data.maxBullet = 25;
        data.bulletCount = data.maxBullet;
        data.info = "ÇÚµå°Ç1";
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
