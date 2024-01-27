using static SDL2.SDL;
namespace Engine2D
{

  public partial class Engine : WM.WindowManager
  {
    public partial class Scene
    {
      public class PhysicalObject : GameObject
      {
        protected SDL_FRect  _hitbox;
        protected SDL_FPoint _center;

        protected SDL_FPoint _velocity;
        protected SDL_FPoint _acceleration;

        protected float      _mass = 1;

        protected bool _isShowHitbox    = false;
        protected bool _isCollideable   = false;
        protected bool _isGravitateable = false;
        
        public PhysicalObject() { _isPhysical = true; }


        public ref SDL_FRect  GetHitbox()       { return ref _hitbox;   }
        public     bool       GetIsShowHitbox() { return _isShowHitbox; }
        public     SDL_FPoint GetCenter()
        {
          return new SDL_FPoint
            {
              x = _hitbox.x + _hitbox.w / 2,
              y = _hitbox.y + _hitbox.h / 2
            };
        }
        public     SDL_FPoint GetVelocity()     { return _velocity;     }
        public     SDL_FPoint GetAcceleration() { return _acceleration; }
        public     float      GetMass()         { return _mass;         }     
        public void SetHitbox(SDL_FRect hitbox)
        {
          _hitbox = hitbox;
        }
        public void SetIsShowHitbox(bool isShowHitbox)       { _isShowHitbox  = isShowHitbox; }
        public void SetVelocity    (SDL_FPoint velocity)     {  _velocity     = velocity;     }
        public void SetAcceleration(SDL_FPoint acceleration) {  _acceleration = acceleration; }
        public void SetMass        (float      mass)         {  _mass         = mass;         }

      }
    }
  }
}
