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
        data.info = "핸드건1";
        //data.soundEffect = ;
        data.bullet = Resources.Load<GameObject>("DefultBullet");
        data.image = image;
    }

    public override void InitPool()
    {
        base.InitPool();
    }

    //public override void Using(Transform sPos)
    //{
    //    base.Using(sPos);
    //}
    //todo 이거 함수 베이스 그대로 사용할거면 굳이 여기서 재정의 하는 이유가?
}
