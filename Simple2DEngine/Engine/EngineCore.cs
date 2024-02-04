using static SDL2.SDL;
using static SDL2.SDL_image;
using static Engine2D.Engine.Graphic.DrawingMethods;
using System;
using WM;

namespace Engine2D
{
  public partial class Engine : WindowManager
  {
    private static bool     _isRunning = true;
    private static readonly Dictionary<string, nint> _textures = [];
    private static readonly Dictionary<string, Scene> _scenes = [];


    public static void SetupEngine()
    {
      Graphic.SetBackgroundColor(RGBA(255, 255, 255, 255));
    }
    public static void SetupEngine(uint sdlInitFlags)
    {
      SetupWM(sdlInitFlags);
      SetupEngine();
    }
    public static void SetupEngine(uint sdlInitFlags, IMG_InitFlags imgInitFlags)
    {
      SetupWM(sdlInitFlags, imgInitFlags);
      SetupEngine();
    }
    

    public static new void CreateWindow(string title, 
                                        int x, int y,
                                        int w, int h,
                                        SDL_WindowFlags   windowFlags,
                                        SDL_RendererFlags rendererFlags)
    {
      WindowManager.CreateWindow(title, 
                      x, y,
                      w, h,
                      windowFlags, rendererFlags);
      LoadTexture(1, "./Engine/standartth/images/", "MissingTexture.png");
    }

    public static void CreateScene(string title, Window? window)
    {
      _scenes[title] = new Scene(window);
    }


    private static void CleanUp()
    {
      CloseWindows();
      DestroyTextures();
      SDL_Quit();
    }

    public static void RunEngine()
    {
      while (_isRunning)
      {
        EventHandler.PollEvents();
        Physic.UpdateObjects();
        Graphic.Render();
        
      }
      CleanUp();
    }

    public static void StopEngine()
    {
      _isRunning = false;
    }
  }
}