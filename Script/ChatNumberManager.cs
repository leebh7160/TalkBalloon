//save 해야하는 데이터
using System.Collections;
using System.IO;

public enum NPCName
{
    None = -1, LeftMan = 0, RightMan = 1
}

public class ChatNumberManager
{
    private int Talk_LeftMan = 0;
    private int Talk_RightMan = 0;


    internal int GetTalkingData(NPCName npcname)
    {
        switch (npcname)
        {
            case NPCName.LeftMan:
                return Talk_LeftMan;
            case NPCName.RightMan:
                return Talk_RightMan;
            default:
                return -1;
        }
    }

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

    //데이터 불러오기
    public IEnumerator LoadTalkingData()
    {
        /*while(true)
        {
            loadDataFile.
        }*/

        yield return null;
    }

}
