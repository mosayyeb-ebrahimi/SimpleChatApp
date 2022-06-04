namespace ChatApp.Client.Services;

public interface IDialogIconProvider
{
    IconData? Provide(string? name);
}
