/**
 * Manage Result
 * 
 * @version 0.1, First version
 * @author S3
 * @date 2024/01/27
 */

using UnityEngine;
using UnityEngine.UI;

public class Result_Script : MonoBehaviour
{
    public GameObject Result_Panel;
    private Text Result_Text;

    private Point_Script point_Script;
    private Continue_Script continue_Script;
    private SceneDirector_Script sd_Script;

    // Specifies
    private void Awake()
    {
        Result_Panel = GameObject.Find("Result_Panel");
        Result_Text = GameObject.Find("Result_Text").GetComponent<Text>();
    }

    private void Start()
    {
        point_Script = GetComponent<Point_Script>();
        continue_Script = GetComponent<Continue_Script>();
        sd_Script = GameObject.Find("Scene_Director").GetComponent<SceneDirector_Script>();

        Result_Panel.GetComponent<Button>().onClick.AddListener(CloseResult);
    }

    // Init
    public void Init()
    {
        Result_Panel.SetActive(false);
    }

    /*
     * Game Result
     * 
     * @param int resultNumber
     */
    public void ResultTurn(int resultNumber)
    {
        /*
         * - Show game result
         * - Settle point
         */
        if (resultNumber == 0)
        {
            Result_Text.text = "Your Defeat...";
        }
        else if (resultNumber == 1)
        {
            Result_Text.text = "Your Victory!";
            point_Script.PlusMyPoint(2);
        }
        else if (resultNumber == 2)
        {
            Result_Text.text = "Push";
            point_Script.PlusMyPoint(1);
        }
        else if (resultNumber == 3)
        {
            Result_Text.text = "Burst...";
        }
        else if (resultNumber == 4)
        {
            Result_Text.text = "Black Jack!";
            point_Script.PlusMyPoint(2.5f);
        }
        else if(resultNumber == 5)
        {
            Result_Text.text = "Even Money!";
            point_Script.PlusMyPoint(2);
        }
        else if(resultNumber == 6)
        {
            Result_Text.text = "Insurance";
            point_Script.InitInsurancePoint();
            point_Script.PlusMyPoint(1);
        }
        else if (resultNumber == 7)
        {
            Result_Text.text = "Insurance Fail...";
            point_Script.InitInsurancePoint();
        }
        Result_Panel.SetActive(true);
    }

    // Close result
    public void CloseResult()
    {
        if (point_Script.GetMyPoint() != 0)
        {
            Result_Panel.SetActive(false);
            continue_Script.ContinueQuestion_Panel.SetActive(true);
        }
        else
        {
            sd_Script.MainOn();
        }
    }
}