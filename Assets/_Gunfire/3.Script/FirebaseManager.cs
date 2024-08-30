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

    

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
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

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            print($"userId : {userData.userId} \n  ");
        }

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
            Debug.LogWarning($"���� ���� : {status}");
            //todo : ��Ʈ��ũ ���� ���� ui
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

            this.userData = userData;

            onLogin?.Invoke(result.User);
        }
        catch (FirebaseException e)
        {
            Debug.LogError(e.Message);
        }
    }
    
    public async void Login(string email, string password, Action<FirebaseUser> callback = null)
    {
        var result = await Auth.SignInWithEmailAndPasswordAsync(email, password);

        usersRef = DB.GetReference($"users/{result.User.UserId}");

        DataSnapshot userDataValues = await usersRef.GetValueAsync();

        if(userDataValues.Exists)
        {
            string json = userDataValues.GetRawJsonValue();
            userData = JsonConvert.DeserializeObject<UserData>(json);
        }
        else
        {
            //todo ����â ����
        }

        onLogin?.Invoke(result.User);
        callback?.Invoke(result.User);
    }

    public async void SetName(string name, Action callback = null)
    {
        var targetRef = usersRef.Child("userName");
        await targetRef.SetValueAsync(name);

        userData.userName = name;

        callback?.Invoke();
        //todo �г��� �ߺ��˻� �����
    }

    public async void FindNickName(string name)
    {
        //DB.GetReference($"users
        // �ٵ� �̷����ϸ� ������ 1000���̸� 1000�� �� �˻� �ؾ� �ϴµ�? 
    }

    public void SendInvitation(string receiver, Message msg)
    {
        var invitRef = DB.GetReference($"invitation/{receiver}");
        invitRef.SetValueAsync(msg);



    }

    public void InvitationEventHandler(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError);
            return;
        }

        string inviter = args.Snapshot.Value.ToString();
        if (!string.IsNullOrEmpty(inviter))
        {
            Debug.Log($"{inviter}�� ���� �ʴ�");
        }
        onInviteMessage?.Invoke(inviter);



    }
}
