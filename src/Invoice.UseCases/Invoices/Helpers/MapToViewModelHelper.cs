using Invoice.CoreBusiness;

namespace Invoice.UseCases.Invoices.Helpers
{
    public static class MapToViewModelHelper
    {
        public static string ToViewModelString(this ProcessingStatus processingStatus)
        {
            return processingStatus switch
            {
                ProcessingStatus.New => "Новый",
                ProcessingStatus.Paid => "Оплачен",
                ProcessingStatus.Canceled => "Отменен",
                _ => "",
            };
        }

        public static string ToViewModelString(this PaymentMethod paymentMethod)
        {
            return paymentMethod switch
            {
                PaymentMethod.CreditCard => "Кредитная карта",
                PaymentMethod.DebitCard => "Дебетовая карта",
                PaymentMethod.ElectronicCheck => "Электронный чек",
                _ => "",
            };
        }
    }
}
