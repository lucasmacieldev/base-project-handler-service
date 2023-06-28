namespace Common
{
    public static class BrokerQueueName
    {
        public const string VoucherOrderOperationCommand = "client.cmd";
        public const string VoucherOrderOperationEvent = "uber.evt.voucher-order-operation";
        public const string UpdateVoucherResultSaverName = "uber.evt.voucher-order-saver";
        public const string UpdateVoucherResultMSHName = "uber.evt.voucher-order-msh";
    }
}
