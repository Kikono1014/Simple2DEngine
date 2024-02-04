using static SDL2.SDL;
namespace Engine2D
{

  public partial class Engine : WM.WindowManager
  {
    public partial class Scene
    {
      public class GameObject
      {
        protected string _name = "";
        protected string _scene = "";
        protected int _currentTexture = 0;
        protected List<nint> _textures = [];
        protected SDL_Rect _textureDst;
        protected int  _angle = 0;
        protected bool _isStatic = false;
        protected bool _isShow   = true;

        protected bool _isPhysical = false;

        protected GameObject() { }

        public GameObject(string name, List<nint> textures, SDL2.SDL.SDL_Rect dst)
        {
          _name = name;
          _textures = textures;
          _textureDst = dst;
        }

        public string GetName()             { return _name;                      }
        public string GetSceneName()        { return _scene;                     }
        public nint   GetTexture(int id)    { return _textures[id];              }
        public nint   GetCurrentTexture()   { return _textures[_currentTexture]; }
        public int    GetCurrentTextureId() { return _currentTexture;            }
        public ref    SDL_Rect GetDst()     { return ref _textureDst;            }
        public int    GetAngle()            { return _angle;                     }
        public bool   GetIsStatic()         { return _isStatic;                  }
        public bool   GetIsShow()           { return _isShow;                    }
        public bool   GetIsPhysical()       { return _isPhysical;                }

        public void SetName            (string name)          { _name           = name;               }
        public void SetSceneName       (string name)          { _scene          = name;               }
        public void SetTexture         (int id, nint texture) { _textures[id]   = texture;            }
        public void SetCurrentTexture  (int id, nint texture) { _textures[_currentTexture] = texture; }
        public void SetCurrentTextureId(int id)               { _currentTexture = id;                 }
        public void SetDst             (SDL_Rect textureDst)  { _textureDst     = textureDst;         }
        public void SetAngle           (int        angle)     { _angle          = angle;              }
        public void SetIsStatic        (bool       isStatic)  { _isStatic       = isStatic;           }
        public void SetIsShow          (bool       isShow)    { _isShow         = isShow;             }

        public int GetX() { return _textureDst.x; }
        public int GetY() { return _textureDst.y; }
        public int GetW() { return _textureDst.w; }
        public int GetH() { return _textureDst.h; }

        public void SetX(int x) { _textureDst.x = x; }
        public void SetY(int y) { _textureDst.y = y; }
        public void SetW(int w) { _textureDst.w = w; }
        public void SetH(int h) { _textureDst.h = h; }

        public void AddX(int x) { _textureDst.x += x; }
        public void AddY(int y) { _textureDst.y += y; }
        public void AddW(int w) { _textureDst.w += w; }
        public void AddH(int h) { _textureDst.h += h; }

        public void AddAngle(int a) { _angle = (a + _angle) % 360; }

        public void NextTexture()     { _currentTexture = ++_currentTexture % _textures.Count; }
        public void PreviousTexture() { _currentTexture = (--_currentTexture + _textures.Count) % _textures.Count; }
      }
    }
  }
}
