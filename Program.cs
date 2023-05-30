using System;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.CognitiveServices.Speech.Translation;

namespace SpeechTranslation
{
    class Program
    {
        static void Main(String[] args)
        {
            TranslateSpeech().Wait();
            Console.ReadKey();
        }

        private static async Task TranslateSpeech()
        {
            string fromLanguage = "es-mx";
            var config = SpeechTranslationConfig.FromSubscription("SUBSCRIPTION_KEY", "eastus");
            config.SpeechRecognitionLanguage = fromLanguage;
            config.AddTargetLanguage("en");

            //const string frenchVoice = "fr-FR";
            //config.VoiceName = frenchVoice;

            using (var recognizer = new TranslationRecognizer(config))
            {
                recognizer.Recognized += (s, e) =>
                {
                    if (e.Result.Reason == ResultReason.TranslatedSpeech)
                    {
                        Console.WriteLine($"\nRecognized text in {fromLanguage} - {e.Result.Text}.");
                    }

                    foreach (var element in e.Result.Translations)
                    {
                        Console.WriteLine($"TRANSLATING into '{element.Key}' - {element.Value}");
                    }
                };

                recognizer.Synthesizing += (s, e) => {
                    var audio = e.Result.GetAudio();
                    if (audio.Length > 0)
                    {
                        Console.WriteLine($"Audio size; {audio.Length}");
                        File.WriteAllBytes("MyFrenchSpeech.wav", audio);
                    }
                };

                recognizer.SessionStarted += (s, e) => { Console.WriteLine("\nSession started. Please speak something..."); };
                recognizer.SessionStopped += (s, e) => { Console.WriteLine("\nSession stopped event."); };

                await recognizer.StartContinuousRecognitionAsync();
                do
                {
                    Console.WriteLine("Enter key to stop...");
                }
                while (Console.ReadKey().Key != ConsoleKey.Enter);
                await recognizer.StopContinuousRecognitionAsync();
            }
        }
    }
}