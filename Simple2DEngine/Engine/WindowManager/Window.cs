using static SDL2.SDL;

namespace WM
{
  public class Window
  {
    private readonly IntPtr _renderer;
    private readonly IntPtr _windowPtr;

    public IntPtr GetRenderer () { return _renderer;  }
    public IntPtr GetWindowPtr() { return _windowPtr; }

    public int    GetWindowW()     { SDL_GetWindowSize    (_windowPtr, out int w, out int _); return w; }
    public int    GetWindowH()     { SDL_GetWindowSize    (_windowPtr, out int _, out int h); return h; }
    public int    GetWindowX()     { SDL_GetWindowPosition(_windowPtr, out int x, out int _); return x; }
    public int    GetWindowY()     { SDL_GetWindowPosition(_windowPtr, out int _, out int y); return y; }
    public string GetWindowTitle() { return SDL_GetWindowTitle(_windowPtr); }

    public Window(string title,
                  int x, int y,
                  int w, int h,
                  SDL_WindowFlags   windowFlags,
                  SDL_RendererFlags _rendererFlags)
    {
      _windowPtr = SDL_CreateWindow(title, x, y, w, h, windowFlags);

      if (_windowPtr == IntPtr.Zero)
      {
        Console.WriteLine($"There was an issue creating the window. {SDL_GetError()}");
      }

      _renderer = SDL_CreateRenderer(_windowPtr, -1, _rendererFlags);

      if (_renderer == IntPtr.Zero)
      {
        Console.WriteLine($"There was an issue creating the _renderer. {SDL_GetError()}");
      }
    }
    ~Window() { Close(); }

    public void Close()
    {
      SDL_DestroyRenderer(_renderer);
      SDL_DestroyWindow(_windowPtr);
    }


  }
}