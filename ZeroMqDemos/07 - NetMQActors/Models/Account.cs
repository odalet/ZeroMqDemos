namespace NetMQActors.Models
{
    public class Account
    {
        public Account(int id, string name, string sortCode, decimal balance)
        {
            Id = id;
            Name = name;
            SortCode = sortCode;
            Balance = balance;
        }

        public int Id { get; }
        public string Name { get; }
        public string SortCode { get; }
        public decimal Balance { get; set; }
    }
}
