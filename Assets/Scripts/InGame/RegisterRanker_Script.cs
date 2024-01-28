/**
 * Manage register ranker
 * 
 * @version 0.1, First version
 * @author S3
 * @date 2024/01/27
 */

using UnityEngine;
using UnityEngine.UI;

public class RegisterRanker_Script : MonoBehaviour
{
    public GameObject RegisterRanker_Panel;
    private InputField InputNickname_InputField;
    private Button RegisterRanker_Button;

    private SceneDirector_Script sd_Script;
    private FileController_Script fc_Script;
    private Point_Script point_Script;

    private void Awake()
    {
        RegisterRanker_Panel = GameObject.Find("RegisterRanker_Panel");
        InputNickname_InputField = GameObject.Find("InputNickname_InputField").GetComponent<InputField>();
        RegisterRanker_Button = GameObject.Find("RegisterRanker_Button").GetComponent<Button>();
    }

    private void Start()
    {
        sd_Script = GameObject.Find("Scene_Director").GetComponent<SceneDirector_Script>();
        fc_Script = GameObject.Find("Scene_Director").GetComponent<FileController_Script>();
        point_Script = GetComponent<Point_Script>();

        RegisterRanker_Button.onClick.AddListener(RegisterRanker_Button_OnClick);
    }

    public void Init()
    {
        RegisterRanker_Panel.SetActive(false);
    }

    public void RankChecker()
    {
        if (fc_Script.GetRank(point_Script.GetMyPoint()) != 0)
            RegisterRanker_Panel.SetActive(true);
        else
            sd_Script.MainOn();
    }

    private void RegisterRanker_Button_OnClick()
    {
        fc_Script.RegisterNewRanker(fc_Script.GetRank(point_Script.GetMyPoint()), InputNickname_InputField.text, point_Script.GetMyPoint());

        sd_Script.MainOn();
    }
}
