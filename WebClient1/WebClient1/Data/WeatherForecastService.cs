namespace WebClient1.Data
{
    public class WeatherForecastService
    {
        

        public Task<WeatherForecast[]> GetForecastAsync(DateTime startDate)
        {
            return Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = startDate.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = ConstData.Summaries[Random.Shared.Next(ConstData.Summaries.Length)]
            }).ToArray());
        }
    }
}