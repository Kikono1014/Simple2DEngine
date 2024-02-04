using static SDL2.SDL;
using WM;
using SixLabors.ImageSharp.ColorSpaces;
namespace Engine2D
{

  public partial class Engine : WindowManager
  {
    public static Scene? GetScene(string title)
    {
      Scene? scene;
      if (_scenes.TryGetValue(title, out scene))
      {
        return scene;
      } else {
        return null;
      }
    }


    public static Window? GetScenesWindow(string sceneTitle)
    { 
      return GetScene(sceneTitle)?.GetWindow();
    }

    public static Scene.GameObject? GetObjectFromScene(string sceneTitle, string name)
    {
      return GetScene(sceneTitle)?.GetObject(name);
    }

    public static void AddObjectToScene(string sceneTitle, Scene.GameObject obj)
    {
      GetScene(sceneTitle)?.AddObject(obj);
    }
    
    public static void RemoveObjectFromScene(string sceneTitle, string name)
    {
      GetScene(sceneTitle)?.RemoveObject(name);
    }

    public partial class Scene
    {
      private readonly Window? _window;
      private readonly Dictionary<string, GameObject> _objects = [];
      private SDL_Point _camCoord;

      public Window? GetWindow() { return _window; }
      public GameObject? GetObject(string name)
      {
        GameObject? obj;
        if (_objects.TryGetValue(name, out obj))
        {
          return obj;
        } else {
          return null;
        }
      }

      public ref SDL_Point GetCamCoord() { return ref _camCoord; }

      public Scene(Window? window)
      {
        _window = window;
        if (_window != null)
        {
          _camCoord =  new() { 
            x = _window.GetWindowW() / 2,
            y = _window.GetWindowH() / 2
          };
        }
      }

      public void AddObject(GameObject obj)
      {
        _objects[obj.GetName()] = obj;
      }
      public void RemoveObject(string name)
      {
        if (GetObject(name) != null)
        {
          _objects.Remove(name);
        }
      }

      public void Render()
      {
        if (_window != null)
        {
          foreach (var obj in _objects.Values)
          {
            if (obj.GetIsShow())
            {
              SDL_Rect objDst = obj.GetDst();
              if (!obj.GetIsStatic())
              {
                objDst.x -= _camCoord.x - _window.GetWindowW() / 2;
                objDst.y -= _camCoord.y - _window.GetWindowH() / 2;
              }
              
              SDL_RenderCopyEx( _window.GetRenderer(), obj.GetCurrentTexture(),
                                IntPtr.Zero, ref objDst,
                                obj.GetAngle(), IntPtr.Zero,
                                obj.GetFlip()
                              );
              
              if (obj.GetIsPhysical())
              {
                PhysicalObject phObj = (PhysicalObject)obj;
                if (phObj.GetIsShowHitbox())
                {
                  if (!obj.GetIsStatic())
                  {
                    SDL_FRect hitbox = phObj.GetHitbox();
                    hitbox.x -= _camCoord.x - _window.GetWindowW() / 2;
                    hitbox.y -= _camCoord.y - _window.GetWindowH() / 2;
                    
                    SDL_FPoint center = phObj.GetCenter();
                    center.x -= _camCoord.x - _window.GetWindowW() / 2;
                    center.y -= _camCoord.y - _window.GetWindowH() / 2;

                    Graphic.DrawingMethods.SDL_RenderDrawColorRectF(
                      _window.GetRenderer(),
                      ref hitbox,
                      Graphic.DrawingMethods.RGBA(250, 180, 20, 255));
                    Graphic.DrawingMethods.SDL_RenderDrawColorLineF(
                      _window.GetRenderer(),
                      center.x,
                      center.y,
                      center.x + phObj.GetVelocity().x * 10,
                      center.y + phObj.GetVelocity().y * 10,
                      Graphic.DrawingMethods.RGBA(250, 1, 200, 255));
                    Graphic.DrawingMethods.SDL_RenderDrawColorLineF(
                      _window.GetRenderer(),
                      center.x,
                      center.y,
                      center.x + phObj.GetAcceleration().x * 10,
                      center.y + phObj.GetAcceleration().y * 10,
                      Graphic.DrawingMethods.RGBA(25, 200, 0, 255));
                  }
                }
              }
            }
          }
        }
      }
    
      public void UpdatePhysicalObjects()
      {
        foreach (var obj in _objects.Values)
        {
          if (obj.GetIsPhysical())
          {
            PhysicalObject phObj = (PhysicalObject)obj;

            System.Console.WriteLine();
            Console.WriteLine($"v( {phObj.GetHitbox().x}, {phObj.GetHitbox().y} )");
            Console.WriteLine($"v( {phObj.GetVelocity().x}, {phObj.GetVelocity().y} )");
            Console.WriteLine($"a( {phObj.GetAcceleration().x}, {phObj.GetAcceleration().y} )");


            SDL_FPoint resist = new() 
              {
                x = -phObj.GetVelocity().x * phObj.GetMass() * 0.045f,
                y = -phObj.GetVelocity().y * phObj.GetMass() * 0.045f
              };

            
            Engine.Physic.MoveObject(phObj);
            Engine.Physic.UpdateVelocity(phObj);
            phObj.SetAcceleration(resist);
          }
        }
      }
    }
  }
}
