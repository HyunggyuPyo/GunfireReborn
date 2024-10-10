using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultGun : Gun
{
    public Sprite image;
    public AudioClip shopClip;
    public AudioClip reLoadClip;

    public override void InitSetting()
    {
        data.level = 0;
        data.damage = 10;
        data.delay = 0.8f;
        data.maxBullet = 20;
        bulletCount = data.maxBullet;
        data.info = "±âº» ±ÇÃÑ";
        data.shotClip = shopClip;
        data.reLoadClip = reLoadClip;
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
