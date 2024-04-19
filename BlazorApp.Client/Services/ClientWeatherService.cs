using System.Net.Http.Json;
using System.Text.RegularExpressions;
using BlazorApp.Client.Models;

namespace BlazorApp.Client.Services;

public class ClientWeatherService(HttpClient httpClient) : IWeatherService
{
    public Task<WeatherForecast[]> GetWeather() =>
        httpClient.GetFromJsonAsync<WeatherForecast[]>("/api/weather")!;

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
