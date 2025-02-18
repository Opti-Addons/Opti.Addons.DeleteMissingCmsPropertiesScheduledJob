namespace OptimizelyDeleteMissingCmsProperties;

public class MissingCmsPropertiesService : IMissingCmsPropertiesService
{
    private readonly IContentTypeRepository _contentTypeRepository;
    private readonly IPropertyDefinitionRepository _propertyDefinitionRepository;

    private readonly List<Type> _allowedTypes = new()
    {
        typeof(PageData),
        typeof(BlockData),
        typeof(MediaData),
        typeof(ContentFolder)
    };
    
    private readonly List<string> _excludedBaseTypes = new()
    {
        "EPiServer.Forms.Core.BlockBase" 
    };

    public MissingCmsPropertiesService(IContentTypeRepository contentTypeRepository,
                                       IPropertyDefinitionRepository propertyDefinitionRepository)
    {
        _contentTypeRepository = contentTypeRepository ?? throw new ArgumentNullException(nameof(contentTypeRepository));
        _propertyDefinitionRepository = propertyDefinitionRepository ?? throw new ArgumentNullException(nameof(propertyDefinitionRepository));
    }

    public List<PropertyModel> ListPropertiesToBeRemoved()
    {
        var propsToRemove = new List<PropertyModel>();
        var allTypes = _contentTypeRepository.List().ToList();

        var types = allTypes.Where(t => _allowedTypes.Any(x => x.IsAssignableFrom(t.ModelType)))
                            .Where(t => !_excludedBaseTypes.Any(x => DoesInheritFrom(t.ModelType, x)))
                            .ToList();

        foreach (var contentType in types)
        {
            var objectProps = contentType.ModelType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var pd in contentType.PropertyDefinitions)
            {
                if (objectProps.All(p => p.Name != pd.Name)) propsToRemove.Add(new PropertyModel(contentType.ModelType, pd));
            }
        }

        return propsToRemove;
    }

    public void Remove(List<PropertyModel> listOfPropertiesToRemove)
    { 
        foreach (var property in listOfPropertiesToRemove)
        {
            var propertyDefinitionType = _propertyDefinitionRepository.Load(property.PropertyDefinition.ID);
            
            var propertyToRemove = propertyDefinitionType.CreateWritableClone();
            
            _propertyDefinitionRepository.Delete(propertyToRemove);
        }
    }
    
    private static bool DoesInheritFrom(Type type, string baseType)
    {
        if (type == null) return false;
        if (string.Equals(type.FullName, baseType, StringComparison.OrdinalIgnoreCase)) return true;
        return DoesInheritFrom(type.BaseType, baseType);
    }
}