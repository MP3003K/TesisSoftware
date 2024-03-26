using angular.Server.Filters;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Filters;

public class TransactionFilter : ActionFilterAttribute
{
    private readonly IDatabaseTransaction _databaseTransaction;

    public TransactionFilter(IDatabaseTransaction databaseTransaction)
    {
        _databaseTransaction = databaseTransaction;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        _databaseTransaction.BeginTransaction();
    }

    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception == null && context.ModelState.IsValid)
        {
            _databaseTransaction.CommitTransactionAsync();
        }
        else
        {
            _databaseTransaction.RollbackTransactionAsync();
        }
    }
}