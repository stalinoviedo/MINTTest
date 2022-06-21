using System;
using System.Collections.Generic;

namespace MINTTest
{
    public class SourceData
    {
        public string categoryName { get; set; }
        public string eventName { get; set; }
        public List<SlotGroup> slotGroups { get; set; }
    }

    public class Resource
    {
        public string firstName { get; set; }
        public string name { get; set; }
        public string fullName { get { return firstName + " " + name; } }
        public string userId { get; set; }
        public string photo { get; set; }
        public List<string> certificates { get; set; }
    }

    public class SlotGroup
    {
        public string slotGroupName { get; set; }
        public List<Resource> resources { get; set; }
    }
}
