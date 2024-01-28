using System;
using System.Collections.Generic;

[Serializable]
public class RankerDataList
{
    public List<RankerData> Rankers = new List<RankerData>(new RankerData[10]);
}

[Serializable]
public class RankerData
{
    public string nickname;
    public ulong point;

    public RankerData()
    {
        nickname = "Not Registered";
        point = 0;
    }
}