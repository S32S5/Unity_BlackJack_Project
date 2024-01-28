/**
 * Manage betting point
 * 
 * @version 0.1, First version
 * @author S3
 * @date 2024/01/26
 */

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BettingPoint_Script : MonoBehaviour
{
    private GameObject Betting_Panel;
    private InputField BettingPoint_InputField;
    private Button Betting_Button;
    private GameObject PETA_Text;

    private Point_Script point_Script;
    private InGameDirector_Script igd_Script;

    // Specifies
    private void Awake()
    {
        Betting_Panel = GameObject.Find("Betting_Panel");
        BettingPoint_InputField = GameObject.Find("BettingPoint_InputField").GetComponent<InputField>();
        Betting_Button = GameObject.Find("Betting_Button").GetComponent<Button>();
        PETA_Text = GameObject.Find("PETA_Text");
    }

    // Specifies when game start
    private void Start()
    {
        point_Script = GetComponent<Point_Script>();
        igd_Script = GetComponent<InGameDirector_Script>();

        BettingPoint_InputField.onValueChanged.AddListener(delegate { LimitBetting_InputField(); });
        Betting_Button.onClick.AddListener(Betting_Button_OnClick);
    }

    // Init
    public void Init()
    {
        BettingPoint_InputField.text = "";
        PETA_Text.SetActive(false);
        Betting_Panel.SetActive(true);
    }

    // Limit betting inputfield is "-", "0", over myPoint
    private void LimitBetting_InputField()
    {
        if (BettingPoint_InputField.text.Equals("-") || BettingPoint_InputField.text.Equals("0"))
            BettingPoint_InputField.text = "";
        else if (!BettingPoint_InputField.text.Equals("") && ulong.Parse(BettingPoint_InputField.text) > point_Script.GetMyPoint())
            BettingPoint_InputField.text = point_Script.GetMyPoint().ToString();
    }

    // Betting_Button onclick action
    private void Betting_Button_OnClick()
    {
        if (BettingPoint_InputField.text == "")
        {
            IEnumerator ShowPETA_Text()
            {
                PETA_Text.SetActive(true);

                yield return new WaitForSeconds(1.75f);
                PETA_Text.SetActive(false);
            }
            StartCoroutine(ShowPETA_Text());
        }
        else
        {
            Betting_Panel.SetActive(false);

            point_Script.PlusBettedPoint(ulong.Parse(BettingPoint_InputField.text));

            StartCoroutine(igd_Script.PlayerTurn());
        }
    }
}