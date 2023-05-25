// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");
using Microsoft.CognitiveServices.Speech;
using System;
using System.Threading.Tasks;

namespace Speech_To_Text_app
{
    class Program
    {

        static async Task Main(string[] args)
        {
            await RecognizeSpeech();
            Console.WriteLine("Finished");
        }

        private static async Task RecognizeSpeech()
        {
            var configuration = SpeechConfig.FromSubscription("00e1204b96cf4fe6a604c593347578ff", "eastus");
            //configuration.SpeechRecognitionLanguage = "en";
            using (var recog = new SpeechRecognizer(configuration))
            {
                Console.WriteLine("Speak something...");
                var result = await recog.RecognizeOnceAsync();
                Console.WriteLine("Can this be understood?");
                Console.WriteLine(result.Reason);
                if (result.Reason == ResultReason.RecognizedSpeech)
                {
                    Console.WriteLine(result.Text);
                }
                else if (result.Reason == ResultReason.NoMatch)
                {
                    Console.WriteLine($"NOMATCH: SPEECH could not be recognized.");
                    Console.WriteLine(result.Text);
                }
                else if (result.Reason == ResultReason.Canceled)
                {
                    var cancellation = CancellationDetails.FromResult(result);
                    Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

                    if (cancellation.Reason == CancellationReason.Error)
                    {
                        Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                        Console.WriteLine($"CANCELED: ErrorDetails={cancellation.ErrorDetails}");
                        Console.WriteLine($"CANCELED: Did you update the subscription info?");
                    }
                }
            }
        }
    }
}