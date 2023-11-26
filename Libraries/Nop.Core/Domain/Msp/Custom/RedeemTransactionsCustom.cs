
namespace Nop.Core.Domain.Msp
{
    public class RedeemTransactionsCustom //JK 20180912 MSP-96
    {
        public int CustomerID { get; set; }

        public string Username { get; set; }
        
        public string WalletID { get; set; }
        
        public decimal AvailableBalance { get; set; }

        public decimal RedeemWalletBalance { get; set; }
    }
}
