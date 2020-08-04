﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChoiceA.Services
{
    public interface ITimeService
    {
        DateTime Time { get; }
    }

    public class TimeService : ITimeService
    {
        public DateTime Time { private set; get; }

        public TimeService()
        {
            Time = DateTime.Now;
        }
    }

    public static class TimeServiseExtention
    {
        public static IServiceCollection AddTime(this IServiceCollection services)
            => services.AddTransient<ITimeService, TimeService>();
    }
}