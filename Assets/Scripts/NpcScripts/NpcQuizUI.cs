using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NpcQuizUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI feedbackText;
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TMP_InputField answerInput;

    [SerializeField] private Button questionNumber;
    [SerializeField] private Button submitBtn;
    [SerializeField] private QuizData quizData;
    [SerializeField] private GameObject npcQuiz2UI;

    private int currentQuestionIndex = 0;
    private int score = 0;
    private bool inputBlocked = false;

    private void Awake()
    {
        submitBtn.onClick.AddListener(CheckAnswer);
    }

    public void CheckAnswer()
    {
        if (inputBlocked)
        {
            return;
        }

        inputBlocked = true;

        string userAnswer = answerInput.text.Trim().ToLower();
        string correctAnswer = quizData.questions[currentQuestionIndex].correctAnswer.Trim().ToLower();

        if (userAnswer == correctAnswer)
        {
            feedbackText.text = "<color=green>Răspuns Corect!</color>";
            score++;
        }
        else
        {
            feedbackText.text = "<color=red>Greșit! Răspunsul corect este: " + quizData.questions[currentQuestionIndex].correctAnswer + "</color>";
        }

        currentQuestionIndex++;
        StartCoroutine(WaitAndLoadNextQuestion());
    }

    void LoadQuestion()
    {
        if (currentQuestionIndex < quizData.questions.Count)
        {
            questionText.text = quizData.questions[currentQuestionIndex].questionText;
            answerInput.text = "";
            feedbackText.text = "";
            questionNumber.GetComponentInChildren<TMP_Text>().text = quizData.questions[currentQuestionIndex].questionNumber;
        }
        else
        {
            ShowFinalScore();
        }
    }

    void ShowFinalScore()
    {
        PlayerScore.Instance.UpdateScore(score);
        scoreText.text = $"Felicitări ai acumulat {score} puncte la primul test! Pregătește-te pentru urmatorul test!";
        questionText.text = "";
        feedbackText.text = "";
        HideContent();
        StartCoroutine(WaitUntilNextQuiz());
    }

    private IEnumerator WaitUntilNextQuiz()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
        npcQuiz2UI.gameObject.SetActive(true);
    }

    private IEnumerator WaitAndLoadNextQuestion()
    {
        yield return new WaitForSeconds(2f);
        LoadQuestion();
        inputBlocked = false;
    }

    private void OnEnable()
    {
        HelperFunctions.UnlockCursor();
        scoreText.text = "";
        currentQuestionIndex = 0;
        ShowContent();
        LoadQuestion();
    }

    private void HideContent()
    {
        answerInput.gameObject.SetActive(false);
        submitBtn.gameObject.SetActive(false);
        questionNumber.gameObject.SetActive(false);
    }

    private void ShowContent()
    {
        answerInput.gameObject.SetActive(true);
        submitBtn.gameObject.SetActive(true);
        questionNumber.gameObject.SetActive(true);
    }

}
