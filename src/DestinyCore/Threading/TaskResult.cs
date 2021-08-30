using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DestinyCore.Threading
{
  
    public struct TaskResult<T>
    {
        public T Ok { get; } 
        public Exception Error { get; } 

        public bool IsFailed => Error != null;
        public bool IsOk => !IsFailed;

        public TaskResult(T ok)
        {
            Ok = ok;
            Error = default;
        }

        public TaskResult(Exception error) 
        {
            Error = error;
            Ok = default;
        }

        public R Match<R>(Func<T, R> okMap, Func<Exception, R> failureMap)
        {
            return IsOk ? okMap(Ok) : failureMap(Error); 
        }

        public void Match(Action<T> okAction, Action<Exception> errorAction)
        {
            if (IsOk) okAction(Ok);
            else errorAction(Error); 
        }

        public static implicit operator TaskResult<T>(T ok)
        {
            return new TaskResult<T>(ok); 
        }

        public static implicit operator TaskResult<T>(Exception error)
        {
            return new TaskResult<T>(error);
        }

        public static implicit operator TaskResult<T>(TaskResult.Ok<T> ok)
        {
            return new TaskResult<T>(ok.Value); 
        }

        public static implicit operator TaskResult<T>(TaskResult.Failure error)
        {
            return new TaskResult<T>(error.Error); 
        }
    }

    public static class TaskResult
    {
        public struct Ok<L>
        {
            internal L Value { get; }

            internal Ok(L value)
            {
                Value = value;
            }
        }

        public struct Failure
        {
            internal Exception Error { get; }

            internal Failure(Exception error)
            {
                Error = error;
            }
        }
    }

}
