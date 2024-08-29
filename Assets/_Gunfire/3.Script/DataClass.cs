using System;

[Serializable]
public class UserData
{

    public string userId;
    public string userName;
    //todo : ��ɽ�ų ������ �߰�
    public int soulPoint;

    public UserData() { }

    public UserData(string userId, string userName, int soul)
    {
        this.userId = userId;
        this.userName = userName;
        this.soulPoint = soul;
    }

    public UserData(string userId)//, string userName
    {
        this.userId = userId;
        //this.userName = userName;         todo : �г��� ���� �����
        userName = $"{userId}";
        soulPoint = 0;
    }
}

[Serializable]
public class Message
{
    public string sender;
}