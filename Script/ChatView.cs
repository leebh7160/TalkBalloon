using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChatView : MonoBehaviour
{
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

    private List<string> chat_loadList;
    private List<string> chat_floatingList;

    private void Start()
    {
        chat_loadList       = new List<string>();
        chat_floatingList   = new List<string>();
        getChildComponent();
    }

    private void getChildComponent()
    {
        chat_Character_Background   = obj_Character_Background.GetComponent<Image>();
        chat_Name_Background        = obj_Name_Background.GetComponent<Image>();
    }

    #region 대화 데이터 가져오는 부분

    internal void Chat_Initial()
    {
        TM_chat_Character.text = null;
        TM_chat_Name.text = null;
        chat_floatingList.Clear();
    }

    internal void ChatText(NPCName npcname, int datanumber)//무슨 데이터를 불러올 지 확인하는 부분
    {
        TM_chat_Character.text = null;
        //chat_loadList //파일 데이터 불러오기
    }

    private void ChatDataFloating()
    {
        chat_floatingList.Clear();
        //sql도 해보고 txt도 해봐서 저장하기
    }

    internal void Chat_FirstChat()
    {
        //데이터 파일 불러오는 부분
        //TM_chat_Character.text = 
    }

    internal void Chat_NextChat()
    {

    }

    internal void Chat_EndChat()
    {
        StartCoroutine("ChatViewEnd");
    }

    #endregion

    #region 대화창과 대화문 이벤트에 대한 것

    internal IEnumerator ChatViewPlay() //대화창 서서히 나타나는 코루틴
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

    internal IEnumerator ChatViewEnd()
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
