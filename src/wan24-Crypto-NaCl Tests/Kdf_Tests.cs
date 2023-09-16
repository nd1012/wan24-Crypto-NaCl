using wan24.Crypto.Tests;

namespace wan24_Crypto_NaCl_Tests
{
    [TestClass]
    public class Kdf_Tests
    {
        [TestMethod]
        public void All_Tests() => KdfTests.TestAllAlgorithms();
    }
}
