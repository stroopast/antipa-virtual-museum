using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;



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
    private string animalName;
    private int currentQuestionIndex = 0;
    private int score = 0;

    private const int MAX_SCORE = 4;
    private bool inputBlocked = false;

    private void Update()
    {
        HandleInput();
    }
    public void LoadQuizData(QuizData data, ExhibitData exhibit)
    {
        quizData = data;
        animalName = exhibit.exhibitName;
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
            CheckQuizCompletion();
        }
    }

    void CheckAnswer()
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

    void ShowFinalScore()
    {
        if(score < MAX_SCORE / 2)
        {
            scoreText.text = $"Ai răspuns corect la {score}/{quizData.questions.Count} întrebări. Nu-i nimic, mai încearcă! Fiecare răspuns greșit este o lecție.";
        }
        else
        {
            scoreText.text = $"Felicitări! Ai răspuns corect la {score}/{quizData.questions.Count} întrebări!";
        }
        questionText.text = "";
        feedbackText.text = "";
        answerInput.gameObject.SetActive(false);
        submitButton.gameObject.SetActive(false);
        questionNumber.gameObject.SetActive(false);
        tryAgainButton.gameObject.SetActive(true);
    }

    void CheckQuizCompletion()
    {
        if (score == MAX_SCORE)
        {
            AchievementManager.Instance.UnlockAchievement(animalName);
            ExitQuiz();
        }
        else
        {
            ShowFinalScore();
        }
    }

    public void ResetQuiz()
    {
        currentQuestionIndex = 0;
        score = 0;
        scoreText.text = "";
        answerInput.gameObject.SetActive(true);
        submitButton.gameObject.SetActive(true);
        questionNumber.gameObject.SetActive(true);
        tryAgainButton.gameObject.SetActive(false);
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

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitQuiz();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            CheckAnswer();
        }
    }

    private IEnumerator WaitAndLoadNextQuestion()
    {
        yield return new WaitForSeconds(2f);
        LoadQuestion();
        inputBlocked = false;
    }
}
