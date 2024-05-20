using System;

namespace Abyss.StartupManager
{
    public class StartupManagerException : AggregateException
    {
        public StartupManagerException(string message, Exception inner) : base(message, inner)
        {
        }
        
        public StartupManagerException(string message) : base(message)
        {
        }
    }
}