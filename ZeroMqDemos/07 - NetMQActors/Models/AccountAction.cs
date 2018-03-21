namespace NetMQActors.Models
{
    public enum TransactionType { Debit = 1, Credit = 2 }

    public class AccountAction
    {
        public AccountAction(TransactionType transactionType, decimal amount)
        {
            TransactionType = transactionType;
            Amount = amount;
        }

        public TransactionType TransactionType { get; }
        public decimal Amount { get; }
    }
}
