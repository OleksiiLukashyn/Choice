using ChoiceA.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace ChoiceA.Services
{
    public class GroupService : IGroupService
    {
        private const string GroupsDataFileName = "groups.json";

        public List<string> Groups { get; private set; }

        public GroupService()
        {
            var jsonString = File.ReadAllText(GroupsDataFileName);
            var myJsonObject = JsonConvert.DeserializeObject<GroupsFileJsonModel>(jsonString);
            Groups = myJsonObject.Groups;
        }
    }
}

