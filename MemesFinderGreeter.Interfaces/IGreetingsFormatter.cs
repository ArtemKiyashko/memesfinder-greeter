using MemesFinderGreeter.Models;

namespace MemesFinderGreeter.Interfaces
{
    public interface IGreetingsFormatter
    {
        public string FormatGreetingMessage<T>(string template, T chatMember, IEnumerable<AdminInfo> adminUsernames);
    }
}

