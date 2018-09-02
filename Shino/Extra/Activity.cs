using Discord;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shino.Extra
{
    public class Activity : IActivity
    {
        public string name;
        public string Name { get { return name; } }

        public ActivityType type;
        public ActivityType Type { get { return type; } }

        public Activity(string name, ActivityType type)
        {
            this.name = name;
            this.type = type;
        }
    }
}
