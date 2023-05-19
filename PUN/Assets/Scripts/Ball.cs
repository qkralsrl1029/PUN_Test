using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Ball : MonoBehaviourPun
{
    //������ ���� ���� �����ϰ�, ���� ������ �����̵���
    //�Ϲ� Ŭ���̾�Ʈ�� ������ ������ ���� ����並 ���� �޾ƿ� �� ����ȭ�� ����
    public bool isMasterClientLocal => PhotonNetwork.IsMasterClient && photonView.IsMine;

    private Vector2 dir = Vector2.right;
    private readonly float speed = 10f;
    private readonly float randomRefectionIntensity = 0.1f; //�Ի簢�� �ݻ簢�� ���� ��ȭ

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
