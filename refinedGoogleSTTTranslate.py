import io
import googletrans
from googletrans import Translator
from google.oauth2 import service_account
from google.cloud import speech

# Indicating where to start reading the output
#print('START HERE')

client_file = 'key.json'
credentials = service_account.Credentials.from_service_account_file(client_file)
client = speech.SpeechClient(credentials=credentials)

# Load audio file
audio_file = 'harvestL1.wav'
with io.open(audio_file, 'rb') as f:
    content = f.read()
    audio = speech.RecognitionAudio(content=content)

config = speech.RecognitionConfig(
    encoding=speech.RecognitionConfig.AudioEncoding.LINEAR16,
    sample_rate_hertz=44100,
    language_code='zh',
    audio_channel_count=2,
    enable_separate_recognition_per_channel=True
)

response = client.recognize(config=config, audio=audio)
cleanResult = ""
timesLooped = 0
for result in response.results:
    if timesLooped < 1:
        #print('im here')
        #print('current result', result)
        #print(result.alternatives[0].transcript)
        cleanResult += result.alternatives[0].transcript
        #print('the current cleanResult', cleanResult)
        timesLooped += 1
        #print('timesLooped', timesLooped)

print('Original Chinese Text', cleanResult)

#print(response)
strResponse = str(response)

# Translating it into English
translator = Translator(service_urls=['translate.googleapis.com'])
#print('Original Chinese Text: ', strResponse)
translatedText = translator.translate(cleanResult, dest='en')
#for result in translatedText.results:
#    print(result.alternatives[0].transcript)
#print('Translated From Chinese: ', translator.translate(strResponse))
print('Translated From Chinese', translatedText)