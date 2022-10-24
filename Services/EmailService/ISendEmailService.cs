using EngineMonitoring.Models;

namespace EngineMonitoring.Services
{
    public interface ISendEmailService
    {
        string Send(Email email);
    }
}