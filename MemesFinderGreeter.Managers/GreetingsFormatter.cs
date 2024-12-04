using MemesFinderGreeter.Interfaces;
using MemesFinderGreeter.Models;
using System.Text;

namespace MemesFinderGreeter.Managers
{
    public class GreetingsFormatter : IGreetingsFormatter
    {
        public string FormatGreetingMessage<T>(string template, T chatMember, IEnumerable<AdminInfo> adminUsernames)
        {
            var greetingsBuilder = new StringBuilder(template);
            var chatMemberFields = typeof(T).GetProperties();

            foreach (var field in chatMemberFields)
                greetingsBuilder.Replace($"{{{field.Name}}}", field?.GetValue(chatMember)?.ToString());

            greetingsBuilder.AppendLine("\n\n");
            foreach (var admin in adminUsernames)
            {
                string userRepresentation = admin.Username != null ? $"@{admin.Username} " : $"[{admin.FirstName}](tg://user?id={admin.ID}) ";
                greetingsBuilder.AppendLine(userRepresentation);
            }

            return greetingsBuilder.ToString();
        }
    }
}

