using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefaultGun : Gun
{
    public Sprite image;

    public override void InitSetting()
    {
        data.delay = 1f;
        data.maxBullet = 20;
        data.info = "±âº» ±ÇÃÑ";
        //data.soundEffect = ;
        data.bullet = Resources.Load<GameObject>("DefultBullet");
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
