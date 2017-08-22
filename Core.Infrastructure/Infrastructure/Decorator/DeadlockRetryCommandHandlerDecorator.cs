using System;
using System.Data.Common;
using System.Threading;

namespace Infrastructure.Decorator
{
    public class DeadlockRetryCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
    {
        private readonly ICommandHandler<TCommand> decorated;

        public DeadlockRetryCommandHandlerDecorator(ICommandHandler<TCommand> decorated)
        {
            this.decorated = decorated;
        }

        public void Handle(TCommand command)
        {
            this.HandleWithCountDown(command, 5);
        }

        private void HandleWithCountDown(TCommand command, int count)
        {
            try
            {
                this.decorated.Handle(command);
            }
            catch (Exception ex)
            {
                if (count <= 0 || !IsDeadlockException(ex))
                    throw;

                Thread.Sleep(300);

                this.HandleWithCountDown(command, count - 1);
            }
        }

        private static bool IsDeadlockException(Exception ex)
        {
            while (ex != null)
            {
                if (ex is DbException && ex.Message.Contains("deadlock"))
                    return true;

                ex = ex.InnerException;
            }

            return false;
        }
    }
}