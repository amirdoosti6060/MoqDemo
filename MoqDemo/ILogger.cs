namespace MoqDemo
{
    public interface ILogger
    {
        public bool IsVerbose { get; set; }
        public void Log(string message);
        public Task LogAsync (string message);
    }
}
