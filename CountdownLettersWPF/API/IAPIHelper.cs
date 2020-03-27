using System.Net.Http;

namespace CountdownLettersWPF.API
{
    public interface IAPIHelper
    {
        HttpClient ApiClient { get; }
    }
}