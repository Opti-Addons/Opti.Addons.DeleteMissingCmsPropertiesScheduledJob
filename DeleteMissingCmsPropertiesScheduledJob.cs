namespace OptimizelyDeleteMissingCmsProperties;

[ScheduledPlugIn(DisplayName = "[Opti.Addons] Delete missing CMS data: Delete",
    Description = "Removes properties that are not present in the model class(es). ⚠️ Warning: Please make sure to backup your database before running this job. " +
                  "You may also run the 'list' job first to see what properties will be removed.",
    GUID = "96F1B6FB-871C-40E9-9207-8DA0734B7F5F")]
public class DeleteMissingCmsPropertiesScheduledJob : ScheduledJobBase
{
    private readonly IMissingCmsPropertiesService _missingCmsPropertiesService;

    public DeleteMissingCmsPropertiesScheduledJob(IMissingCmsPropertiesService missingCmsPropertiesService)
    {
        _missingCmsPropertiesService = missingCmsPropertiesService ?? throw new ArgumentNullException(nameof(missingCmsPropertiesService));

        IsStoppable = true;
    }

    public override string Execute()
    {
        var listPropertiesToBeRemoved = _missingCmsPropertiesService.ListPropertiesToBeRemoved();

        if (!listPropertiesToBeRemoved.Any()) return "No properties to be removed";

        _missingCmsPropertiesService.Remove(listPropertiesToBeRemoved);

        var sb = new StringBuilder();
        sb.AppendLine("Properties removed: (type.name)");
        listPropertiesToBeRemoved.ForEach(p => sb.Append($"{p.Type.Name}.{p.Name()}<br>"));

        return sb.ToString();
    }
}