using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private ChatView chatView;

    private ChatNumberManager chatNumberManager = new ChatNumberManager();

    private int npctalknumber = -1;


    private bool isGamePause = false;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void GamePause(bool pause)
    {
        isGamePause = pause;
    }

    private void OnMouseDown()
    {
        if (isGamePause == false)
            return;

        chatView.Chat_NextChat();
    }


    #region NPC 이름 찾기

    internal void PlayerNPCChat(string tagdata)
    {
        NPCName npcname;
        npcname = chatNumberManager.GetNPCNameData(tagdata);
        npctalknumber = chatNumberManager.GetTalkingData(npcname);
        chatView.ChatText(npcname, npctalknumber);
        chatView.Chat_FirstChat();
        GamePause(true);
        StartCoroutine(chatView.ChatViewPlay());
    }

    internal void PlayerNPCChatEnd()
    {
        GamePause(false);
        StartCoroutine(chatView.ChatViewEnd());
    }
    #endregion
}
