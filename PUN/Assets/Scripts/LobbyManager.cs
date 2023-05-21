using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    public Text nameTxt;


    void Start()
    {
        try
        {
            if (AuthManager.firebaseUser != null)
                nameTxt.text = AuthManager.userId;
        }catch( UnityException e)
        {
            Debug.Log(e.ToString());
        }
    }   
}
