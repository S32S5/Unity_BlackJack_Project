/**
 * Manage Continue
 * 
 * @version 0.1, First version
 * @author S3
 * @date 2024/01/27
 */

using UnityEngine;
using UnityEngine.UI;

public class Continue_Script : MonoBehaviour
{
    public GameObject ContinueQuestion_Panel;
    private Button YesContinue_Button, NoContinue_Button;

    private InGameDirector_Script igd_Script;
    private RegisterRanker_Script rr_Script;

    private void Awake()
    {
        ContinueQuestion_Panel = GameObject.Find("ContinueQuestion_Panel");
        YesContinue_Button = GameObject.Find("YesContinue_Button").GetComponent<Button>();
        NoContinue_Button = GameObject.Find("NoContinue_Button").GetComponent<Button>();
    }

    private void Start()
    {
        igd_Script = GetComponent<InGameDirector_Script>();
        rr_Script = GetComponent<RegisterRanker_Script>();

        YesContinue_Button.onClick.AddListener(YesContinue_Button_OnClick);
        NoContinue_Button.onClick.AddListener(NoContinue_Button_OnClick);
    }

    public void Init()
    {
        ContinueQuestion_Panel.SetActive(false);
    }

    // Continue game action
    private void YesContinue_Button_OnClick()
    {
        igd_Script.InitGame();

        ContinueQuestion_Panel.SetActive(false);
    }

    // Stop game action
    private void NoContinue_Button_OnClick()
    {
        ContinueQuestion_Panel.SetActive(false);
        rr_Script.RankChecker();
    }
}