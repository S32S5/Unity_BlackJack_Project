/**
 * EvenMoney and Insurance script
 * 
 * Script description
 * - EvenMoney action
 * - Insurance action
 * 
 * @version 0.1, First version
 * @author S3
 * @date 2024/01/27
 */

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EvenMoneyInsurance_Script : MonoBehaviour
{
    public GameObject EvenMoneyQuestion_Panel;
    private Button YesEvenMoney_Button, NoEvenMoney_Button;

    public GameObject InsuranceQuestion_Panel;
    private Button YesInsurance_Button, NoInsurance_Button;

    private Point_Script point_Script;
    private InGameDirector_Script igd_Script;
    private DeckController_Script dc_Script;
    private Result_Script result_Script;
    private PlayerAction_Script pa_Script;

    // Specifes
    private void Awake()
    {
        EvenMoneyQuestion_Panel = GameObject.Find("EvenMoneyQuestion_Panel");
        YesEvenMoney_Button = GameObject.Find("YesEvenMoney_Button").GetComponent<Button>();
        NoEvenMoney_Button = GameObject.Find("NoEvenMoney_Button").GetComponent<Button>();

        InsuranceQuestion_Panel = GameObject.Find("InsuranceQuestion_Panel");
        YesInsurance_Button = GameObject.Find("YesInsurance_Button").GetComponent<Button>();
        NoInsurance_Button = GameObject.Find("NoInsurance_Button").GetComponent<Button>();

        point_Script = GetComponent<Point_Script>();
        dc_Script = GetComponent<DeckController_Script>();
    }

    // Specifes when game start
    private void Start()
    {
        igd_Script = GetComponent<InGameDirector_Script>();
        result_Script = GetComponent<Result_Script>();
        pa_Script = GetComponent<PlayerAction_Script>();

        YesEvenMoney_Button.onClick.AddListener(YesEvenMoney_Button_OnClick);
        NoEvenMoney_Button.onClick.AddListener(NoEvenMoney_Button_OnClick);

        YesInsurance_Button.onClick.AddListener(YesInsurance_Button_OnClick);
        NoInsurance_Button.onClick.AddListener(NoInsurance_Button_OnClick);
    }

    // Init
    public void Init()
    {
        EvenMoneyQuestion_Panel.SetActive(false);
        InsuranceQuestion_Panel.SetActive(false);
    }

    // Yes EvenMoney action
    private void YesEvenMoney_Button_OnClick()
    {
        EvenMoneyQuestion_Panel.SetActive(false);
        result_Script.ResultTurn(5);
    }

    // No EvenMoney action
    private void NoEvenMoney_Button_OnClick()
    {
        EvenMoneyQuestion_Panel.SetActive(false);
        StartCoroutine(igd_Script.dealerTurn());
    }

    // Yes Insurance action
    private void YesInsurance_Button_OnClick()
    {
        InsuranceQuestion_Panel.SetActive(false);
        point_Script.SetInsurancePoint();
        point_Script.UpdateInsurancePoint_Text();

        IEnumerator CheckDealerCard()
        {
            yield return new WaitForSeconds(0.75f);
            dc_Script.OpenHidingCard();

            if (dc_Script.GetScore(0) == 21)
                result_Script.ResultTurn(6);
            else
                result_Script.ResultTurn(7);
        }
        StartCoroutine(CheckDealerCard());
    }

    // No Insurance action
    private void NoInsurance_Button_OnClick()
    {
        InsuranceQuestion_Panel.SetActive(false);
        pa_Script.SetPlayerAction(true);
    }
}
