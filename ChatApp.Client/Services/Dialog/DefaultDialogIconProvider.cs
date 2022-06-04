namespace ChatApp.Client.Services;

public class DefaultDialogIconProvider : IDialogIconProvider
{
    public IconData? Provide(string? name)
    {
        if (string.IsNullOrWhiteSpace(name)) return default;

        return new IconData
        {
            Icon = App.Current.TryFindResource($"{name}Geometry"),
            Color = App.Current.TryFindResource($"{name}GeometryColor")
        };
    }
}
