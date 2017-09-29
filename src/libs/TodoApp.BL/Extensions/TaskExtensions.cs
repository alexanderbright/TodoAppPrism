using System;
using System.Threading;
using System.Threading.Tasks;

namespace TodoApp.BL.Extensions
{
  public static class TaskExtensions
  {
    public static Task OnSuccess(this Task task, Action continuactionAction)
    {
      task.ContinueWith((t) => continuactionAction(),
        CancellationToken.None,
        TaskContinuationOptions.OnlyOnRanToCompletion | TaskContinuationOptions.AttachedToParent,
        TaskScheduler.FromCurrentSynchronizationContext());
      return task;
    }

    public static Task OnSuccess<T>(this Task<T> task, Action<T> continuactionAction)
    {
      task.ContinueWith(async (t) => continuactionAction(await t),
        CancellationToken.None,
        TaskContinuationOptions.OnlyOnRanToCompletion | TaskContinuationOptions.AttachedToParent,
        TaskScheduler.FromCurrentSynchronizationContext());
      return task;
    }

    public static Task<T> OnSuccess<T>(this Task<T> task, Func<T, T> continuactionAction)
    {
      task.ContinueWith(async (t) => continuactionAction(await t),
        CancellationToken.None,
        TaskContinuationOptions.OnlyOnRanToCompletion | TaskContinuationOptions.AttachedToParent,
        TaskScheduler.FromCurrentSynchronizationContext());
      return task;
    }

    public static Task OnError(this Task task, Action continuactionAction)
    {
      task.ContinueWith((t) => continuactionAction(),
        CancellationToken.None,
        TaskContinuationOptions.OnlyOnFaulted,
        TaskScheduler.FromCurrentSynchronizationContext());
      return task;
    }

    public static Task Then(this Task task, Action continuactionAction)
    {
      task.ContinueWith((t) => continuactionAction(),
        CancellationToken.None, TaskContinuationOptions.None,
        TaskScheduler.FromCurrentSynchronizationContext());
      return task;
    }
  }
}