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
    private bool talkNPC = false;

    void Start()
    {
    }

    void Update()
    {
        PlayerMove();

        if (Input.GetKeyDown(KeyCode.E))
            talkNPC = true;
        else if (Input.GetKeyUp(KeyCode.E))
            talkNPC = false;

    }

    private void PlayerMove()
    {
        Horizontal = Input.GetAxis("Horizontal");

        position.x += Horizontal * Time.deltaTime * speed;
        this.transform.position = position;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "NPC" && talkNPC == true)
        {
            talkNPC = false;
            gameManager.PlayerNPCChat(collision.name);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "NPC")
            gameManager.PlayerNPCChatEnd();
    }

}
