using Microsoft.Extensions.Logging;
using wan24.Core;
using wan24.Crypto.Tests;

namespace wan24_Crypto_NaCl_Tests
{
    [TestClass]
    public class A_Initialization
    {
        public static ILoggerFactory LoggerFactory { get; private set; } = null!;

        [AssemblyInitialize]
        public static void Init(TestContext tc)
        {
            wan24.Tests.TestsInitialization.Init(tc);
            SharedTests.Initialize();
            Logging.WriteInfo("wan24-Crypto-NaCl Tests initialized");
        }
    }
}
