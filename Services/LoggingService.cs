using System;
using System.IO;
using System.Threading.Tasks;

namespace GESTION_DES_STOCKS.Services
{
    public interface ILoggingService
    {
        void LogInformation(string message);
        void LogWarning(string message);
        void LogError(string message, Exception? exception = null);
        void LogDebug(string message);
        Task LogAsync(string level, string message, Exception? exception = null);
    }

    public class LoggingService : ILoggingService
    {
        private readonly string _logFilePath;
        private readonly object _lockObject = new object();

        public LoggingService()
        {
            var logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
            Directory.CreateDirectory(logDirectory);
            _logFilePath = Path.Combine(logDirectory, $"stockges_{DateTime.Now:yyyyMMdd}.log");
        }

        public void LogInformation(string message)
        {
            LogAsync("INFO", message).ConfigureAwait(false);
        }

        public void LogWarning(string message)
        {
            LogAsync("WARN", message).ConfigureAwait(false);
        }

        public void LogError(string message, Exception? exception = null)
        {
            LogAsync("ERROR", message, exception).ConfigureAwait(false);
        }

        public void LogDebug(string message)
        {
            LogAsync("DEBUG", message).ConfigureAwait(false);
        }

        public async Task LogAsync(string level, string message, Exception? exception = null)
        {
            try
            {
                var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                var logEntry = $"[{timestamp}] [{level}] {message}";

                if (exception != null)
                {
                    logEntry += $"\nException: {exception.Message}\nStackTrace: {exception.StackTrace}";
                }

                logEntry += Environment.NewLine;

                await File.AppendAllTextAsync(_logFilePath, logEntry);
                
                // Aussi afficher dans la console de débogage
                System.Diagnostics.Debug.WriteLine(logEntry);
            }
            catch (Exception ex)
            {
                // En cas d'erreur de logging, afficher dans la console
                System.Diagnostics.Debug.WriteLine($"Erreur de logging: {ex.Message}");
            }
        }

        public void LogUserAction(string username, string action, string details = "")
        {
            var message = $"User: {username} | Action: {action}";
            if (!string.IsNullOrEmpty(details))
            {
                message += $" | Details: {details}";
            }
            LogInformation(message);
        }

        public void LogProductOperation(string operation, string productName, string username)
        {
            LogInformation($"Product {operation}: {productName} by user {username}");
        }

        public void LogStockMovement(string productName, int quantity, string movementType, string username)
        {
            LogInformation($"Stock movement: {productName} | Quantity: {quantity} | Type: {movementType} | User: {username}");
        }

        public void LogAuthentication(string username, bool success, string ipAddress = "")
        {
            var status = success ? "SUCCESS" : "FAILED";
            var message = $"Authentication {status}: {username}";
            if (!string.IsNullOrEmpty(ipAddress))
            {
                message += $" | IP: {ipAddress}";
            }
            
            if (success)
                LogInformation(message);
            else
                LogWarning(message);
        }
    }
} 