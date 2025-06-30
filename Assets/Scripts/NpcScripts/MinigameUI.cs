using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class MinigameUI : MonoBehaviour
{
    [SerializeField] private Button answerBtnA;
    [SerializeField] private Button answerBtnB;
    [SerializeField] private Button answerBtnC;
    [SerializeField] private Button answerBtnD;

    [SerializeField] private Image tracksImage;
    [SerializeField] private TextMeshProUGUI waitingText;

    [SerializeField] private MinigameData minigameData;

    [SerializeField] private GameObject finalScoreUI;

    private int currentQuestionIndex = 0;
    private bool inputBlocked = false;
    private int score = 0;

    private void Update()
    {

    }

    private void Awake()
    {
        answerBtnA.onClick.AddListener(() => CheckAnswer(answerBtnA));
        answerBtnB.onClick.AddListener(() => CheckAnswer(answerBtnB));
        answerBtnC.onClick.AddListener(() => CheckAnswer(answerBtnC));
        answerBtnD.onClick.AddListener(() => CheckAnswer(answerBtnD));
    }

    private void OnEnable()
    {
        HelperFunctions.UnlockCursor();
        currentQuestionIndex = 0;
        LoadQuestion();
    }

    private void LoadQuestion()
    {
        if (currentQuestionIndex < minigameData.questions.Count)
        {
            tracksImage.sprite = minigameData.questions[currentQuestionIndex].animalTracks;
            answerBtnA.GetComponentInChildren<TextMeshProUGUI>().text = minigameData.questions[currentQuestionIndex].answers[0];
            answerBtnB.GetComponentInChildren<TextMeshProUGUI>().text = minigameData.questions[currentQuestionIndex].answers[1];
            answerBtnC.GetComponentInChildren<TextMeshProUGUI>().text = minigameData.questions[currentQuestionIndex].answers[2];
            answerBtnD.GetComponentInChildren<TextMeshProUGUI>().text = minigameData.questions[currentQuestionIndex].answers[3];
        }
        else
        {
            FinishQuiz();
        }
    }

    public void CheckAnswer(Button btn)
    {
        if (inputBlocked)
        {
            return;
        }

        inputBlocked = true;

        string userAnswer = btn.GetComponentInChildren<TextMeshProUGUI>().text;
        string correctAnswer = minigameData.questions[currentQuestionIndex].correctAnswer;

        if(correctAnswer == userAnswer)
        {
            score++;
            StartCoroutine(Flash(btn.GetComponent<Image>(), Color.green));
        }
        else
        {
            StartCoroutine(Flash(btn.GetComponent<Image>(), Color.red));
        }
        currentQuestionIndex++;
        StartCoroutine(WaitAndLoadNextQuestion());
    }

    private void FinishQuiz()
    {
        PlayerScore.Instance.UpdateScore(score);
        if(GameModeManager.Instance.GetGameMode() == 1)
        {
            MultiplayerManager.Instance.UpdatePlayerNpcQuizScore(PlayerScore.Instance.GetPlayerScore());

        }
        finalScoreUI.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    private IEnumerator WaitAndLoadNextQuestion()
    {
        yield return new WaitForSeconds(2f);
        LoadQuestion();
        inputBlocked = false;
    }

    private IEnumerator Flash(Image buttonImage, Color color)
    {
        Color originalColor = buttonImage.color;
        for (int i = 0; i < 3; i++)
        {
            buttonImage.color = color;
            yield return new WaitForSeconds(0.25f);
            buttonImage.color = originalColor;
            yield return new WaitForSeconds(0.25f);
        }
    }
}
