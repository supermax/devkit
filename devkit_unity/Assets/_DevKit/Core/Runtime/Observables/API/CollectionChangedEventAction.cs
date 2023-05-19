namespace DevKit.Core.Observables.API
{
    public enum CollectionChangedEventAction
    {
        /// <summary>
        /// New item(s) added
        /// </summary>
        Add,

        /// <summary>
        /// Item(s) removed
        /// </summary>
        Remove,

        /// <summary>
        /// Item(s) replaced
        /// </summary>
        Replace,

        /// <summary>
        /// The collection was reset (cleaned or item(s) range added)
        /// </summary>
        Reset,
    }
}
