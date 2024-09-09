using wan24.Crypto.Tests;
using wan24.Tests;

namespace wan24_Crypto_NaCl_Tests
{
    [TestClass]
    public class Kdf_Tests : TestBase
    {
        [TestMethod]
        public void All_Tests() => KdfTests.TestAllAlgorithms();
    }
}
