/**
 * Manage related to point
 * 
 * @version 0.1, First version
 * @author S3
 * @date 2024/01/27
 */

using UnityEngine;
using UnityEngine.UI;

public class Point_Script : MonoBehaviour
{
    public Text MyPoint_Text, BettedPoint_Text, InsurancePoint_Text;

    private ulong bettedPoint, myPoint, insurancePoint;

    // Specifies
    private void Awake()
    {
        MyPoint_Text = GameObject.Find("MyPoint_Text").GetComponent<Text>();
        BettedPoint_Text = GameObject.Find("BettedPoint_Text").GetComponent<Text>();
        InsurancePoint_Text = GameObject.Find("InsurancePoint_Text").GetComponent<Text>();
    }

    // Init my point
    public void InitMyPoint()
    {
        myPoint = 1000;
        UpdateMyPoint_Text();
    }

    // Init betted point
    public void InitBettedPoint()
    {
        bettedPoint = 0;
        UpdateBettedPoint_Text();
    }

    // Init insurance point
    public void InitInsurancePoint()
    {
        insurancePoint = 0;
        UpdateInsurancePoint_Text();
    }

    // Update MyPoint_Text
    public void UpdateMyPoint_Text()
    {
        MyPoint_Text.text = "My Point: " + myPoint;
    }

    // Update BettedPoint_Text
    public void UpdateBettedPoint_Text()
    {
        if (bettedPoint == 0)
            BettedPoint_Text.text = "";
        else
            BettedPoint_Text.text = "Betted Point: " + bettedPoint;
    }

    // Update InsurancePoint_Text
    public void UpdateInsurancePoint_Text()
    {
        if (insurancePoint == 0)
            InsurancePoint_Text.text = "";
        else
            InsurancePoint_Text.text = "Insurance Point: " + insurancePoint;
    }

    // Plus betted point
    public void PlusBettedPoint(ulong bettingPoint)
    {
        bettedPoint += bettingPoint;
        myPoint -= bettingPoint;

        UpdateBettedPoint_Text();
        UpdateMyPoint_Text();
    }

    // Plus my point
    public void PlusMyPoint(float x)
    {
        myPoint += (ulong)Mathf.FloorToInt(bettedPoint * x);
        bettedPoint = 0;
        UpdateBettedPoint_Text();
        UpdateMyPoint_Text();
    }

    // Set insurance point
    public void SetInsurancePoint()
    {
        insurancePoint = bettedPoint / 2;
    }

    /*
     * Get bettedPoint
     * 
     * @return ulong bettedPoint
     */
    public ulong GetBettedPoint()
    {
        return bettedPoint;
    }

    /*
     * Get myPoint
     * 
     * @return ulong myPoint
     */
    public ulong GetMyPoint()
    {
        return myPoint;
    }
}