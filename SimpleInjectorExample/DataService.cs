using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleInjectorExample
{
    public interface IDataService
    {
        void SaveData(string data);
    }

    public class DataService : IDataService
    {
        public void SaveData(string data)
        {
            Console.WriteLine($"Data saved: {data}");
        }
    }
    public interface ILogger
    {
    }

    public class Logger : ILogger
    {
        public Logger() { }
    }

    public interface IRepository
    {
    }

    public class Repository : IRepository
    {
        public Repository(ILogger logger) { }
    }

    public interface IService
    {
    }

    public class Service : IService
    {
        public Service(IRepository repository) { }
    }

    public interface IEmailService
    {
    }

    public class EmailService : IEmailService
    {
        public EmailService() { }
    }

    public interface ILoggingService
    {
    }

    public class LoggingService : ILoggingService
    {
        public LoggingService() { }
    }

    public interface IUserRepository
    {
    }

    public class UserRepository : IUserRepository
    {
        public UserRepository() { }
    }

    public interface IUserService
    {
        void RegisterUser(string email, string password);
    }

    public class UserService : IUserService
    {
        private readonly IEmailService _emailService;
        private readonly ILoggingService _loggingService;
        private readonly IUserRepository _userRepository;

        public UserService(IEmailService emailService, ILoggingService loggingService, IUserRepository userRepository)
        {
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _loggingService = loggingService ?? throw new ArgumentNullException(nameof(loggingService));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public void RegisterUser(string email, string password)
        {
            _userRepository.ToString();
            _loggingService.ToString();
            _emailService.ToString();
            Console.WriteLine($"User {email} registered.");
        }
    }
}
