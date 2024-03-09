using wan24.Core;

[assembly: Bootstrapper(typeof(wan24.Crypto.NaCl.Bootstrapper), nameof(wan24.Crypto.NaCl.Bootstrapper.Boot))]

namespace wan24.Crypto.NaCl
{
    /// <summary>
    /// Bootstrapper
    /// </summary>
    public static class Bootstrapper
    {
        /// <summary>
        /// Boot
        /// </summary>
        public static void Boot() => KdfHelper.Algorithms[KdfArgon2IdAlgorithm.ALGORITHM_NAME] = KdfArgon2IdAlgorithm.Instance;
    }
}
