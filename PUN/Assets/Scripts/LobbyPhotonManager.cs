using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine;

public class LobbyPhotonManager : MonoBehaviourPunCallbacks
{

    private readonly string gameVersion = "1"; //게임 버전에 따라 매칭 가능 여부 판단

    public Text connectionInfo;
    public Button joinButton;


    private void Start()
    {
        // 로비 진입 동시에 마스터 서버에 접속
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.ConnectUsingSettings();

        joinButton.interactable = false;
        connectionInfo.text = "Connecting to Master Server...";
        Debug.Log("Connecting to Master Server...");


    }


    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        joinButton.interactable = true;
        connectionInfo.text = "Success to Connect!";
        Debug.Log("Success to Connect!");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        connectionInfo.text = "Disconnected! "+cause.ToString();
        Debug.Log("Disconnected! " + cause.ToString());

        PhotonNetwork.ConnectUsingSettings();
    }


    public void Connect()
    {
        //접속 버튼 클릭시 룸 접속 시도
        if (!PhotonNetwork.IsConnected)
            return;

        connectionInfo.text = "Connecting to Random room...";
        Debug.Log("Connecting to Random room...");
        PhotonNetwork.JoinRandomRoom();

    }


    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        // 룸 참가에 실패했을 경우/(빈 방 없음,...)
        base.OnJoinRandomFailed(returnCode, message);
        connectionInfo.text = "There is no empty room, creating new room";
        Debug.Log("There is no empty room, creating new room");

        PhotonNetwork.CreateRoom("test", new RoomOptions { MaxPlayers = 2 });
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        connectionInfo.text = "Connected to Room Success!";
        Debug.Log("Connected to Room Success!");
        PhotonNetwork.LoadLevel("Main");
    }
}
