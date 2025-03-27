using System.Collections.Generic;

namespace DevKit.Entities
{
    public class EntityPropertiesContainer : Dictionary<string, PropertyValueHolder>
    {
        public EntityPropertiesContainer() : base() { }
        public EntityPropertiesContainer(Dictionary<string, PropertyValueHolder> dictionary) : base(dictionary) { }
    }
}
