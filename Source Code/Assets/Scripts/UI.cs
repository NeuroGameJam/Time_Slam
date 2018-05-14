using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour 
{
    public static UI instance;

    [SerializeField, Multiline] private string scoreInitialText;
    [SerializeField] private string[] niceScoreSamples;
    [SerializeField] private TextMesh scoreText;
    [SerializeField] private TextMesh finalCounterText;
    [SerializeField] private TextMesh niceShotText;
    [SerializeField] private Animator niceShotAnim;
    [SerializeField] private Animator finalButtonsAnim;

    private void Awake()
    {
        if (instance && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

    }

    private void Start()
    {

        scoreText.text = scoreInitialText + 0;
        GameManager.instance.OnScore += AtualizeScore;
    }

    private void AtualizeScore(int score)
    {
        scoreText.text = scoreInitialText + score;
        ShowNiceScore();
    }

    private void ShowNiceScore()
    {
        int randomTextIndex = Random.Range(0, niceScoreSamples.Length);
        niceShotText.text = niceScoreSamples[randomTextIndex];
        niceShotAnim.SetTrigger("ShowNiceShot");
    }

    public void ShowFinalButtons()
    {
        finalButtonsAnim.SetBool("Show Buttons", true);
    }

    public void AtualizeFinalCounterText(int secconds)
    {
        int minutes = (int) secconds / 60;
        int sec = secconds - (minutes * 60);
        if (sec < 0)
            sec = 0;
        string secondsText = sec == 0 ? "00" : sec < 10 ? "0" + sec : sec.ToString();

        finalCounterText.text = minutes + ":" + secondsText;
    }

}
