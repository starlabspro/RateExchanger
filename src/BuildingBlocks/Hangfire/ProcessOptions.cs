namespace BuildingBlocks.Hangfire
{
    public class ProcessOptions
    {
        public bool Enabled { get; set; }

        public string Name { get; set; }

        public string CronInterval { get; set; }
    }
}
