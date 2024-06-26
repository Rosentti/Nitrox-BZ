﻿using NitroxModel.Logger;

namespace NitroxModel_BelowZero.Logger
{
    /// <summary>
    ///     Log handler for logging and showing information to the player.
    ///     The log is also written to the log file.
    /// </summary>
    public class BelowZeroInGameLogger : InGameLogger
    {
        public void Log(object message) => Log(message?.ToString());
        public void Log(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return;
            }
            // Logs to the top-left of the screen using UWE code.
            ErrorMessage.AddMessage(message);
        }
    }
}
