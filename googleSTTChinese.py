import io
from google.oauth2 import service_account
from google.cloud import speech

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
print(response)