namespace PatternTemplate.Tables
{
    /// <summary>
    /// Interface for base entity properties used for auditing and soft deletion.
    /// </summary>
    public interface IBaseEntity
    {
        /// <summary>
        /// The date and time the entity was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Indicates whether the entity is soft-deleted.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// The reason for deletion, if applicable.
        /// </summary>
        public string? DeletedReason { get; set; }

        /// <summary>
        /// The user ID who added the entity.
        /// </summary>
        public long? AddedById { get; set; }

        /// <summary>
        /// The user ID who last modified the entity.
        /// </summary>
        public long? LastModifiedById { get; set; }

        /// <summary>
        /// The date and time the entity was last modified.
        /// </summary>
        public DateTime? LastModifiedDate { get; set; }

        /// <summary>
        /// The user ID who deleted the entity.
        /// </summary>
        public long? DeletedById { get; set; }

        /// <summary>
        /// The date and time the entity was deleted.
        /// </summary>
        public DateTime? DeletedDate { get; set; }
    }
}
