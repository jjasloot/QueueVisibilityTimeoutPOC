using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;

namespace QueueVisibilityTimeoutPOC
{
    public class Functions
    {
        public static void ProcessQueueMessage([QueueTrigger("queue")] string message, DateTimeOffset NextVisibleTime, ILogger logger)
        {
            logger.LogInformation(message);
            logger.LogInformation("Now:" + DateTime.UtcNow + " NextVisibleTime: " + NextVisibleTime.ToString());
        }
    }
}
