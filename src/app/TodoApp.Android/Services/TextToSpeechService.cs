using System.Collections.Generic;
using Android.Speech.Tts;
using Java.Lang;
using TodoApp.BL.Services;
using Xamarin.Forms;

namespace TodoApp.Droid.Services
{
  public class TextToSpeechService : Object, ITextToSpeechService, TextToSpeech.IOnInitListener
  {
    TextToSpeech _speaker;
    string _toSpeak;

    public void Say(string text)
    {
      if (!string.IsNullOrWhiteSpace(text))
      {
        _toSpeak = text;
        if (_speaker == null)
          _speaker = new TextToSpeech(Forms.Context, this);
        else
        {
          var p = new Dictionary<string, string>();
          _speaker.Speak(_toSpeak, QueueMode.Flush, p);
        }
      }
    }
    public void OnInit(OperationResult status)
    {
      if (status.Equals(OperationResult.Success))
      {
        var p = new Dictionary<string, string>();
        _speaker.Speak(_toSpeak, QueueMode.Flush, p);
      }
    }
  }
}