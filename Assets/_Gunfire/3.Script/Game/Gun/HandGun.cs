using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGun : Gun
{
    public Sprite image;
    public AudioClip shopClip;
    public AudioClip reLoadClip;

    public override void InitSetting()
    {
        data.level = Random.Range(0, 4);
        data.damage = 14;
        data.delay = 0.7f;
        data.maxBullet = 25;
        bulletCount = data.maxBullet;
        data.info = "�ڵ��1";
        data.shotClip = shopClip;
        data.reLoadClip = reLoadClip;
        data.bullet = Resources.Load<GameObject>("DefultBullet");
        data.image = image;
    }

    public override void InitPool()
    {
        base.InitPool();
    }

    public override void ReLoad()
    {
        base.ReLoad();
    }

    //public override void Using(Transform sPos)
    //{
    //    base.Using(sPos);
    //}
    //todo �̰� �Լ� ���̽� �״�� ����ҰŸ� ���� ���⼭ ������ �ϴ� ������?
}
