using AVFoundation;
using TodoApp.BL.Services;

namespace TodoApp.iOS.Services
{
  public class TextToSpeechService : ITextToSpeechService
  {
    private const float Volume = 0.5f;
    private const float Pitch = 1.0f;

    public void Say(string text)
    {
      if (string.IsNullOrWhiteSpace(text))
        return;

      var speechSynthesizer = new AVSpeechSynthesizer();
      var speechUtterance = new AVSpeechUtterance(text)
      {
        Rate = AVSpeechUtterance.MaximumSpeechRate / 3,
        Voice = AVSpeechSynthesisVoice.FromLanguage("en-US"),
        Volume = Volume,
        PitchMultiplier = Pitch
      };

      speechSynthesizer.SpeakUtterance(speechUtterance);
    }
  }
}