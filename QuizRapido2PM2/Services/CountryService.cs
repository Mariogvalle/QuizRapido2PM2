using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using QuizRapido2PM2.Models;


namespace QuizRapido2PM2.Services
{
    public class CountryService
    {
        private readonly HttpClient _httpClient;

        public CountryService()
        {
            _httpClient = new HttpClient();
        }

    }

    public class Country
    {
        public Name Name { get; set; }
    }

    public class Name
    {
        public string Common { get; set; }
    }
}
