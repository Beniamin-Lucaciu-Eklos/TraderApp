namespace TraderApp.Domain.Models
{
    public class Account : DomainObject
    {
        public User User { get; set; }

        public decimal Balance { get; set; }

        public ICollection<AssetTransaction> AssetTransactions { get; set; }

    }
}
