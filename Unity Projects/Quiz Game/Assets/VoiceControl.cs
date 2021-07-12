using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class VoiceControl : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;

    private GameManager gameManager;    
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    public void Start()
    {
        actions.Add("True", TrueAction);
        actions.Add("False", FalseAction);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech){
        Debug.Log(speech.text);

        actions[speech.text].Invoke();

    }

    private void TrueAction(){
        gameManager.UserSelectTrue();
    }

    private void FalseAction(){
        gameManager.UserSelectFalse();
    }

}
