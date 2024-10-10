using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager Instance { get; private set; }

    public FirebaseApp App { get; private set; }
    public FirebaseAuth Auth { get; private set; }
    public FirebaseDatabase DB { get; private set; }

    public bool IsInitialized { get; private set; } = false;

    public DatabaseReference usersRef;
    public UserData userData;

    public event Action<FirebaseUser> onLogin;
    public event Action<string> onInviteMessage;

    public Dictionary<SkillType, int> skillData = new Dictionary<SkillType, int>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        onLogin += OnLogin;
        InitializeAsync();
    }

    void OnLogin(FirebaseUser user)
    {
        var invitRef = DB.GetReference($"invitation/{user.UserId}");
        //invitRef.ChildAdded += InvitationEventHandler;
        invitRef.ValueChanged += InvitationEventHandler;
    }

    async void InitializeAsync()
    {
        DependencyStatus status = await FirebaseApp.CheckAndFixDependenciesAsync();

        //if (status != DependencyStatus.Available) return;
        if (status == DependencyStatus.Available)
        {
            App = FirebaseApp.DefaultInstance;
            Auth = FirebaseAuth.DefaultInstance;
            DB = FirebaseDatabase.DefaultInstance;
            IsInitialized = true;
        }
        else
        {
            Debug.LogWarning($"연결 실패 : {status}");
            //todo : 네트워크 연결 실패 ui
        }
    }

    public async void SignUp(string email, string password, Action<FirebaseUser> callback = null)
    {
        try
        {
            var result = await Auth.CreateUserWithEmailAndPasswordAsync(email, password);

            callback?.Invoke(result.User);

            usersRef = DB.GetReference($"users/{result.User.UserId}");

            UserData userData = new UserData(result.User.UserId);

            string userDataJson = JsonConvert.SerializeObject(userData);

            await usersRef.SetRawJsonValueAsync(userDataJson);

            var invitRef = DB.GetReference($"invitation/{result.User.UserId}");

            await invitRef.SetValueAsync("");

            this.userData = userData;


            GetSkill();

            onLogin?.Invoke(result.User);
        }
        catch (FirebaseException e)
        {
            Debug.LogError(e.Message);
        }
    }

    public async void Login(string email, string password, Action<FirebaseUser> callback = null, Action<string> callback2 = null)
    {
        try
        {
            var result = await Auth.SignInWithEmailAndPasswordAsync(email, password);

            usersRef = DB.GetReference($"users/{result.User.UserId}");

            DataSnapshot userDataValues = await usersRef.GetValueAsync();

            if (userDataValues.Exists)
            {
                string json = userDataValues.GetRawJsonValue();
                userData = JsonConvert.DeserializeObject<UserData>(json);
                GetSkill();
                print(this.userData.soulPoint);
            }
            else
            {
                print("설마 오류난다고");
                throw new Exception();
            }

            onLogin?.Invoke(result.User);
            callback?.Invoke(result.User);
        }
        catch (FirebaseException e)
        {
            callback2?.Invoke("오류 404.");
        }
        catch (Exception e)
        {
            callback2?.Invoke("아이디가 존재하지 않습니다.");
        }     
 
    }

    public async void SetName(string name, Action callback = null)
    {
        var targetRef = usersRef.Child("userName");
        await targetRef.SetValueAsync(name);

        userData.userName = name;

        var invitRef = DB.GetReference($"userName/{name}");
        await invitRef.SetValueAsync(userData.userId);

        callback?.Invoke();
        //todo 닉네임 중복검사 만들기
    }

    public async void Search(string name, string inviter)
    {
        var invtiRef = DB.GetReference($"userName/{name}");
        DataSnapshot snapshot = await invtiRef.GetValueAsync();

        if(snapshot.Exists)
        {
            string result = snapshot.Value.ToString();
            SendInvitation(result, inviter);
        }
    }

    public void SendInvitation(string receiver, string inviter)
    {
        var invitRef = DB.GetReference($"invitation/{receiver}");
        invitRef.SetValueAsync("");
        invitRef.SetValueAsync(inviter);

        //var invitJson = JsonConvert.SerializeObject(msg);
        //invitRef.Child( msg.nickname).SetRawJsonValueAsync(invitJson);
        //invitRef.SetValueAsync(msg.nickname);
    }

    public void InvitationEventHandler(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError);
            print("여기서 오류");
            return;
        }

        string inviter = args.Snapshot.Value.ToString();    

        if (!string.IsNullOrEmpty(inviter))
        {
            onInviteMessage?.Invoke(inviter);
        }

        //수락이든 거절이든 버튼 받으면 값 다시 초기화 

        //var rawJson = args.Snapshot.GetRawJsonValue();

        //Message msg = JsonConvert.DeserializeObject<Message>(rawJson);

        //string msgString = $"{msg.nickname}님으로부터 초대받으셨습니다. \n 파티에 참가하시겠습니까?";

        //onInviteMessage?.Invoke(msgString);
    }

    async void GetSkill()
    {
        foreach (SkillType id in Enum.GetValues(typeof(SkillType)))
        {
            DatabaseReference skillRef = DB.GetReference($"skill/{userData.userId}/{id}");

            DataSnapshot snapshot = await skillRef.GetValueAsync();

            if (snapshot.Exists)
            {
                int value = Convert.ToInt32(snapshot.Value);
                skillData.Add(id, value);
            }
            else
            {
                skillData.Add(id, 0);
            }
        }
    }

    public async void SetSkill(SkillType id, Action callback = null)
    {
        DatabaseReference skillRef = DB.GetReference($"skill/{userData.userId}/{id}");

        skillData[id] += 1;

        await skillRef.SetValueAsync(skillData[id]);

        var soulRef = usersRef.Child("soulPoint");

        await soulRef.SetValueAsync(userData.soulPoint);

        callback?.Invoke();
    }
}
