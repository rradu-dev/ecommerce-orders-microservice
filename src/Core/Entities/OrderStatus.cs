namespace Ecommerce.Services.Orders.Core.Entities
{
	public enum OrderStatus
	{
		Created,
		PaymentPending,
		PaymentFailed,
		PaymentCompleted,
		Processing,
		AwaitingShipment,
		InTransit,
		Shipped,
		Cancelled,
		Declined,
		Completed,
		Refunded
	}
}
