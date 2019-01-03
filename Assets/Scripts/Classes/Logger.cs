using UnityEngine;

// I'm using this to ease the debug with the AndroidDeviceMonitor
public class Logger
{
   static string _logHeader = "Log : ";

   public static void Log(string message)
   {
        Debug.Log(_logHeader + message);
   }
}
