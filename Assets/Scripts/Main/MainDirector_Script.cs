/**
 * Manage related to Main_Canvas
 * 
 * Script description
 * - Manage ranking system
 * - Manage the GameStart_Button action
 * - Quit game
 * 
 * @version 0.1, First version
 * @author S3
 * @date 2024/01/26
 */

using UnityEngine;
using UnityEngine.UI;

public class MainDirector_Script : MonoBehaviour
{
    private GameObject Rank_Panel_Prefab;
    private GameObject[] rankPanels = new GameObject[10];
    private static Color32[] rankingTextColor = new Color32[10] {Color.red, new Color32(255, 25, 200, 255), new Color32(255, 94, 0, 255),
        Color.yellow, Color.green, Color.cyan,
        Color.blue, new Color32(255, 0, 127, 255), new Color32(255, 0, 255, 255),
        new Color32(241, 95, 95, 255)};

    private GameObject QuitQuestionBackground_Panel;

    private SceneDirector_Script sd_Script;
    private FileController_Script fc_Script;

    // Specifies
    private void Awake()
    {
        Rank_Panel_Prefab = Resources.Load("Prefabs/Rank_Panel_Prefab") as GameObject;
        for (int i = 0; i < 10; i++)
        {
            rankPanels[i] = Instantiate(Rank_Panel_Prefab, GameObject.Find("RankingPanels_Panel").transform);
            rankPanels[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -24.5f - (49 * i));
            rankPanels[i].transform.Find("Rank_Text").GetComponent<Text>().color = rankingTextColor[i];
            rankPanels[i].transform.Find("Nickname_Text").GetComponent<Text>().color = rankingTextColor[i];
            rankPanels[i].transform.Find("Point_Text").GetComponent<Text>().color = rankingTextColor[i];
        }

        QuitQuestionBackground_Panel = GameObject.Find("QuitQuestionBackground_Panel");
        QuitQuestionBackground_Panel.SetActive(false);
    }

    // Specifies when game start
    private void Start()
    {
        sd_Script = GameObject.Find("Scene_Director").GetComponent<SceneDirector_Script>();
        fc_Script = GameObject.Find("Scene_Director").GetComponent<FileController_Script>();
    }

    // Quit game question short cut key
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitQuestionBackground_Panel.SetActive(!QuitQuestionBackground_Panel.activeSelf);
        }
    }

    // Init ranker text
    public void Init()
    {
        RankerDataList rds = fc_Script.GetRankers();
        for (int i = 0; i < 10; i++)
        {
            rankPanels[i].transform.Find("Rank_Text").GetComponent<Text>().text = (i + 1).ToString();
            rankPanels[i].transform.Find("Nickname_Text").GetComponent<Text>().text = rds.Rankers[i].nickname;
            rankPanels[i].transform.Find("Point_Text").GetComponent<Text>().text = rds.Rankers[i].point.ToString();
        }
    }

    // GameStart_Button action
    public void GameStart_Button_OnClick()
    {
        sd_Script.InGameOn();
    }

    // Quit game
    public void QuitGame()
    {
        Application.Quit();
    }

    // No quit game
    public void NoQuitGame()
    {
        QuitQuestionBackground_Panel.SetActive(false);
    }
}