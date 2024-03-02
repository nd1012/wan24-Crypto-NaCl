using System.ComponentModel.DataAnnotations;
using wan24.Core;

namespace wan24.Crypto.NaCl
{
    /// <summary>
    /// NaCl app configuration (<see cref="AppConfig"/> ; should be applied AFTER bootstrapping (<see cref="AppConfigAttribute.AfterBootstrap"/>))
    /// </summary>
    public class NaClCryptoAppConfig : AppConfigBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public NaClCryptoAppConfig() : base() { }

        /// <summary>
        /// Applied TPM crypto app configuration
        /// </summary>
        public NaClCryptoAppConfig? AppliedNaClCryptoConfig { get; protected set; }

        /// <summary>
        /// Default degree of parallelism
        /// </summary>
        [Range(KdfArgon2IdAlgorithm.DEFAULT_PARALLELISM, KdfArgon2IdAlgorithm.MAX_PARALLELISM)]
        public int? DefaultParallelism { get; set; }

        /// <summary>
        /// Default memory limit in bytes
        /// </summary>
        [Range(KdfArgon2IdAlgorithm.DEFAULT_MEMORY, KdfArgon2IdAlgorithm.MAX_MEMORY)]
        public long? DefaultMemoryLimit { get; set; }

        /// <inheritdoc/>
        public override void Apply()
        {
            if (SetApplied)
            {
                if (AppliedNaClCryptoConfig is not null) throw new InvalidOperationException();
                AppliedNaClCryptoConfig = this;
            }
            if (DefaultParallelism.HasValue) KdfArgon2IdOptions.DefaultParallelism = DefaultParallelism.Value;
            if (DefaultMemoryLimit.HasValue) KdfArgon2IdOptions.DefaultMemoryLimit = DefaultMemoryLimit.Value;
            ApplyProperties(afterBootstrap: false);
            ApplyProperties(afterBootstrap: true);
        }

        /// <inheritdoc/>
        public override async Task ApplyAsync(CancellationToken cancellationToken = default)
        {
            if (SetApplied)
            {
                if (AppliedNaClCryptoConfig is not null) throw new InvalidOperationException();
                AppliedNaClCryptoConfig = this;
            }
            if (DefaultParallelism.HasValue) KdfArgon2IdOptions.DefaultParallelism = DefaultParallelism.Value;
            if (DefaultMemoryLimit.HasValue) KdfArgon2IdOptions.DefaultMemoryLimit = DefaultMemoryLimit.Value;
            await ApplyPropertiesAsync(afterBootstrap: false, cancellationToken).DynamicContext();
            await ApplyPropertiesAsync(afterBootstrap: true, cancellationToken).DynamicContext();
        }
    }
}
