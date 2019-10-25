using Castle.Core.Logging;
using Microsoft.Extensions.Logging;

namespace FunctionApp2
{
    public class Scoped1: IScoped1
    {
        public Scoped1(ILogger<Scoped1> logger)
        {

        }
    }
}