/**
 * Manage player action
 * 
 * @version 0.1, First version
 * @author S3
 * @date 2024/01/27
 */

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAction_Script : MonoBehaviour
{
    private GameObject PlayerActionButton_Panel, CardHit_Button, DoubleDown_Button, CardStay_Button;

    private InGameDirector_Script igd_Script;
    private Point_Script point_Script;
    private DeckController_Script dc_Script;
    private Result_Script result_Script;

    // Specifies
    private void Awake()
    {
        PlayerActionButton_Panel = GameObject.Find("PlayerActionButton_Panel");
        CardHit_Button = GameObject.Find("CardHit_Button");
        DoubleDown_Button = GameObject.Find("DoubleDown_Button");
        CardStay_Button = GameObject.Find("CardStay_Button");
    }

    // Specifies when game start
    private void Start()
    {
        igd_Script = GetComponent<InGameDirector_Script>();
        point_Script = GetComponent<Point_Script>();
        dc_Script = GetComponent<DeckController_Script>();
        result_Script = GetComponent<Result_Script>();

        CardHit_Button.GetComponent<Button>().onClick.AddListener(CardHit_Button_OnClick);
        DoubleDown_Button.GetComponent<Button>().onClick.AddListener(DoubleDown_Button_OnClick);
        CardStay_Button.GetComponent<Button>().onClick.AddListener(CardStay_Button_OnClick);
    }

    // Init
    public void Init()
    {
        PlayerActionButton_Panel.SetActive(false);
    }

    /* 
     * Set player action button are active
     * 
     * @param bool active
     */
    public void SetPlayerAction(bool active)
    {
        if (dc_Script.decks[1].Count == 2 && point_Script.GetBettedPoint() <= point_Script.GetMyPoint())
            DoubleDown_Button.SetActive(active);
        else if (!active)
            DoubleDown_Button.SetActive(false);

        PlayerActionButton_Panel.SetActive(active);
    }

    /*
     * CardHit_Button action
     */
    private void CardHit_Button_OnClick()
    {
        SetPlayerAction(false);

        IEnumerator CardHit()
        {
            yield return StartCoroutine(dc_Script.DrawCard(false, 1));
            if (dc_Script.GetScore(1) > 21)
                result_Script.ResultTurn(3);
            else if (dc_Script.GetScore(1) == 21)
                StartCoroutine(igd_Script.dealerTurn());
            else
                SetPlayerAction(true);
        }
        StartCoroutine(CardHit());
    }

    /*
     * DoubleDown action
     */
    private void DoubleDown_Button_OnClick()
    {
        SetPlayerAction(false);
        point_Script.PlusBettedPoint(point_Script.GetBettedPoint());

        IEnumerator DoubleDown()
        {
            yield return StartCoroutine(dc_Script.DrawCard(false, 1));
            if (dc_Script.GetScore(1) > 21)
                result_Script.ResultTurn(3);
            else
                StartCoroutine(igd_Script.dealerTurn());
        }
        StartCoroutine(DoubleDown());
    }

    /*
     * CardStay_Button action
     */
    private void CardStay_Button_OnClick()
    {
        SetPlayerAction(false);
        StartCoroutine(igd_Script.dealerTurn());
    }
}
