namespace MoqDemo
{
    public class Calculator
    {
        private readonly ILogger _logger;

        public virtual event EventHandler? OnError;

        public Calculator(ILogger logger)
        {
            _logger = logger;
        }

        public int Add(int a, int b)
        {
            _logger.Log($"Add({a},{b})");
            return a + b;
        }

        public int Subtract(int a, int b)
        {
            //_logger.Log($"Subtract({a},{b})");
            return a - b;
        }

        public async Task<int> Multiply(int a, int b)
        {
            await _logger.LogAsync($"Myltiply({a},{b})");
            return a * b;
        }

        public int Devide(int a, int b)
        {
            _logger.Log($"Devide({a},{b})");

            if (b == 0)
            {
                if(OnError != null)
                    OnError(this, new EventArgs());
                return 0;
            }

            return a / b;
        }
    }
}
