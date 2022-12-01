//save 해야하는 데이터
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro.EditorUtilities;
using Unity.VisualScripting;

public enum NPCName
{
    None = -1, LeftMan = 1000, RightMan = 2000
}

public class ChatNumberManager
{
    private List<int> TalkCountData = new List<int>();

    public ChatNumberManager()
    {
        for (int i = 0; i < Enum.GetValues(typeof(NPCName)).Length - 1; i++)
        {
            TalkCountData.Add(0);
        }  
    }

    #region 대화 GetSet

    internal int GetTalkingData(NPCName npcname) //저장된 데이터 가져오기
    {
        int count = Talk_ListLocation(npcname);
        int returndata = TalkCountData[count];

        return returndata;
    }

    internal void SetTalkingData(NPCName npcname, int talkcount)//대화 얼마나 했는지 저장
    {
        int temp = Talk_ListLocation(npcname);

        TalkCountData[temp - 1] = Talk_ListCount(talkcount);
    }

    private void WhoTalkThis(NPCName npcname, int talkcount)
    {
        //이걸 NPC마다 가지고있어하는구만
        //그래서 그 NPC의 데이터를 총합해서 저장하는 곳을 만들고
        //그걸로 움직였어야 하는데 이거 잘못했네
        //그래서 플레이 했을 때 NPC한테 대화를 로드해주는 것부터 시작해서
        //그 후에 넘어갔어야 했네
    }

    private int Talk_ListLocation(NPCName npcname) //enum에 들어간 NPC코드이름 뒤에서부터 4번째 숫자 가져오기
    {
        int npcnumber = (int)npcname;
        string talkData = npcnumber.ToString();
        talkData = talkData.Substring(talkData.Length - 4,1);

        int returndata = int.Parse(talkData);

        return returndata;
    }

    private int Talk_ListCount(int talkcount)
    {
        int npcnumber = talkcount;
        string talkData = npcnumber.ToString();
        talkData = talkData.Substring(talkData.Length - 3);

        int returndata = int.Parse(talkData);

        return returndata;
    }
    #endregion

    internal NPCName GetNPCNameData(string npcstring)
    {
        return SearchNPCName(npcstring);
    }

    private NPCName SearchNPCName(string npcstring)
    {
        NPCName npcname;

        switch (npcstring)
        {
            case "LeftMan":
                npcname = NPCName.LeftMan;
                break;
            case "RightMan":
                npcname = NPCName.RightMan;
                break;
            default:
                npcname = NPCName.LeftMan;
                break;
        }

        return npcname;
    }

    
    //게임 진행상황 데이터 불러오기
    public IEnumerator LoadTalkingData()
    {
        /*while(true)
        {
            loadDataFile.
        }*/

        yield return null;
    }

}
