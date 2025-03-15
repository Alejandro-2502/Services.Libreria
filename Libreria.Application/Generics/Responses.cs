using System.Net;

namespace Libreria.Application.Generics;

public class Responses<T>
{
    public bool Success { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    public List<string> Messages { get; set; } = null;
    public T Response { get; set; }
    public Responses()
    {
        Messages = new List<string>();
    }
    public Responses(HttpStatusCode httpStatusCode, T response, bool success = true)
    {
        Success = success;
        Response = response;
        Messages = new List<string>();
        StatusCode = httpStatusCode;
    }

    public void AddMessages(string mensaje)
    {
        Messages.Add(mensaje);
    }
}
