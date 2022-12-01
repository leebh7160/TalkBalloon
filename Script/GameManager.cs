using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
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

    private void GamePause(bool pause)
    {
        isGamePause = pause;
    }


    #region NPC 이름 찾기

    internal void PlayerNPCChat(string tagdata)
    {
        Dictionary<int, int> instantPlayData = new Dictionary<int, int>();

        GamePause(true);

        NPCName npcname;
        npcname         = chatNumberManager.GetNPCNameData(tagdata);
        npctalknumber   = chatNumberManager.GetTalkingData(npcname);
        instantPlayData.Add(npctalknumber, (int)npcname);
        Debug.Log(npctalknumber);

        chatView.PlayData_Send(instantPlayData);
        chatView.Chat_FadeIn();
    }

    internal void PlayerNPCChatEnd()
    {
        GamePause(false);
        StartCoroutine(chatView.Co_ChatViewFadeOut());
    }
    #endregion

    #region NPC 대화 저장
    internal void NPCChatSave(string npcname_str, int talkcount)
    {
        NPCName npcname;
        npcname = chatNumberManager.GetNPCNameData(npcname_str);

        chatNumberManager.SetTalkingData(npcname, talkcount);
    }
    #endregion
}
