namespace BeautyLand.Application.Services.Site.Orders.Dtos
{
    public enum PaymentStatus
    {
        /// <summary>
        /// پرداخت نشده
        /// </summary>
        UnPaid = 0,

        /// <summary>
        /// درخواست پرداخت  
        /// </summary>
        RequestPayment = 1,

        /// <summary>
        /// پرداخت شده است
        /// </summary>
        Paid = 2,
    }

}
