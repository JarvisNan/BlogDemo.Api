using System.Collections.Generic;

namespace BlogDemo.DB.Services
{
    public interface IPropertyMapping
    {
        Dictionary<string, List<MappedProperty>> MappingDictionary { get; }
    }
}