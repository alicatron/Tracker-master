using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tracker.Models.TrackerModels
{
    public class UserRole
    {
        public UserRole() { }

        public UserRole(ApplicationRole role)
        {
            Id = role.Id;
            Name = role.Name;
        }

        public string Id { get; set; }
        public string Name { get; set; }
    }
}
