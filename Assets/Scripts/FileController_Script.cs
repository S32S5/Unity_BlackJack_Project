/**
 * Manage the ranking file
 * 
 * Script description
 * - Encoding
 * - Decoding
 * - Return rankers, calculate rank
 * - Register new ranker
 * 
 * @version 0.1, First version
 * @author S3
 * @date 2024/01/26
 */

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class FileController_Script : MonoBehaviour
{
    private RankerDataList rds;
    private BinaryFormatter bf;

    private static string rankersDataPath;

    // If no ranking file in rankerDataPath, add it.
    private void Awake()
    {
        rankersDataPath = Path.Combine(Application.persistentDataPath + "/RankerData.json");
        bf = new BinaryFormatter();

        if (!File.Exists(rankersDataPath))
        {
            /*
             * - Create new RankerDataList
             * - Convert RankerDataList to string and make json file
             */

            // Create new rds
            rds = new RankerDataList();
            for (int i = 0; i < 10; i++)
            {
                RankerData rd = new RankerData();
                rds.Rankers[i] = rd;
            }

            // Convert rds and create a json file
            File.WriteAllText(rankersDataPath, encodeRds());
        }
        else
            rds = GetRankers();
    }

    /*
     * Get string what encoded RankerDataList
     * 
     * @return string encodedRds
     */
    private string encodeRds()
    {
        var ms = new MemoryStream();
        bf.Serialize(ms, rds);
        string encodedRds = Convert.ToBase64String(ms.ToArray());
        return encodedRds;
    }

    /*
     * Get rankers data
     * 
     * @return RankerDataList decodedRds
     */
    public RankerDataList GetRankers()
    {
        RankerDataList decodedRds()
        {
            string json = File.ReadAllText(rankersDataPath);
            byte[] bytes = Convert.FromBase64String(json);

            var ms = new MemoryStream();
            ms.Write(bytes, 0, bytes.Length);
            ms.Seek(0, SeekOrigin.Begin);
            var rds = bf.Deserialize(ms);
            return (RankerDataList)rds;
        }
        return decodedRds();
    }

    /*
     * Calculate rank
     * 
     * @return int rank or return 0, if rank over 10
     */
    public int GetRank(ulong point)
    {
        for (int i = 0; i < 10; i++)
        {
            if (point > rds.Rankers[i].point)
                return i + 1;
        }

        return 0;
    }

    /*
     * Register new ranker
     * 
     * @param int rank, string nickname, ulong myPoint
     */
    public void RegisterNewRanker(int rank, string nickname, ulong myPoint)
    {
        // Lower ranking with a lower rank
        for (int i = 9; i > rank - 1; i--)
            rds.Rankers[i] = rds.Rankers[i - 1];

        rds.Rankers[rank - 1] = new RankerData();
        rds.Rankers[rank - 1].nickname = nickname;
        rds.Rankers[rank - 1].point = myPoint;

        // encoding
        File.WriteAllText(rankersDataPath, encodeRds());
    }
}