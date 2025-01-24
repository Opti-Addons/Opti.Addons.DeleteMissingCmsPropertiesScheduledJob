namespace OptimizelyDeleteMissingCmsProperties;

[ScheduledPlugIn(DisplayName = "[Opti.Addons] Delete missing CMS data: List",
    Description = "Lists properties that are not present in the CMS model classes",
    GUID = "1CFC10C9-0524-46C0-97B0-6ED8A141EDA2")]
public class ListMissingCmsPropertiesScheduledJob : ScheduledJobBase
{
    private readonly IMissingCmsPropertiesService _missingCmsPropertiesService;

    public ListMissingCmsPropertiesScheduledJob(IMissingCmsPropertiesService missingCmsPropertiesService)
    {
        _missingCmsPropertiesService = missingCmsPropertiesService ?? throw new ArgumentNullException(nameof(missingCmsPropertiesService));

        IsStoppable = true;
    }

    public override string Execute()
    {
        var listPropertiesToBeRemoved = _missingCmsPropertiesService.ListPropertiesToBeRemoved()
                                                                    .OrderBy(p => p.Type.Name)
                                                                    .ThenBy(p => p.Name())
                                                                    .ToList();

        if (listPropertiesToBeRemoved.Count == 0) return "No properties to be removed";

        var sb = new StringBuilder();
        sb.AppendLine("Properties to be removed: (type.name)");
        listPropertiesToBeRemoved.ForEach(p => sb.Append($"{p.Type.Name}.{p.Name()}<br>"));

        return sb.ToString();
    }
}