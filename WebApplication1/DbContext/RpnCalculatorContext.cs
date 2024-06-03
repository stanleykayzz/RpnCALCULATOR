using WebApplication1.Models;

namespace WebApplication1.DbContext
{
    public class RpnCalculatorContext
    {
        public WebApplication1.Models.RpnCalculator RpnCalculator { get; set; }


        public RpnCalculatorContext()
        {
            RpnCalculator = new WebApplication1.Models.RpnCalculator();
        }
    }
}
