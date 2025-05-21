using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;



public class ExhibitQuizMenuUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private TMP_InputField answerInput;
    [SerializeField] private TextMeshProUGUI feedbackText;
    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private Button questionNumber;
    [SerializeField] private Button submitBtn;
    [SerializeField] private Button tryAgainBtn;
    [SerializeField] private Button backBtn;

    [SerializeField] private GameObject ExhibitMainMenuUI;

    private QuizData quizData;
    private string animalName;
    private int currentQuestionIndex = 0;
    private int score = 0;

    private const int MAX_SCORE = 4;
    private bool inputBlocked = false;
    private Coroutine loadNextQuestionCoroutine;

    private void Awake()
    {
        submitBtn.onClick.AddListener(CheckAnswer);

        backBtn.onClick.AddListener(() =>
        {
            ExitQuiz();
            ExhibitMainMenuUI.gameObject.SetActive(true);
            HelperFunctions.UnlockCursor();
        });

        tryAgainBtn.onClick.AddListener(() =>
        {
            ResetQuiz();
        });
    }

    private void Start()
    {
        Hide();
    }

    public void LoadQuizData(QuizData data, ExhibitData exhibit)
    {
        quizData = data;
        animalName = exhibit.exhibitName;
    }

    public void StartQuiz()
    {
        tryAgainBtn.gameObject.SetActive(false);
        backBtn.gameObject.SetActive(true);
        answerInput.gameObject.SetActive(true);
        submitBtn.gameObject.SetActive(true);
        questionNumber.gameObject.SetActive(true);
        //submitBtn.onClick.RemoveAllListeners();
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
        loadNextQuestionCoroutine  = StartCoroutine(WaitAndLoadNextQuestion());
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
        submitBtn.gameObject.SetActive(false);
        questionNumber.gameObject.SetActive(false);
        tryAgainBtn.gameObject.SetActive(true);
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
        submitBtn.gameObject.SetActive(true);
        questionNumber.gameObject.SetActive(true);
        tryAgainBtn.gameObject.SetActive(false);
        LoadQuestion();
    }

    public void ExitQuiz()
    {
        if (loadNextQuestionCoroutine != null)
        {
            StopCoroutine(loadNextQuestionCoroutine);
            loadNextQuestionCoroutine = null;
        }

        inputBlocked = false;
        currentQuestionIndex = 0;
        score = 0;
        scoreText.text = "";
        HelperFunctions.LockCursor();
        gameObject.SetActive(false);
    }

    private IEnumerator WaitAndLoadNextQuestion()
    {
        yield return new WaitForSeconds(2f);
        LoadQuestion();
        inputBlocked = false;
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
}
