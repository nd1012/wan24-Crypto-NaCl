using wan24.Core;

namespace wan24.Crypto.NaCl
{
    /// <summary>
    /// Argon2id KDF algorithm options
    /// </summary>
    public sealed class KdfArgon2IdOptions
    {
        /// <summary>
        /// Default degree of parallelism
        /// </summary>
        private int _Parallelism = KdfArgon2IdAlgorithm.DEFAULT_PARALLELISM;
        /// <summary>
        /// Default memory limit
        /// </summary>
        private long _MemoryLimit = KdfArgon2IdAlgorithm.DEFAULT_MEMORY;

        /// <summary>
        /// Constructor
        /// </summary>
        public KdfArgon2IdOptions() { }

        /// <summary>
        /// Degree of parallelism
        /// </summary>
        public int Parallelism
        {
            get => _Parallelism;
            set
            {
                if (value < KdfArgon2IdAlgorithm.DEFAULT_PARALLELISM || value > KdfArgon2IdAlgorithm.MAX_PARALLELISM) throw new ArgumentOutOfRangeException(nameof(value));
                _Parallelism = value;
            }
        }

        /// <summary>
        /// Memory limit
        /// </summary>
        public long MemoryLimit
        {
            get => _MemoryLimit;
            set
            {
                if (value < KdfArgon2IdAlgorithm.DEFAULT_MEMORY || value > KdfArgon2IdAlgorithm.MAX_MEMORY) throw new ArgumentOutOfRangeException(nameof(value));
                _MemoryLimit = value;
            }
        }

        /// <summary>
        /// Cast as JSON string
        /// </summary>
        /// <param name="options">Options</param>
        public static implicit operator string(KdfArgon2IdOptions options) => options.ToJson();

        /// <summary>
        /// Cast as options
        /// </summary>
        /// <param name="json">JSON string</param>
        public static implicit operator KdfArgon2IdOptions?(string? json)
            => json is null ? null : json.DecodeJson<KdfArgon2IdOptions>() ?? throw new InvalidDataException("Invalid JSON data");
    }
}
