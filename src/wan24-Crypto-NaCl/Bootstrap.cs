using wan24.Core;

[assembly: Bootstrapper(typeof(wan24.Crypto.NaCl.Bootstrap), nameof(wan24.Crypto.NaCl.Bootstrap.Boot))]

namespace wan24.Crypto.NaCl
{
    /// <summary>
    /// Bootstrapper
    /// </summary>
    public static class Bootstrap
    {
        /// <summary>
        /// Boot
        /// </summary>
        public static void Boot() => KdfHelper.Algorithms[KdfArgon2IdAlgorithm.ALGORITHM_NAME] = KdfArgon2IdAlgorithm.Instance;
    }
}
