namespace Packt.Shared{
    public class BankAccount
    {
        public string AccountName;          // instance member
        public decimal Balance;             // instance memeber
        public static decimal InterestRate; // shared member
        // each instance of BankAccount will have their own AccountName and Balance
        // but they will all have the same InterestRate
    }
}