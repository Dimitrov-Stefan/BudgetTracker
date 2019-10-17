namespace Models.DatatableModels
{
    /// <summary>
    /// The additional columns that you can send to jQuery DataTables for automatic processing.
    /// </summary>
    public abstract class DTRow
    {
        /// <summary>
        /// Set the ID property of the dt-tag tr node to this value
        /// </summary>
        public virtual string DT_RowId => null;

        /// <summary>
        /// Add this class to the dt-tag tr node
        /// </summary>
        public virtual string DT_RowClass => null;

        /// <summary>
        /// Add this data property to the row's dt-tag tr node allowing abstract data to be added to the node, using the HTML5 data-* attributes.
        /// This uses the jQuery data() method to set the data, which can also then be used for later retrieval (for example on a click event).
        /// </summary>
        public virtual object DT_RowData => null;
    }
}
