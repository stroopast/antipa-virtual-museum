using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewQuizData", menuName = "Quiz/Quiz Data")]
public class QuizData : ScriptableObject
{
    [System.Serializable]
    public class Question
    {
        [TextArea] public string questionText;
        public string correctAnswer;
        public string questionNumber;
    }

    public List<Question> questions = new List<Question>();
}
