using BlazorApp.Client.Models;
using BlazorApp.Client.Services;
using System.Text;
using System.Text.RegularExpressions;

namespace BlazorApp;

public class ServerWeatherServiceCopy 
{

    private string _filePath;

    private IDictionary<string, string> _settings = new Dictionary<string, string>();
    private IDictionary<string, string> _settingComments = new Dictionary<string, string>();

    private UTF8Encoding _encoding = new UTF8Encoding();

    private const char SPLIT_CHAR = '=';



    public void Save()
    {
        using (FileStream stream = File.Create(_filePath))
        {
            byte[] data = ToByteArray();

            stream.Write(data, 0, data.Length);
        }
    }

    private byte[] ToByteArray()
    {
        StringBuilder builder = new StringBuilder();

        foreach (var pair in _settings)
        {
            if (_settingComments.ContainsKey(pair.Key))
            {
                builder.Append(_settingComments[pair.Key]);
                builder.AppendLine();
            }

            builder.AppendFormat("{0}={1}", pair.Key, pair.Value);
            builder.AppendLine();
        }

        return _encoding.GetBytes(builder.ToString());
    }

    public async Task<WeatherForecast[]> GetWeather()
    {
        // Short simulated delay for obtaining the data
        await Task.Delay(1000);

        var startDate = DateOnly.FromDateTime(DateTime.Now);
        var summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };
        var forecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = startDate.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = summaries[Random.Shared.Next(summaries.Length)]
        }).ToArray();

        return forecasts;
    }

    public string UpdateCustomerPassword(string txtUsername, string txtPassword)
    {
        string userName = txtUsername;
        string password = txtPassword;

        Regex testPassword = new Regex(userName);
        Match match = testPassword.Match(password);
        if (match.Success)
        {
           return "Do not include name in password.";
        }
        else
        {
            return "Good password.";
        }
    }
}
 