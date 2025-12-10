using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

[System.Serializable]
public class CurrentWeather
{
    public float temperature;
    public float windspeed;
    public float winddirection;
    public int weathercode;
    public string time;
}

[System.Serializable]
public class OpenMeteoResponse
{
    public float latitude;
    public float longitude;
    public string timezone;
    public CurrentWeather current_weather;
}

public class ServerDataFetcher : MonoBehaviour
{
    [Header("UI елементи")]
    public TMP_Text serverDataText;

    [Header("URL API сервера (Open-Meteo для Києва)")]
    public string apiUrl =
        "https://api.open-meteo.com/v1/forecast?latitude=50.45&longitude=30.52&current_weather=true";


    public void FetchDataFromServer()
    {
        StartCoroutine(FetchCoroutine());
    }

    private IEnumerator FetchCoroutine()
    {
        if (serverDataText != null)
            serverDataText.text = "Отримую дані про погоду...";

        using (UnityWebRequest request = UnityWebRequest.Get(apiUrl))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Weather API error: " + request.error);
                if (serverDataText != null)
                    serverDataText.text = "Помилка: " + request.error;
            }
            else
            {
                string json = request.downloadHandler.text;
                Debug.Log("Weather API response: " + json);

                OpenMeteoResponse data = null;
                try
                {
                    data = JsonUtility.FromJson<OpenMeteoResponse>(json);
                }
                catch
                {

                }

                if (data != null && data.current_weather != null)
                {
                    if (serverDataText != null)
                    {
                        serverDataText.text =
                            $"Погода (Київ)\n" +
                            $"Температура: {data.current_weather.temperature:0.0} °C\n" +
                            $"Вітер: {data.current_weather.windspeed:0.0} м/с\n" +
                            $"Напрям вітру: {data.current_weather.winddirection:0}°\n" +
                            $"Час вимірювання: {data.current_weather.time}\n" +
                            $"Часовий пояс: {data.timezone}";
                    }
                }
                else
                {
                    if (serverDataText != null)
                        serverDataText.text = "Не вдалося розпарсити відповідь.\nJSON:\n" + json;
                }
            }
        }
    }
}