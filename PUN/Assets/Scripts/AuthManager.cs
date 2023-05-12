using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AuthManager : MonoBehaviour
{
    public bool isFirebaseReady { get; private set; }
    public bool isSigninOnProgress { get; private set; }

    public InputField emailField;
    public InputField passwordField;
    public Button signInButton;

    public static FirebaseApp firebaseApp;
    public static FirebaseAuth firebaseAuth;

    public static FirebaseUser firebaseUser;
    public static string userId;

    void Start()
    {

        signInButton.interactable = false;

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task=>
        {
            var result = task.Result;

            if (result != DependencyStatus.Available)
            {
                Debug.LogError(result.ToString());
                isFirebaseReady = false;
                return;
            }
            Debug.Log(result.ToString());

            isFirebaseReady = true;
            firebaseApp = FirebaseApp.DefaultInstance;
            firebaseAuth = FirebaseAuth.DefaultInstance;

            signInButton.interactable = true;
        });
    }

    public void SignIn()
    {
        if (!isFirebaseReady || isSigninOnProgress || firebaseUser != null)
            return;

        isSigninOnProgress = true;

        firebaseAuth.SignInWithEmailAndPasswordAsync(emailField.text, passwordField.text).ContinueWithOnMainThread(task =>
        {
            Debug.Log(task.Status);
            isSigninOnProgress = false;

            if (task.IsFaulted)
                Debug.LogError(task.Exception);
            else if (task.IsCanceled)
                Debug.LogError("task cancled");
            else
            {
                firebaseUser = task.Result.User;
                userId = string.Copy(firebaseUser.Email);
                
                Debug.Log("Success! : " + firebaseUser.Email);
                SceneManager.LoadScene("LobbyScene");
            }
        });
    }
}
