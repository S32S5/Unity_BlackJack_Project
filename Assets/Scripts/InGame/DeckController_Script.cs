/**
 * Manage related to deck system
 * 
 * Script description
 * - Draw new card
 * - Get score and update score's text
 * - Open hiding card
 * 
 * @version 0.1, First version
 * @author S3
 * @date 2024/01/27
 */

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DeckController_Script : MonoBehaviour
{
    private GameObject cardPrefab;

    public List<GameObject>[] decks = new List<GameObject>[2];
    private List<string> cards;
    private Text[] Score_Texts = new Text[2];

    private static float[] deckPosition = new float[2] { 3.75f, -3.25f};
    private static float cardDistance = 0.5f;
    
    //Specifies
    private void Awake()
    {
        cardPrefab = Resources.Load("Prefabs/CardPrefab") as GameObject;

        for(int i = 0; i< decks.Length; i++)
            decks[i] = new List<GameObject>();
        cards = new List<string>();

        Score_Texts[0] = GameObject.Find("DealerScore_Text").GetComponent<Text>();
        Score_Texts[1] = GameObject.Find("PlayerScore_Text").GetComponent<Text>();
    }

    // Init deck
    public void Init()
    {
        for(int i = 0; i < decks.Length;i++)
            if (decks[i] != new List<GameObject>())
            {
                foreach (GameObject card in decks[i])
                    Destroy(card);

                decks[i].Clear();
            }

        if (cards != new List<string>())
            cards.Clear();
        string[] cardName = { "Clo", "Dia", "Hrt", "Spd" };
        foreach (string name in cardName)
        {
            for (int j = 1; j <= 13; j++)
                cards.Add(name + j.ToString());
        }

        for (int i = 0; i < Score_Texts.Length; i++)
            Score_Texts[i].text = "";
    }

    // Draw new card
    public IEnumerator DrawCard(bool hide, int deck)
    {
        int ranInt = Random.Range(0, cards.Count);
        string drawnCard = cards[ranInt];
        GameObject pf = Instantiate(cardPrefab);
        pf.GetComponent<Card_Script>().Init(drawnCard, hide);

        while (true)
        {
            yield return null;
            if (pf.transform.position.y < deckPosition[deck])
            {
                pf.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                Destroy(pf.GetComponent<AudioSource>());

                cards.Remove(cards[ranInt]);
                decks[deck].Add(pf);
                UpdateScore_Text(deck);

                float startX = -(cardDistance / 2 * (decks[deck].Count - 1));
                for(int i = 0; i < decks[deck].Count; i++)
                {
                    decks[deck][i].transform.position = new Vector3(startX + cardDistance * i, deckPosition[deck], 0);
                    decks[deck][i].GetComponent<SpriteRenderer>().sortingOrder = i;
                }

                break;
            }
        }
    }

    /*
     * Calculate deck's card sum
     * 
     * @param List<GameObject> deck
     * @return int card's sum
     */
    public int GetScore(int deck)
    {
        List<int> cardNumbers = new List<int>();
        foreach (GameObject card in decks[deck])
        {
            if (!card.GetComponent<Card_Script>().GetIsHide())
                cardNumbers.Add(card.GetComponent<Card_Script>().GetCardNumber());
        }
        cardNumbers = cardNumbers.OrderByDescending(x => x).ToList();

        int aNumber = 0;
        for (int i = 0; i < cardNumbers.Count; i++)
            if (cardNumbers[i] == 1)
                aNumber++;
        int score = 0;
        for (int i = 0; i < cardNumbers.Count - aNumber; i++)
        {
            if (cardNumbers[i] >= 10)
                score += 10;
            else
                score += cardNumbers[i];
        }
        if (aNumber > 0)
        {
            int bestCaseNumber = 11 + aNumber - 1;

            if (score + bestCaseNumber <= 21)
                score += bestCaseNumber;
            else
                score += aNumber;
        }

        return score;
    }

    /*
     * Update Score_Text's text
     * 
     * @param int deck number
     */
    public void UpdateScore_Text(int deck)
    {
        Score_Texts[deck].text = GetScore(deck).ToString();
    }

    /*
     * Check setted card and return condition
     * 
     * @return int condition-1 == player black jack, 2 == player black jack and open card is 1, 3 == insurance-
     */
    public int checkSettedCard()
    {
        if (decks[0][1].GetComponent<Card_Script>().GetCardNumber() == 1)
        {
            if (GetScore(1) == 21)
                return 2;
            else
                return 3;
        }
        else if (GetScore(1) == 21)
            return 1;
        else
            return 0;
    }

    // Open hiding card
    public void OpenHidingCard()
    {
        decks[0][0].GetComponent<Card_Script>().SetCardHide(false);
        UpdateScore_Text(0);
    }
}