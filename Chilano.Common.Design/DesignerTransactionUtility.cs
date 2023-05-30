using System.ComponentModel.Design;

namespace Chilano.Common.Design;

public abstract class DesignerTransactionUtility
{
    public static object DoInTransaction(IDesignerHost theHost, string theTransactionName, TransactionAwareParammedMethod theMethod, object theParam)
    {
        DesignerTransaction designerTransaction = null;
        object result = null;
        try
        {
            designerTransaction = theHost.CreateTransaction(theTransactionName);
            result = theMethod(theHost, theParam);
        }
        catch (CheckoutException ex)
        {
            if (ex != CheckoutException.Canceled)
            {
                throw ex;
            }
        }
        catch
        {
            if (designerTransaction != null)
            {
                designerTransaction.Cancel();
                designerTransaction = null;
            }
            throw;
        }
        finally
        {
            designerTransaction?.Commit();
        }
        return result;
    }
}
