using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager Instance;

    public Text score;
    public Transform[] spawnPositions;
    public GameObject playerPrefab;
    public GameObject ballPrefab;

    private int[] playerScores;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        playerScores = new int[2] { 0, 0 };
        SpawnPlayer();  //로컬과 리모트에서 모두 실행

        if (PhotonNetwork.IsMasterClient)
        {
            SpawnBall();    //방장만이 볼을 생성
        }
    }

    private void SpawnPlayer()
    {
        int localPlayerIndex = PhotonNetwork.LocalPlayer.ActorNumber - 1;
        Transform spawnPos = spawnPositions[localPlayerIndex % 2];

        PhotonNetwork.Instantiate(playerPrefab.name, spawnPos.position, spawnPos.rotation);
    }

    private void SpawnBall()
    {
        PhotonNetwork.Instantiate(ballPrefab.name, Vector3.zero, Quaternion.identity);
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        SceneManager.LoadScene("LobbyScene");
    }

    public void AddScore(int player, int score)
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        playerScores[player - 1] += score;
        photonView.RPC("RPCTextUpdate", RpcTarget.All, playerScores[0].ToString(), playerScores[1].ToString());
    }

    [PunRPC]
    private void RPCTextUpdate(string player1,string player2)
    {
        score.text = player1 + " : " + player2;
    }
}
