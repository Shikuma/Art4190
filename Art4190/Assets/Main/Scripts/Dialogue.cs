using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{

    public Text dialogueText;
    public GameObject panel;
    public string[] goodFeedback, badFeedback, neutralFeedback;
    private GameStats gs;

    // Use this for initialization
    void Start()
    {
        gs = GetComponent<GameStats>();
        panel.SetActive(false);
    }
    public void GiveBadFeedBack() {
        dialogueText.text = FeedBack(0);
        panel.SetActive(true);
    }
    public void GiveGoodFeedBack() {
        dialogueText.text = FeedBack(100);
        panel.SetActive(Random.Range(0, 5) >= 2); 
    }
    public void GiveFeedBack()
    {
        dialogueText.text = FeedBack(gs.rating);
        panel.SetActive(Random.Range(0, 5) >= 1);
    }
    private string FeedBack(float score)
    {
        int random = Random.Range(0, 3);
        string feedback;
        if (score > 75) feedback = RandomGoodFeedBack();
        else if (score > 50) feedback = RandomNeutralFeedBack();
        else feedback = RandomBadFeedBack();
        return feedback;
    }
    private string RandomGoodFeedBack()
    {
        return goodFeedback[Random.Range(0, goodFeedback.Length)];
    }
    private string RandomBadFeedBack()
    {
        return badFeedback[Random.Range(0, badFeedback.Length)];
    }
    private string RandomNeutralFeedBack()
    {
        return neutralFeedback[Random.Range(0, neutralFeedback.Length)];
    }
}
