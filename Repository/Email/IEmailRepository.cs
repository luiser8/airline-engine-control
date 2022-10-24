using EngineMonitoring.Models;

namespace EngineMonitoring.Repository
{
    public interface IEmailRepository
    {
        string Send(Email email);
    }
}