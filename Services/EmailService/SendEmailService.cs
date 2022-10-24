using EngineMonitoring.Repository;
using EngineMonitoring.Models;

namespace EngineMonitoring.Services
{
    public class SendEmailService : ISendEmailService
    {
        private readonly IEmailRepository _emailRepository;

        public SendEmailService(IEmailRepository emailRepository)
        {
            _emailRepository = emailRepository;
        }

        public string Send(Email email)
        {
            try{
                var response = _emailRepository.Send(email);
                return response;
            }catch(Exception ex){
                throw new NotImplementedException(ex.Message);
            }
        }
    }
}