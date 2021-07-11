using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class VoiceControl : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    void Start()
    {
        actions.Add('True', TrueAction);
        actions.Add('False', FalseAction);

        keywordRecognizer = new KeywordRecognizer(replys.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech){
        Debug.log(speech.text);

        actions[speech.text].Invoke();

    }

    private void TrueAction(){
        //TODO: Call Function that is called when True Button is pressed;
    }

    private void FalseAction(){
        //TODO: Call Function that is called when False Button is pressed;
    }

}
