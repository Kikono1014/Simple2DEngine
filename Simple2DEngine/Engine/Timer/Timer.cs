using WM;
using System;
using System.Timers;

namespace Engine2D
{
  public partial class Engine : WindowManager 
  {
    private static readonly Dictionary<string, System.Timers.Timer> _timers = [];

    public static System.Timers.Timer? GetTimer(string name)
    {
      System.Timers.Timer? timer;
      if (_timers.TryGetValue(name, out timer))
      {
        return timer;
      } else {
        return null;
      }
    }

    public static void AddTimer(string name, double interval, ElapsedEventHandler eventFunc)
    {
      AddTimer(name, interval, eventFunc, true);
    }
    public static void AddTimer(string name, double interval, ElapsedEventHandler eventFunc, bool autoReset)
    {
      var timer = new System.Timers.Timer(interval)
      {
        AutoReset = autoReset,
        Enabled = true
      };
      timer.Elapsed += eventFunc;
      
      _timers[name] = timer;
    }
    
  }
}