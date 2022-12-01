//save �ؾ��ϴ� ������
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

    #region ��ȭ GetSet

    internal int GetTalkingData(NPCName npcname) //����� ������ ��������
    {
        int count = Talk_ListLocation(npcname);
        int returndata = TalkCountData[count];

        return returndata;
    }

    internal void SetTalkingData(NPCName npcname, int talkcount)//��ȭ �󸶳� �ߴ��� ����
    {
        int temp = Talk_ListLocation(npcname);

        TalkCountData[temp - 1] = Talk_ListCount(talkcount);
    }

    private void WhoTalkThis(NPCName npcname, int talkcount)
    {
        //�̰� NPC���� �������־��ϴ±���
        //�׷��� �� NPC�� �����͸� �����ؼ� �����ϴ� ���� �����
        //�װɷ� ��������� �ϴµ� �̰� �߸��߳�
        //�׷��� �÷��� ���� �� NPC���� ��ȭ�� �ε����ִ� �ͺ��� �����ؼ�
        //�� �Ŀ� �Ѿ��� �߳�
    }

    private int Talk_ListLocation(NPCName npcname) //enum�� �� NPC�ڵ��̸� �ڿ������� 4��° ���� ��������
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

    
    //���� �����Ȳ ������ �ҷ�����
    public IEnumerator LoadTalkingData()
    {
        /*while(true)
        {
            loadDataFile.
        }*/

        yield return null;
    }

}
