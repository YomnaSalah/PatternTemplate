namespace PatternTemplate.Tables
{
    /// <summary>
    /// Abstract base class implementing common entity properties for auditing and soft deletion.
    /// </summary>
    public abstract class BaseEntity : IBaseEntity
    {
        /// <inheritdoc/>
        public bool IsDeleted { get; set; }

        /// <inheritdoc/>
        public string? DeletedReason { get; set; }

        /// <inheritdoc/>
        public long? AddedById { get; set; }

        /// <inheritdoc/>
        public DateTime CreatedDate { get; set; }

        /// <inheritdoc/>
        public long? LastModifiedById { get; set; }

        /// <inheritdoc/>
        public DateTime? LastModifiedDate { get; set; }

        /// <inheritdoc/>
        public long? DeletedById { get; set; }

        /// <inheritdoc/>
        public DateTime? DeletedDate { get; set; }
    }
}
