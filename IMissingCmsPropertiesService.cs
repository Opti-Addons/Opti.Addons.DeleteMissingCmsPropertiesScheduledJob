namespace OptimizelyDeleteMissingCmsProperties;

public interface IMissingCmsPropertiesService
{
    List<PropertyModel> ListPropertiesToBeRemoved();
    void Remove(List<PropertyModel> listOfPropertiesToRemove);
}