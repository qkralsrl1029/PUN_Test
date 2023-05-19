using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player : MonoBehaviourPun
{
    private Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;

    public float speed = 3f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (photonView.IsMine)
            spriteRenderer.color = Color.blue;
        else
            spriteRenderer.color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
            return;

        var input = InputButton.VerticalInput;
        var distance = input * speed * Time.deltaTime;
        var targetPos = transform.position + Vector3.up * distance;

        rigidbody.MovePosition(targetPos);
    }
}
