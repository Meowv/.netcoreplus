using Plus.Domain.Entities;

namespace Plus.Domain.Uow
{
    /// <summary>
    /// Standard filters of Plus.
    /// </summary>
    public static class PlusDataFilters
    {
        /// <summary>
        /// "SoftDelete".
        /// Soft delete filter.
        /// Prevents getting deleted data from database.
        /// See <see cref="ISoftDelete"/> interface.
        /// </summary>
        public const string SoftDelete = "SoftDelete";
    }
}