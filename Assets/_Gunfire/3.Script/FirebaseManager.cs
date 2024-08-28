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

    public DatabaseReference usersRef;
    public UserData userData;

    public bool IsInitialized { get; private set; } = false;

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        InitializeAsync();
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

            this.userData = userData;
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
            //todo 에러창 띄우기
        }

        callback?.Invoke(result.User);
    }

    public async void SetName(string name, Action callback = null)
    {
        var targetRef = usersRef.Child("userName");
        await targetRef.SetValueAsync(name);

        userData.userName = name;

        callback?.Invoke();
        //todo 닉네임 중복검사 만들기
    }
}
