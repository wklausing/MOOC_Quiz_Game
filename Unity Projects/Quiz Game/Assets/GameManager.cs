using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;

public class GameManager : MonoBehaviour
{

public Question[] questions;
private static List<Question> unansweredQuestions;

private Question currentQuestion;

[SerializeField]
private Text factText;

[SerializeField]
private Text trueAnswerText;

[SerializeField]
private Text falseAnswerText;

[SerializeField]
private float timeBetweenQuestions = 1f;

[SerializeField]
private Animator animator;

private KeywordRecognizer keywordRecognizer;
private Dictionary<string, Action> actions = new Dictionary<string, Action>();

void Start () 
{

    if (unansweredQuestions == null || unansweredQuestions.Count == 0)
    {
      unansweredQuestions= questions.ToList<Question>();
    }

    actions.Add("True", UserSelectTrue);
    actions.Add("False", UserSelectFalse);

    keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
    keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
    keywordRecognizer.Start();

    SetCurrentQuestion();
  }

  void SetCurrentQuestion()
  {

    int randomQuestionIndex = UnityEngine.Random.Range(0,unansweredQuestions.Count);
    currentQuestion = unansweredQuestions[randomQuestionIndex];

    factText.text = currentQuestion.fact;

    if(currentQuestion.isTrue)
    {
      trueAnswerText.text="CORRECT";
      falseAnswerText.text = "WRONG";

    }else
    {
      trueAnswerText.text = "WRONG";
      falseAnswerText.text = "CORRECT";
    }

  }

  IEnumerator TransitionToNextQuestion()
  {
    unansweredQuestions.Remove(currentQuestion);
    
    yield return new WaitForSeconds(timeBetweenQuestions);

    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

  }

  private void RecognizedSpeech(PhraseRecognizedEventArgs speech){
        Debug.Log(speech.text);

        actions[speech.text].Invoke();

   }
  
  public void UserSelectTrue()
  {
    animator.SetTrigger("True");
    if(currentQuestion.isTrue)
    {
      Debug.Log("Correct!");

    } else
    {
      Debug.Log("Wrong");
    }

    StartCoroutine(TransitionToNextQuestion());

  }

  
  public void UserSelectFalse()
  {
    animator.SetTrigger("False");
    if(!currentQuestion.isTrue)
    {
      Debug.Log("Correct!");

    } else
    {
      Debug.Log("Wrong");
    }

     StartCoroutine(TransitionToNextQuestion());

  }

}
