using System.Threading.Tasks;

namespace ChatApp.Client.Services;

public interface IDialogService
{
    Task<TResult> ShowDialog<TResult>() where TResult : DialogData;
    Task ShowDialog<TResult>(TResult dialogData) where TResult : DialogData;
}
