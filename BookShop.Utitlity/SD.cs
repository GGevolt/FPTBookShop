using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPTBookShop.Utitlity
{
	public static class SD
	{
		public const string StatusPending = "Pending";
		public const string StatusApprove = "Approve";
		public const string StatusInprocess = "On working";
		public const string StatusShipped = "On the way";
		public const string StatusCancelled = "Cancelled";
		public const string StatusRefunds = "Has refunded";

		public const string PaymentStatusPending = "Pending";
		public const string PaymentStatusApprove = "Approve";
		public const string PaymentStatusDelayPayment = "Approve for delay payment";
		public const string PaymentStatusRejected = "Rejected";
	}
}
