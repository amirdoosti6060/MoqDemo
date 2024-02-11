namespace MoqDemo
{
    public class MyLogger : ILogger
    {
        private bool verbose = false;

        public bool IsVerbose { get => verbose; set => verbose = value; }

        public void Log(string message)
        {
            Console.WriteLine($"{(verbose? "[" + DateTime.Now.ToString("HH:mm:ss:fff") + "]: " : "")}{message}");
        }

        public async Task LogAsync(string message)
        {
            await Task.Run(() => Console.WriteLine($"{(verbose ? "[" + DateTime.Now.ToString("HH:mm:ss:fff") + "]: " : "")}{message}"));
        }
    }
}
