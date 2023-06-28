namespace Common
{
    public class AkkaOptions
    {
        public string ActorName { get; set; }
        public int MaximumSaveParallelism { get; set; }
        public int MaxDeliveryCount { get; set; }
        public AkkaThrottle Throttle { get; set; }
    }

    public class AkkaThrottle
    {
        public int Elements { get; set; }
        public int IntervalInMillis { get; set; }
        public int MaximumBurst { get; set; }
    }
}
