using UnityEngine;

// I'm using this to ease the debug with the AndroidDeviceMonitor
// As there is no filter to only get Logs, I've done a filter with "Log : "
public class Logger
{
   static string _logHeader = "Log : ";

   public static void Log(string message)
   {
        Debug.Log(_logHeader + message);
   }
}
