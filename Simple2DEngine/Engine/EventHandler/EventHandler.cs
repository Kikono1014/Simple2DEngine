using static SDL2.SDL;

namespace Engine2D
{
  public partial class Engine : WM.WindowManager
  {
    public static class EventHandler
    {
      private static readonly bool[]  _keys = new bool[322];

      public static bool GetKey(int id) { return _keys[id]; }
      public static void UnpressKey(int id) { _keys[id] = false; }

      public delegate void HandleKeyboardInputFunc();
      private static HandleKeyboardInputFunc? HandleKeyboardInput;
      public static void SetHandleKeyboardInputFunc(HandleKeyboardInputFunc func)
        { HandleKeyboardInput = func; }


      public static void PollEvents()
      {
        while (SDL_PollEvent(out SDL_Event e) == 1)
        {
          switch (e.type)
          {
            case SDL_EventType.SDL_QUIT:
              _isRunning = false;
              break;
            case SDL_EventType.SDL_WINDOWEVENT:
              if (e.window.windowEvent == SDL_WindowEventID.SDL_WINDOWEVENT_CLOSE)
              {
                _windows[e.window.windowID].Close();
                _windows.Remove(e.window.windowID);    //? why doesn't Remove call the destructor? 
              }
              break;
            case SDL_EventType.SDL_KEYDOWN:
              _keys[(int)e.key.keysym.scancode] = true;
              break;
            case SDL_EventType.SDL_KEYUP:
              _keys[(int)e.key.keysym.scancode] = false;
              break;
          }
        }
        HandleKeyboardInput?.Invoke();
      }

    }
  }
}