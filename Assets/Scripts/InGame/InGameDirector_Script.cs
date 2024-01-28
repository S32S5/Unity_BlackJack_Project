/**
 * Manage InGame flow
 * 
 * @version 0.1, First version
 * @author S3
 * @date 2024/01/27
 */

using System.Collections;
using UnityEngine;

public class InGameDirector_Script : MonoBehaviour
{
    private Point_Script point_Script;
    private DeckController_Script dc_Script;
    private BettingPoint_Script bp_Script;
    private EvenMoneyInsurance_Script emi_Script;
    private PlayerAction_Script pa_Script;
    private Result_Script result_Script;
    private Continue_Script continue_Script;
    private RegisterRanker_Script rr_Script;

    // Specifies
    private void Awake()
    {
        gameObject.AddComponent<Point_Script>();
        gameObject.AddComponent<DeckController_Script>();
        gameObject.AddComponent<BettingPoint_Script>();
        gameObject.AddComponent<EvenMoneyInsurance_Script>();
        gameObject.AddComponent<PlayerAction_Script>();
        gameObject.AddComponent<Result_Script>();
        gameObject.AddComponent<Continue_Script>();
        gameObject.AddComponent<RegisterRanker_Script>();

        point_Script = GetComponent<Point_Script>();
        dc_Script = GetComponent<DeckController_Script>();
        bp_Script = GetComponent<BettingPoint_Script>();
        emi_Script = GetComponent<EvenMoneyInsurance_Script>();
        pa_Script = GetComponent<PlayerAction_Script>();
        result_Script = GetComponent<Result_Script>();
        continue_Script = GetComponent<Continue_Script>();
        rr_Script = GetComponent<RegisterRanker_Script>();
    }

    // New Gambler
    public void NewGambler()
    {
        point_Script.InitMyPoint();
        InitGame();
    }
    
    // Init Game
    public void InitGame()
    {
        point_Script.InitBettedPoint();
        point_Script.InitInsurancePoint();
        bp_Script.Init();
        dc_Script.Init();
        emi_Script.Init();
        pa_Script.Init();
        result_Script.Init();
        continue_Script.Init();
        rr_Script.Init();
    }

    // Player turn action
    public IEnumerator PlayerTurn()
    {
        yield return StartCoroutine(dc_Script.DrawCard(false, 1));
        yield return new WaitForSeconds(0.3f);
        yield return StartCoroutine(dc_Script.DrawCard(true, 0));
        yield return new WaitForSeconds(0.3f);
        yield return StartCoroutine(dc_Script.DrawCard(false, 1));
        yield return new WaitForSeconds(0.3f);
        yield return StartCoroutine(dc_Script.DrawCard(false, 0));
        yield return new WaitForSeconds(0.3f);

        int condition = dc_Script.checkSettedCard();
        if (condition == 1)
            StartCoroutine(dealerTurn());
        else if (condition == 2)
            emi_Script.EvenMoneyQuestion_Panel.SetActive(true);
        else if (condition == 3)
            emi_Script.InsuranceQuestion_Panel.SetActive(true);
        else
            pa_Script.SetPlayerAction(true);
    }

    // Dealer turn action
    public IEnumerator dealerTurn()
    {
        yield return new WaitForSeconds(0.3f);
        dc_Script.OpenHidingCard();
        yield return new WaitForSeconds(0.75f);

        if (dc_Script.decks[1].Count == 2 && dc_Script.GetScore(1) == 21)
        {
            if (dc_Script.GetScore(0) == 21)
                result_Script.ResultTurn(2);
            else
                result_Script.ResultTurn(4);
        }
        else if (dc_Script.decks[0].Count == 2 && dc_Script.GetScore(0) == 21)
            result_Script.ResultTurn(0);
        else
        {
            IEnumerator dealerAction()
            {
                while (true)
                {
                    if (dc_Script.GetScore(0) < 17)
                    {
                        yield return new WaitForSeconds(0.3f);
                        yield return StartCoroutine(dc_Script.DrawCard(false, 0));
                        yield return new WaitForSeconds(0.3f);
                    }
                    else
                    {
                        if (dc_Script.GetScore(0) > 21)
                            result_Script.ResultTurn(1);
                        else if (dc_Script.GetScore(0) > dc_Script.GetScore(1))
                            result_Script.ResultTurn(0);
                        else if (dc_Script.GetScore(0) < dc_Script.GetScore(1))
                            result_Script.ResultTurn(1);
                        else if (dc_Script.GetScore(0) == dc_Script.GetScore(1))
                            result_Script.ResultTurn(2);

                        break;
                    }
                }
            }
            StartCoroutine(dealerAction());
        }
    }
}