using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Character : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    private float Horizontal;
    private float Vertical;
    private float speed = 5;

    private Vector3 position;


    void Start()
    {
    }

    void Update()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        Horizontal = Input.GetAxis("Horizontal");

        position.x += Horizontal * Time.deltaTime * speed;
        this.transform.position = position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "NPC")
            gameManager.PlayerNPCChat(collision.name);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "NPC")
            gameManager.PlayerNPCChatEnd();
    }

}
