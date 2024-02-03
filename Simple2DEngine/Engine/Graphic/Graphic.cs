using static SDL2.SDL;
using static SDL2.SDL_image;
using static Engine2D.Engine.Graphic.DrawingMethods;
using System;
using WM;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Drawing;
namespace Engine2D
{
  public partial class Engine : WindowManager
  {
    public static partial class Graphic
    {
      private static uint backgroundColor;
      public delegate void DrawFunc();
      private static DrawFunc? Draw;
      public static void SetDrawFunc(DrawFunc drawFunc) { Draw = drawFunc; }
      
      public static void SetBackgroundColor(uint color) { backgroundColor = color; }

      public static void Render()
      {
        foreach (var window in _windows.Values)
        {
          SDL_RenderPresent(window.GetRenderer());
          DecodeRGBA(backgroundColor, out byte r, out byte g, out byte b, out byte a);
          SDL_SetRenderDrawColor(window.GetRenderer(), r, g, b, a);
          SDL_RenderClear(window.GetRenderer());
        }
        Draw?.Invoke();
        foreach (var scene in _scenes.Values)
        {
          scene.Render();
        }
      }


    }
  }
}
