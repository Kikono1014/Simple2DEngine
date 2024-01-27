using static SDL2.SDL;
using static SDL2.SDL_image;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Runtime.InteropServices;

namespace WM
{
  public class WindowManager
  {
    protected static readonly Dictionary<uint, Window> _windows = [];

    public static Window? GetWindow(uint id)
    {
      Window? window;
      if (_windows.TryGetValue(id, out window))
      {
        return window;
      } else {
        return null;
      }
    }
    public static Dictionary<uint, Window> GetWindows() { return _windows; }
    
    protected static void SetupWM(uint sdlInitFlags)
    {
      if (SDL_Init(sdlInitFlags) < 0)
      {
          Console.WriteLine($"There was an issue initializing  {SDL_GetError()}");
      }
    }
    protected static void SetupWM( uint sdlInitFlags,
                                   IMG_InitFlags imgInitFlags)
    {
      SetupWM(sdlInitFlags);
      
      if (IMG_Init(imgInitFlags) < 0)
      {
        Console.WriteLine($"There was an issue initializing  {IMG_GetError()}");
      }
    }

    protected static void CreateWindow( string title, 
                                        int x, int y,
                                        int w, int h,
                                        SDL_WindowFlags   windowFlags,
                                        SDL_RendererFlags rendererFlags)
    {
      Window window = new(title, 
                          x, y,
                          w, h,
                          windowFlags, rendererFlags);
      _windows[SDL_GetWindowID(window.GetWindowPtr())] = window;
    }

    protected static void CloseWindows()
    {
      foreach (var window in _windows.Values)
      {
        window.Close();
      }
    }

  }
}