using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace ChoiceA.Services
{
    public interface IGroupService
    {
        List<string> Groups { get; }
    }

    class MyJsonType
    {
        public List<string> Groups { get; set; }
    }

    public class GroupService : IGroupService
    {
        private const string GroupsDataFileName = "groups.json";

        public List<string> Groups { get; private set; }

        public GroupService()
        {
            var jsonString = File.ReadAllText(GroupsDataFileName);
            var myJsonObject = JsonConvert.DeserializeObject<MyJsonType>(jsonString);
            Groups = myJsonObject.Groups;
        }
    }

    public static class GroupServiceExtension
    {
        public static IServiceCollection AddGroupService(this IServiceCollection services)
        {
            return services.AddSingleton<IGroupService, GroupService>();
        }
    }
}

