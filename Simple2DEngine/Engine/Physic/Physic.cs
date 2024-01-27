using System.Runtime.CompilerServices;
using static SDL2.SDL;

namespace Engine2D
{
  public partial class Engine : WM.WindowManager
  {
    public static class Physic
    {
      public static void UpdateObjects()
      {
        foreach (var scene in _scenes.Values)
        {
          scene.UpdatePhysicalObjects();
        }
      }

      public static void MoveObject(Scene.PhysicalObject obj)
      {
        if (obj.GetVelocity().x != 0 || obj.GetVelocity().y != 0)
        {
          SDL_FPoint position = new() { x = obj.GetHitbox().x, y = obj.GetHitbox().y };
          position.x += obj.GetVelocity().x + obj.GetAcceleration().x / 2;
          position.y += obj.GetVelocity().y + obj.GetAcceleration().y / 2;
          ref SDL_Rect dst = ref obj.GetDst();
          ref SDL_FRect hitbox = ref obj.GetHitbox();
          dst.x = (int)position.x;
          dst.y = (int)position.y;
          hitbox.x = position.x;
          hitbox.y = position.y;
        }
      }

      public static void UpdateVelocity(Scene.PhysicalObject obj)
      {
        if (obj.GetAcceleration().x != 0 || obj.GetAcceleration().y != 0)
        {
          obj.SetVelocity(
              new SDL_FPoint 
                {
                  x = MathF.Round(obj.GetVelocity().x + obj.GetAcceleration().x, 6),
                  y = MathF.Round(obj.GetVelocity().y + obj.GetAcceleration().y, 6)
                });
        }
      }

      public static void ApplyForce(Scene.PhysicalObject obj, SDL_FPoint force)
      {
        if (force.x != 0 || force.y != 0)
        {
          obj.SetAcceleration(
              new SDL_FPoint 
                {
                  x = MathF.Round(obj.GetAcceleration().x + (force.x / obj.GetMass()), 6),
                  y = MathF.Round(obj.GetAcceleration().y + (force.y / obj.GetMass()), 6)
                });
          
        }
      }
      
    }
  }
}