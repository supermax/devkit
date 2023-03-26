using System;

namespace DevKit.Serialization.Json.API
{
    internal interface ILoggable
    {
        /// <summary>
        /// Gets or sets a value indicating whether [is debug mode].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [is debug mode]; otherwise, <c>false</c>.
        /// </value>
        bool IsDebugMode { get; set; }

        /// <summary>
        /// Writes given message to debug log
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        void Log(string format, params object[] args);

        /// <summary>
        /// Writes given message to debug log
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        void LogError(string format, params object[] args);

        /// <summary>
        /// Writes given message to debug log
        /// </summary>
        /// <param name="ex">The exception.</param>
        void LogError(Exception ex);
    }
}
