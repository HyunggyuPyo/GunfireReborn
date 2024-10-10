using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Gun
{
    public Sprite image;
    public AudioClip shopClip;
    public AudioClip reLoadClip;

    public override void InitSetting()
    {
        data.level = Random.Range(0, 4);
        data.damage = 12;
        data.delay = 0.5f;
        data.maxBullet = 30;
        bulletCount = data.maxBullet;
        data.info = "∂Û¿Ã«√ 1";
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
