#region

using System;

#endregion

namespace Navy.Test.Assertion
{
    /// <summary>
    ///     Conformity Type
    /// </summary>
    [Flags]
    public enum ConformityType
    {
        /// <summary>
        ///     Full Conformity
        /// </summary>
        Full = 0x1,

        /// <summary>
        ///     Expected Conformity
        /// </summary>
        Expected = 0x2,

        /// <summary>
        ///     Actual Conformity
        /// </summary>
        Actual = 0x4,

        /// <summary>
        ///     Until first error is null
        /// </summary>
        None = 0x0
    }
}