using Microsoft.Extensions.DependencyInjection;

namespace ChoiceA.Services
{
    public static class GroupServiceExtension
    {
        public static IServiceCollection AddGroupService(this IServiceCollection services)
        {
            return services.AddSingleton<IGroupService, GroupService>();
        }
    }
}
