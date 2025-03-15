namespace Libreria.Application.Interfaces.ICommon;

public interface ILogServicesInteractor
{
    void LogTrace(string message);
    void LogError(string message);
}
