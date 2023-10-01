namespace wan24.Crypto.NaCl
{
    /// <summary>
    /// NaCl helper
    /// </summary>
    public static class NaClHelper
    {
        /// <summary>
        /// Set the implemented algorithms as defaults
        /// </summary>
        /// <param name="useCurrentDefaultAsCounterAlgorithms">Use the current <c>wan24-Crypto</c> defaults as counter algorithms?</param>
        public static void SetDefaults(in bool useCurrentDefaultAsCounterAlgorithms = true)
        {
            if (useCurrentDefaultAsCounterAlgorithms) HybridAlgorithmHelper.KdfAlgorithm = KdfHelper.DefaultAlgorithm;
            KdfHelper.DefaultAlgorithm = KdfArgon2IdAlgorithm.Instance;
        }
    }
}
