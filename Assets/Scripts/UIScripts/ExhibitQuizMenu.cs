using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExhibitQuizMenu : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public TMP_InputField answerInput;
    public TextMeshProUGUI feedbackText;
    public TextMeshProUGUI scoreText;
    public Button questionNumber;
    public Button submitButton;
    public Button tryAgainButton;
    public Button backButton;

    private QuizData quizData;
    private int currentQuestionIndex = 0;
    private int score = 0;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ExitQuiz();
        }

    }
    public void LoadQuizData(QuizData data)
    {
        quizData = data;
    }

    public void StartQuiz()
    {
        tryAgainButton.gameObject.SetActive(false);
        backButton.gameObject.SetActive(true);
        answerInput.gameObject.SetActive(true);
        submitButton.gameObject.SetActive(true);
        questionNumber.gameObject.SetActive(true);
        submitButton.onClick.RemoveAllListeners();
        submitButton.onClick.AddListener(CheckAnswer);
        LoadQuestion();
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

    void CheckAnswer()
    {
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
        Invoke(nameof(LoadQuestion), 2f); // Wait 2 seconds before next question
    }

    void ShowFinalScore()
    {
        questionText.text = "";
        feedbackText.text = "";
        scoreText.text = $"Felicitări! Ai răspuns corect la {score}/{quizData.questions.Count} întrebări!";
        answerInput.gameObject.SetActive(false);
        submitButton.gameObject.SetActive(false);
        questionNumber.gameObject.SetActive(false);
        tryAgainButton.gameObject.SetActive(true);
    }

    public void ResetQuiz()
    {
        currentQuestionIndex = 0;
        score = 0;
        scoreText.text = "";
        tryAgainButton.gameObject.SetActive(false);
        answerInput.gameObject.SetActive(true);
        submitButton.gameObject.SetActive(true);
        questionNumber.gameObject.SetActive(true);
        LoadQuestion();
    }

    public void ExitQuiz()
    {
        currentQuestionIndex = 0;
        score = 0;
        scoreText.text = "";
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameObject.SetActive(false);
    }

    public void OpenQuizMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameObject.SetActive(true);
    }
}
