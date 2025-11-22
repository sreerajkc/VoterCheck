
using System;

[Serializable]
public class VoterIdInfo
{
    public int HairId = 1;
    public int NoseId = 1;
    public int BeardId = 1;
    public int MoustacheId = 1;
    public int EyeColorInt = 1;
    public int SkinColorInt = 1;

    public string FirstName;
    public string LastName;
    public string MothersName;

    public int Age = 50 ;
    public int BirthYear = 1999;
    public int BirthMonth = 1;
    public int BirthDay = 19;

    public static bool operator == (VoterIdInfo v1, VoterIdInfo v2)
    {
        return
            v1.HairId == v2.HairId &&
            v1.NoseId == v2.NoseId &&
            v1.BeardId == v2.BeardId &&
            v1.MoustacheId == v2.MoustacheId &&
            v1.EyeColorInt == v2.EyeColorInt &&
            v1.SkinColorInt == v2.SkinColorInt &&
            v1.FirstName == v2.FirstName &&
            v1.LastName == v2.LastName &&
            v1.MothersName == v2.MothersName &&
            v1.Age == v2.Age &&
            v1.BirthYear == v2.BirthYear &&
            v1.BirthMonth == v2.BirthMonth &&
            v1.BirthDay == v2.BirthDay;
    }

    public static bool operator !=(VoterIdInfo v1, VoterIdInfo v2)
    {
        return !(v1 == v2);
    }
}
