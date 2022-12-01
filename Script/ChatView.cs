using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChatView : MonoBehaviour, IPointerClickHandler
{
    private CSVData csvData;
    private GameManager gamemanager;


    private Image chat_Character_Background;
    private Image chat_Name_Background;

    [SerializeField]
    private GameObject obj_Character_Background;
    [SerializeField]
    private GameObject obj_Name_Background;
    [SerializeField]
    private TextMeshProUGUI TM_chat_Character;
    [SerializeField]
    private TextMeshProUGUI TM_chat_Name;

    private Dictionary<int, int> DIC_PlayData = new Dictionary<int, int>();
    private Dictionary<int, string> DIC_ChatData = new Dictionary<int, string>();

    private int talkUI_TalkCount = 0;

    private bool talkUI_Active = false;


    private void Start()
    {
        csvData = new CSVData();
        gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();
        csvData.CSVDataLoadStart();
        getChildComponent();
    }

    private void getChildComponent()
    {
        chat_Character_Background   = obj_Character_Background.GetComponent<Image>();
        chat_Name_Background        = obj_Name_Background.GetComponent<Image>();
    }

    internal void Chat_Initial()
    {
        TM_chat_Character.text = null;
        TM_chat_Name.text = null;
        DIC_PlayData.Clear();
        DIC_ChatData.Clear();
    }

    #region ��ȭ ������ UI�� ǥ��
    private void ChatUI_ChatShow()//��ȭ UI�� ǥ��
    {
        if (DIC_ChatData == null)
            return;

        TM_chat_Name.text = ChatUI_WhoTalk(DIC_ChatData[talkUI_TalkCount]); //���� ���ߴ��� �Ǻ�
        TM_chat_Character.text = DIC_ChatData[talkUI_TalkCount].Split(":")[1];
    }

    private string ChatUI_WhoTalk(string whotalkdata) //��ȭ ���� �ϴ��� Ȯ��
    {
        string tmp = whotalkdata;
        string[] tmpSplit = tmp.Split(":");

        switch(tmpSplit[0])
        {
            case "-1":
                return "Player";
            case "0":
                return "LeftMan";
            case "1":
                return "RightMan";
            default:
                return null;
        }
    }

    public void OnPointerClick(PointerEventData eventData) //��ȭâ Ŭ��(SpaceŰ�� ���� �����ϰ�)
    {
        if(talkUI_Active == true)
            Chat_NextChat();
    }

    //============================��ȭ â �ѱ��==========================
    internal void Chat_NextChat()//���� ��ȭ UI�� �ѱ��
    {
        bool talkcheck = false;
        talkcheck = Chat_StopChat();

        if (talkcheck == false)
            talkUI_TalkCount++;
        else
            gamemanager.NPCChatSave(TM_chat_Name.text, talkUI_TalkCount);

        ChatUI_ChatShow();
    }

    private bool Chat_StopChat()//��ȭ�� �� ���Ǿ��� �� �ȵ��ư��� Ȯ��
    {
        if (DIC_ChatData == null)
            return true;

        if(DIC_ChatData[talkUI_TalkCount].Split(":").Length > 2)
            return true;
        return false;
    }

    //============================��ȭ â �ѱ��==========================^^
    #endregion

    #region �����Ȳ ������ �ְ�, CSV������ �ޱ�
    internal void PlayData_Send(Dictionary<int, int> dic_playdata) //GameManager�κ��� �����Ȳ ������ �ޱ�
    {
        DIC_PlayData.Clear();
        DIC_PlayData = dic_playdata;
        talkUI_TalkCount = dic_playdata.Keys.First() + dic_playdata.Values.First();

        Debug.Log(talkUI_TalkCount);
        CSVData_GetSetPlayData();
    }

    private void CSVData_GetSetPlayData()//CSVData�� ������ ������ �ޱ�
    {
        if (DIC_PlayData == null)
            return;

        DIC_ChatData.Clear();
        DIC_ChatData = csvData.PlayData_GetData(DIC_PlayData); //���߿� ������ ����

        ChatUI_ChatShow();
    }

    private void ChatDataReset()
    {
        DIC_PlayData.Clear();
    }
    #endregion


    #region FadeIn, FadeOut
    internal void Chat_FadeIn()
    {
        talkUI_Active = true;
        StartCoroutine(Co_ChatViewFadeIn());
    }

    internal void Chat_FadeOut()//��ȭâ ���̵�ƿ� �ڷ�ƾ
    {
        talkUI_Active = false;
        StartCoroutine(Co_ChatViewFadeOut());
    }

    internal IEnumerator Co_ChatViewFadeIn() //��ȭâ ������ ��Ÿ���� �ڷ�ƾ
    {
        float time = 1f;
        float current = 0;
        float percent = 0;
        float character_alphaChange = 0;
        float alphaChange = 0;


        while (percent < 1)
        {
            if (character_alphaChange <= 0.4f)
                character_alphaChange += 0.01f;
            alphaChange           += 0.1f;

            current += Time.deltaTime;
            percent = current / time;

            chat_Character_Background.color     = new Color(1, 1, 1, character_alphaChange);
            chat_Name_Background.color          = new Color(0, 0, 0, alphaChange);
            TM_chat_Character.color             = new Color(1, 1, 1, alphaChange);
            TM_chat_Name.color                  = new Color(1, 1, 1, alphaChange);

            yield return null;
        }

        yield return new WaitForSeconds(1);
    }

    internal IEnumerator Co_ChatViewFadeOut()
    {
        float time = 1f;
        float current = 0;
        float percent = 0;
        float character_alphaChange = 0.4f;
        float alphaChange = 1;

        while (percent < 1)
        {
            character_alphaChange   -= 0.01f;
            alphaChange             -= 0.1f;

            current += Time.deltaTime;
            percent = current / time;

            chat_Character_Background.color     = new Color(1, 1, 1, character_alphaChange);
            chat_Name_Background.color          = new Color(0, 0, 0, alphaChange);
            TM_chat_Character.color             = new Color(1, 1, 1, alphaChange);
            TM_chat_Name.color                  = new Color(1, 1, 1, alphaChange);

            yield return null;
        }

        yield return new WaitForSeconds(1);
    }

    private void ChatViewOnOff(bool onoff)
    {
        obj_Character_Background.SetActive(onoff);
        obj_Name_Background.SetActive(onoff);
        TM_chat_Character.gameObject.SetActive(onoff);
        TM_chat_Name.gameObject.SetActive(onoff);
    }

    internal IEnumerator ChatCharacterPlay()
    {
        yield return null;
    }
    #endregion
}
