using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
using Newtonsoft.Json;
using System.Collections;
using System.Net;
using System;
#endif

namespace YG
{
    public class TokenInfo
    {
        public string iamToken { get; set; }
        public string expiresAt { get; set; }
    }

    public class RootTranslation
    {
        public List<Translation> translations { get; set; }
    }

    public class Translation
    {
        public string text { get; set; }
        public string detectedLanguageCode { get; set; }
    }

    public class LanguageYG : MonoBehaviour
    {
        public Text textUIComponent;
        public TextMesh textMeshComponent;
        [SerializeField] public InfoYG infoYG;
        [Space(10)]
        public string text;
        [Tooltip("RUSSIAN")]
        public string ru, en, tr, az, be, he, hy, ka, et, fr, kk, ky, lt, lv, ro, tg, tk, uk, uz, es, pt, ar, id, ja, it, de, hi;
        public int fontNumber;
        public Font uniqueFont;
        int baseFontSize;

        private static string token;
        private static int hourCountForNewToken = 3;
        private static DateTime timeExpiresToken = DateTime.Now;

        public InfoYG GetInfoYG()
        {
            YandexGame yg = (YandexGame)GameObject.FindObjectOfType<YandexGame>();

            if (yg)
            {
                return yg.infoYG;
            }
            else
            {
#if UNITY_EDITOR
                GameObject ygPrefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/YandexGame/Prefabs/YandexGame.prefab", typeof(GameObject));
                yg = ygPrefab.GetComponent<YandexGame>();
                return yg.infoYG;
#else
                return null;
#endif
            }

        }

        private void Awake()
        {
            // Раскомментируйте нижнюю строку, если вы получаете какие-либо ошибки связанные с InfoYG. В каких то случаях, это может помочь.
            // Uncomment the bottom line if you get any errors related to infoYG. In some cases, it may help.
            //Serialize();
            if (textUIComponent)
                baseFontSize = textUIComponent.fontSize;
            else if (textMeshComponent)
                baseFontSize = textMeshComponent.fontSize;
        }

        [ContextMenu("Reserialize")]
        public void Serialize()
        {
            textUIComponent = GetComponent<Text>();
            textMeshComponent = GetComponent<TextMesh>();
        }

        private void OnEnable()
        {
            YandexGame.SwitchLangEvent += SwitchLanguage;
            SwitchLanguage(YandexGame.savesData.language);
        }

        private void OnDisable() => YandexGame.SwitchLangEvent -= SwitchLanguage;

        public void SwitchLanguage(string lang)
        {
            for (int i = 0; i < languages.Length; i++)
            {
                if (lang == infoYG.LangName(i))
                {
                    AssignTranslate(languages[i]);
                    ChangeFont(infoYG.GetFont(i));
                    FontSizeCorrect(infoYG.GetFontSize(i));
                }
            }
        }

        void AssignTranslate(string translation)
        {
            if (textUIComponent)
                textUIComponent.text = translation;
            else if (textMeshComponent)
                textMeshComponent.text = translation;
        }

        public void ChangeFont(Font[] fontArray)
        {
            Font font;

            if (fontArray.Length >= fontNumber + 1 && fontArray[fontNumber])
            {
                font = fontArray[fontNumber];
            }
            else font = null;

            if (uniqueFont)
            {
                font = uniqueFont;
            }
            else if (font == null)
            {
                if (infoYG.fonts.defaultFont.Length >= fontNumber + 1 && infoYG.fonts.defaultFont[fontNumber])
                {
                    font = infoYG.fonts.defaultFont[fontNumber];
                }
                else if (infoYG.fonts.defaultFont.Length >= 1 && infoYG.fonts.defaultFont[0])
                {
                    font = infoYG.fonts.defaultFont[0];
                }
            }

            if (font != null)
            {
                if (textUIComponent)
                    textUIComponent.font = font;
                else if (textMeshComponent)
                    textMeshComponent.font = font;
            }
        }

        void FontSizeCorrect(int[] fontSizeArray)
        {
            if (textUIComponent)
                textUIComponent.fontSize = baseFontSize;
            else if (textMeshComponent)
                textMeshComponent.fontSize = baseFontSize;

            if (fontSizeArray.Length != 0 && fontSizeArray.Length >= fontNumber - 1)
            {
                if (textUIComponent)
                    textUIComponent.fontSize += fontSizeArray[fontNumber];
                else if (textMeshComponent)
                    textMeshComponent.fontSize += fontSizeArray[fontNumber];
            }
        }

        public string[] languages
        {
            get
            {
                string[] s = new string[27];

                s[0] = ru;
                s[1] = en;
                s[2] = tr;
                s[3] = az;
                s[4] = be;
                s[5] = he;
                s[6] = hy;
                s[7] = ka;
                s[8] = et;
                s[9] = fr;
                s[10] = kk;
                s[11] = ky;
                s[12] = lt;
                s[13] = lv;
                s[14] = ro;
                s[15] = tg;
                s[16] = tk;
                s[17] = uk;
                s[18] = uz;
                s[19] = es;
                s[20] = pt;
                s[21] = ar;
                s[22] = id;
                s[23] = ja;
                s[24] = it;
                s[25] = de;
                s[26] = hi;
                return s;
            }
            set
            {
                ru = value[0];
                en = value[1];
                tr = value[2];
                az = value[3];
                be = value[4];
                he = value[5];
                hy = value[6];
                ka = value[7];
                et = value[8];
                fr = value[9];
                kk = value[10];
                ky = value[11];
                lt = value[12];
                lv = value[13];
                ro = value[14];
                tg = value[15];
                tk = value[16];
                uk = value[17];
                uz = value[18];
                es = value[19];
                pt = value[20];
                ar = value[21];
                id = value[22];
                ja = value[23];
                it = value[24];
                de = value[25];
                hi = value[26];
            }
        }

#if UNITY_EDITOR
        public float textHeight = 20f;
        public string processTranslateLabel;
        public bool componentTextField;

        public void SetLang(int index, string text)
        {
            string[] str = languages;
            str[index] = text;

            languages = str;
        }

        public void Translate(int countLangAvailable)
        {
            StartCoroutine(TranslateEmptyFields(countLangAvailable));
        }

        string Translate(string translationTo = "en")
        {
            string text;

            if (!componentTextField)
                text = this.text;
            else if (textUIComponent)
                text = textUIComponent.text;
            else if (textMeshComponent)
                text = textMeshComponent.text;
            else
            {
                Debug.LogError("(ruСообщение)Текст для перевода не найден!\n(enMessage)The text for translation was not found!");
                return null;
            }

            var url = "https://translate.api.cloud.yandex.net/translate/v2/translate";

            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";

            httpRequest.Accept = "application/json";
            httpRequest.Headers["Authorization"] = "Bearer " + GetNewToken();
            httpRequest.ContentType = "application/json";


            string data = @"{ 
            ""folderId"": ""b1g4sfub2o5ejcr20d4s"",
            ""texts"" : [""" + text + @"""],
            ""targetLanguageCode"": """ + translationTo + @"""
             }";

            using (StreamWriter streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
                streamWriter.Write(data);
            }

            HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse();

            string result;

            using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }

            string response;

            try
            {
                RootTranslation translate = JsonConvert.DeserializeObject<RootTranslation>(result);
                response = translate.translations[0].text;
            }
            catch
            {
                response = "process error";
                StopAllCoroutines();
                processTranslateLabel = processTranslateLabel + " error";

                Debug.LogError("Статус: " + httpResponse.StatusCode + "\nЗапрос вернул: " + result);
            }

            return response;
        }

        private string GetNewToken()
        {
            //return token;

            DateTime dateTimeNow = DateTime.Now;

            int minuteDifference = timeExpiresToken.Minute - dateTimeNow.Minute;
            int hourDifference = timeExpiresToken.Hour - dateTimeNow.Hour;
            int dayDifference = timeExpiresToken.Day - dateTimeNow.Day;
            int totalDifference = dayDifference * 24 * 60 + hourDifference * 60 + minuteDifference;

            if (totalDifference > 0)
            {
                int minuteCount = minuteDifference;
                int hourCount = hourDifference >= 0 ? hourDifference : 24 + hourDifference;

                if (minuteDifference < 0)
                {
                    minuteCount = 60 + minuteDifference;
                    hourCount--;
                }

                //Debug.Log($"До обновления токена еще {hourCount} часов {minuteCount} минут");
                return token;
            }

            string data = @"{
            ""yandexPassportOauthToken"" : ""y0_AgAAAABUkA-LAATuwQAAAADbvuOWkd0wFsbqQyqVacZgsXwsMmK7a5s""
            }";

            string url = "https://iam.api.cloud.yandex.net/iam/v1/tokens";

            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";

            httpRequest.Accept = "application/json";
            httpRequest.ContentType = "application/json";

            using (StreamWriter streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
                streamWriter.Write(data);
            }

            HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse();

            string result;

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }

            DateTime newTokenTime = DateTime.Now;
            timeExpiresToken = newTokenTime.AddHours(hourCountForNewToken);

            TokenInfo tokenInfo = JsonConvert.DeserializeObject<TokenInfo>(result);

            //timeExpiresToken = DateTime.ParseExact(tokenInfo.expiresAt, "O", System.Globalization.CultureInfo.CurrentCulture);

            token = tokenInfo.iamToken;

            return tokenInfo.iamToken;
        }

        public int countLang = 0;
        IEnumerator TranslateEmptyFields(int countLangAvailable)
        {
            countLang = 0;
            processTranslateLabel = "processing... 0/" + countLangAvailable;

            for (int i = 0; i < languages.Length; i++)
            {
                if (infoYG.LangArr()[i] && (languages[i] == null || languages[i] == ""))
                {
                    bool complete = false;
                    SetLang(i, Translate(infoYG.LangName(i)));

                    if (processTranslateLabel.Contains("error"))
                        processTranslateLabel = countLang + "/" + countLangAvailable + " error";
                    else
                    {
                        complete = true;
                        processTranslateLabel = countLang + "/" + countLangAvailable;
                    }

                    yield return complete == true;
                    countLang++;
                }
            }

            processTranslateLabel = countLang + "/" + countLangAvailable + " completed";
        }
#endif
    }
}