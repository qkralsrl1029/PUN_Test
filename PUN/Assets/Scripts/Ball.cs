using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Ball : MonoBehaviourPun
{
    //방장이 먼저 공을 생성하고, 공은 방장이 움직이도록
    //일반 클라이언트는 방장이 움직인 공을 포톤뷰를 통해 받아온 후 동기화만 수행
    public bool isMasterClientLocal => PhotonNetwork.IsMasterClient && photonView.IsMine;

    private Vector2 dir = Vector2.right;
    private readonly float speed = 10f;
    private readonly float randomRefectionIntensity = 0.1f; //입사각과 반사각에 랜덤 변화

    private void FixedUpdate()
    {
        if (!isMasterClientLocal || PhotonNetwork.PlayerList.Length < 2)
            return;

        var disrance = speed * Time.deltaTime;
        var hit = Physics2D.Raycast(transform.position, dir, disrance);

        if(hit.collider!=null)
        {
            var goalPost = hit.collider.GetComponent<Goalpost>();
            if (goalPost != null)
            {
                GameManager.Instance.AddScore(goalPost.playrNum,1);
            }

            dir = Vector2.Reflect(dir, hit.normal);
            dir += Random.insideUnitCircle * randomRefectionIntensity;
        }

        transform.position =(Vector2)transform.position+ dir * disrance;
    }
}
