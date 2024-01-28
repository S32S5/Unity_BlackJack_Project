/**
 * Manages Card
 * 
 * Script description
 * -
 * 
 * @version 0.1, First version
 * @author S3
 * @date 2024/01/17
 */

using UnityEngine;

public class Card_Script : MonoBehaviour
{
    private string cardName;
    private bool isHide;

    private static Vector3 cardStartPos = new Vector3(0, 6, 0);
    private static Vector3 cardVelocity = new Vector3(0, -25, 0);

    // Set cardPosition
    public void Init(string cardName, bool isHide)
    {
        this.cardName = cardName;
        this.isHide = isHide;
        string cardPath = "Images/Sprites/Cards/" + cardName.Substring(0, 3) + "/" + cardName;
        if (isHide)
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/Sprites/Cards/TrumpCardBack");
        else
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(cardPath);

        transform.position = cardStartPos;
        GetComponent<Rigidbody2D>().velocity = cardVelocity;
    }

    public int GetCardNumber()
    {
        return int.Parse(cardName.Substring(3));
    }

    public bool GetIsHide()
    {
        return isHide;
    }

    public void SetCardHide(bool isHide)
    {
        this.isHide = isHide;

        if(isHide)
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/Sprites/Cards/TrumpCardBack");
        else
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/Sprites/Cards/" + cardName.Substring(0, 3) + "/" + cardName);
    }
}