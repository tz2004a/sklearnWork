import googletrans
from googletrans import Translator

#print(googletrans.LANGUAGES)

text1 = "subscribe to my channel"
#print('type of text1', type(text1))
text2 = "suscríbete a mi canal"

text3 = "kanalıma abone ol"

translator = Translator(service_urls=['translate.googleapis.com'])
#translator = Translator()
#print('type of translator', type(translator))
#translator.detect(text2)

# Detecting the text's language
print(translator.detect(text1))
print(translator.detect(text2))
print(translator.detect(text3))

# Translating the text into another English
print('Original Spanish Text: ', text2)
print('Translated From Spanish: ', translator.translate(text2))
print('Original Turkish Text: ', text3)
print('Translated From Turkish: ', translator.translate(text3))