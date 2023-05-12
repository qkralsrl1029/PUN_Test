using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    public Text nameTxt;


    void Start()
    {
        if (AuthManager.firebaseUser != null)
            nameTxt.text = AuthManager.userId;

        Debug.Log(AuthManager.firebaseUser.Email);
    }

    
}
