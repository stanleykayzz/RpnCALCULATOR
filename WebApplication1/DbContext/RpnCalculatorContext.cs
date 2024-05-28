
using WebApplication1.Models;

namespace WebApplication1.DbContext
{
    public class RpnCalculatorContext
    {
        public RpnCalculator RpnCalculator { get; set; }

        public RpnCalculatorContext()
        {
            RpnCalculator = new RpnCalculator();
        }
    }
}
