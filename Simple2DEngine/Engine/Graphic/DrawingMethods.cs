using static SDL2.SDL;
using System;

namespace Engine2D
{
  public partial class Engine : WM.WindowManager
  {
    public static partial class Graphic
    {
      public static class DrawingMethods
      {
        public static void SDL_RenderDrawColorLine(nint renderer, int x1, int y1, int x2, int y2, uint color)
        {
          byte rDefault, gDefault, bDefault, aDefault;
          SDL_GetRenderDrawColor(renderer, out rDefault, out gDefault, out bDefault, out aDefault);

          byte r, g, b, a;
          DecodeRGBA(color, out r, out g, out b, out a);
          SDL_SetRenderDrawColor(renderer, r, g, b, a);

          SDL_RenderDrawLine(renderer, x1, y1, x2, y2);
          
          SDL_SetRenderDrawColor(renderer, rDefault, gDefault, bDefault, aDefault);
        }
        public static void SDL_RenderDrawColorLineF(nint renderer, float x1, float y1, float x2, float y2, uint color)
        {
          byte rDefault, gDefault, bDefault, aDefault;
          SDL_GetRenderDrawColor(renderer, out rDefault, out gDefault, out bDefault, out aDefault);

          byte r, g, b, a;
          DecodeRGBA(color, out r, out g, out b, out a);
          SDL_SetRenderDrawColor(renderer, r, g, b, a);

          SDL_RenderDrawLineF(renderer, x1, y1, x2, y2);
          
          SDL_SetRenderDrawColor(renderer, rDefault, gDefault, bDefault, aDefault);
        }
        public static void SDL_RenderDrawColorLines(nint renderer, SDL_Point[] points, uint color)
        {
          byte rDefault, gDefault, bDefault, aDefault;
          SDL_GetRenderDrawColor(renderer, out rDefault, out gDefault, out bDefault, out aDefault);

          byte r, g, b, a;
          DecodeRGBA(color, out r, out g, out b, out a);
          SDL_SetRenderDrawColor(renderer, r, g, b, a);

          SDL_RenderDrawLines(renderer, points, points.Length);
          
          SDL_SetRenderDrawColor(renderer, rDefault, gDefault, bDefault, aDefault);
        }
        public static void SDL_RenderDrawColorLinesF(nint renderer, SDL_FPoint[] points, uint color)
        {
          byte rDefault, gDefault, bDefault, aDefault;
          SDL_GetRenderDrawColor(renderer, out rDefault, out gDefault, out bDefault, out aDefault);

          byte r, g, b, a;
          DecodeRGBA(color, out r, out g, out b, out a);
          SDL_SetRenderDrawColor(renderer, r, g, b, a);

          SDL_RenderDrawLinesF(renderer, points, points.Length);
          
          SDL_SetRenderDrawColor(renderer, rDefault, gDefault, bDefault, aDefault);
        }
        public static void SDL_RenderDrawColorPoint(nint renderer, int x, int y, uint color)
        {
          byte rDefault, gDefault, bDefault, aDefault;
          SDL_GetRenderDrawColor(renderer, out rDefault, out gDefault, out bDefault, out aDefault);

          byte r, g, b, a;
          DecodeRGBA(color, out r, out g, out b, out a);
          SDL_SetRenderDrawColor(renderer, r, g, b, a);

          SDL_RenderDrawPoint(renderer, x, y);
          
          SDL_SetRenderDrawColor(renderer, rDefault, gDefault, bDefault, aDefault);
        }
        public static void SDL_RenderDrawColorPointF(nint renderer, float x, float y, uint color)
        {
          byte rDefault, gDefault, bDefault, aDefault;
          SDL_GetRenderDrawColor(renderer, out rDefault, out gDefault, out bDefault, out aDefault);

          byte r, g, b, a;
          DecodeRGBA(color, out r, out g, out b, out a);
          SDL_SetRenderDrawColor(renderer, r, g, b, a);

          SDL_RenderDrawPointF(renderer, x, y);
          
          SDL_SetRenderDrawColor(renderer, rDefault, gDefault, bDefault, aDefault);
        }
        public static void SDL_RenderDrawColorPoints(nint renderer, SDL_Point[] points, uint color)
        {
          byte rDefault, gDefault, bDefault, aDefault;
          SDL_GetRenderDrawColor(renderer, out rDefault, out gDefault, out bDefault, out aDefault);

          byte r, g, b, a;
          DecodeRGBA(color, out r, out g, out b, out a);
          SDL_SetRenderDrawColor(renderer, r, g, b, a);

          SDL_RenderDrawPoints(renderer, points, points.Length);
          
          SDL_SetRenderDrawColor(renderer, rDefault, gDefault, bDefault, aDefault);
        }
        public static void SDL_RenderDrawColorPointsF(nint renderer, SDL_FPoint[] points, uint color)
        {
          byte rDefault, gDefault, bDefault, aDefault;
          SDL_GetRenderDrawColor(renderer, out rDefault, out gDefault, out bDefault, out aDefault);

          byte r, g, b, a;
          DecodeRGBA(color, out r, out g, out b, out a);
          SDL_SetRenderDrawColor(renderer, r, g, b, a);

          SDL_RenderDrawPointsF(renderer, points, points.Length);
          
          SDL_SetRenderDrawColor(renderer, rDefault, gDefault, bDefault, aDefault);
        }
        public static void SDL_RenderDrawColorRect(nint renderer, ref SDL_Rect rect, uint color)
        {
          byte rDefault, gDefault, bDefault, aDefault;
          SDL_GetRenderDrawColor(renderer, out rDefault, out gDefault, out bDefault, out aDefault);

          byte r, g, b, a;
          DecodeRGBA(color, out r, out g, out b, out a);
          SDL_SetRenderDrawColor(renderer, r, g, b, a);

          SDL_RenderDrawRect(renderer, ref rect);
          
          SDL_SetRenderDrawColor(renderer, rDefault, gDefault, bDefault, aDefault);
        }
        public static void SDL_RenderDrawColorRectF(nint renderer, ref SDL_FRect rect, uint color)
        {
          byte rDefault, gDefault, bDefault, aDefault;
          SDL_GetRenderDrawColor(renderer, out rDefault, out gDefault, out bDefault, out aDefault);

          byte r, g, b, a;
          DecodeRGBA(color, out r, out g, out b, out a);
          SDL_SetRenderDrawColor(renderer, r, g, b, a);

          SDL_RenderDrawRectF(renderer, ref rect);
          
          SDL_SetRenderDrawColor(renderer, rDefault, gDefault, bDefault, aDefault);
        }
        public static void SDL_RenderDrawColorRects(nint renderer, SDL_Rect[] rects, uint color)
        {
          byte rDefault, gDefault, bDefault, aDefault;
          SDL_GetRenderDrawColor(renderer, out rDefault, out gDefault, out bDefault, out aDefault);

          byte r, g, b, a;
          DecodeRGBA(color, out r, out g, out b, out a);
          SDL_SetRenderDrawColor(renderer, r, g, b, a);

          SDL_RenderDrawRects(renderer, rects, rects.Length);
          
          SDL_SetRenderDrawColor(renderer, rDefault, gDefault, bDefault, aDefault);
        }
        public static void SDL_RenderDrawColorRectsF(nint renderer, SDL_FRect[] rects, uint color)
        {
          byte rDefault, gDefault, bDefault, aDefault;
          SDL_GetRenderDrawColor(renderer, out rDefault, out gDefault, out bDefault, out aDefault);

          byte r, g, b, a;
          DecodeRGBA(color, out r, out g, out b, out a);
          SDL_SetRenderDrawColor(renderer, r, g, b, a);

          SDL_RenderDrawRectsF(renderer, rects, rects.Length);
          
          SDL_SetRenderDrawColor(renderer, rDefault, gDefault, bDefault, aDefault);
        }
        public static void SDL_RenderFillColorRect(nint renderer, ref SDL_Rect rect, uint color)
        {
          byte rDefault, gDefault, bDefault, aDefault;
          SDL_GetRenderDrawColor(renderer, out rDefault, out gDefault, out bDefault, out aDefault);

          byte r, g, b, a;
          DecodeRGBA(color, out r, out g, out b, out a);
          SDL_SetRenderDrawColor(renderer, r, g, b, a);

          SDL_RenderFillRect(renderer, ref rect);
          
          SDL_SetRenderDrawColor(renderer, rDefault, gDefault, bDefault, aDefault);
        }
        public static void SDL_RenderFillColorRectF(nint renderer, ref SDL_FRect rect, uint color)
        {
          byte rDefault, gDefault, bDefault, aDefault;
          SDL_GetRenderDrawColor(renderer, out rDefault, out gDefault, out bDefault, out aDefault);

          byte r, g, b, a;
          DecodeRGBA(color, out r, out g, out b, out a);
          SDL_SetRenderDrawColor(renderer, r, g, b, a);

          SDL_RenderFillRectF(renderer, ref rect);
          
          SDL_SetRenderDrawColor(renderer, rDefault, gDefault, bDefault, aDefault);
        }
        public static void SDL_RenderFillColorRects(nint renderer, SDL_Rect[] rects, uint color)
        {
          byte rDefault, gDefault, bDefault, aDefault;
          SDL_GetRenderDrawColor(renderer, out rDefault, out gDefault, out bDefault, out aDefault);

          byte r, g, b, a;
          DecodeRGBA(color, out r, out g, out b, out a);
          SDL_SetRenderDrawColor(renderer, r, g, b, a);

          SDL_RenderFillRects(renderer, rects, rects.Length);
          
          SDL_SetRenderDrawColor(renderer, rDefault, gDefault, bDefault, aDefault);
        }
        public static void SDL_RenderFillColorRectsF(nint renderer, SDL_FRect[] rects, uint color)
        {
          byte rDefault, gDefault, bDefault, aDefault;
          SDL_GetRenderDrawColor(renderer, out rDefault, out gDefault, out bDefault, out aDefault);

          byte r, g, b, a;
          DecodeRGBA(color, out r, out g, out b, out a);
          SDL_SetRenderDrawColor(renderer, r, g, b, a);

          SDL_RenderFillRectsF(renderer, rects, rects.Length);
          
          SDL_SetRenderDrawColor(renderer, rDefault, gDefault, bDefault, aDefault);
        }
        public static void SDL_BRenderGeometry(nint renderer, nint texture, SDL_Vertex[] vertices, int[]? indices)
        {
          SDL_RenderGeometry(renderer, texture, vertices, vertices.Length, indices, 
                            (indices == null) ? 0 : indices.Length);
        }


        public static uint RGBA(byte r, byte g, byte b, byte a)
        {
          return (uint)(r << 24) | (uint)(g << 16) | (uint)(b << 8) | a;
        }

        public static void DecodeRGBA(uint color, out byte r, out byte g, out byte b, out byte a)
        {
          r = (byte)(color >> 24);
          g = (byte)((color << 8 ) >> 24);
          b = (byte)((color << 16) >> 24);
          a = (byte)((color << 24) >> 24);
        }


        public static void RenderConvexShape(nint renderer, SDL_Vertex[] verts)
        {
          for (int j = 1; j < verts.Length - 1; ++j)
          {
            SDL_Vertex[] pol = [verts[0],
                                verts[j],
                                verts[j+1]];
            SDL_BRenderGeometry(renderer, (nint)null, pol, null);
          }
        }

        public static SDL_Vertex[] CreateNgonalVertexShape(int n, SDL_FPoint center, int size, uint color)
        {
          byte r, g, b, a;
          DecodeRGBA(color, out r, out g, out b, out a);

          SDL_Vertex[] verts = new SDL_Vertex[n];
          for (int i = 0; i < n; ++i)
          {
            var position = center;
            position.y -= size;

            RotatePointRelatively(center, ref position, 360 / n * i);

            verts[i] = new SDL_Vertex {
                    position  = position, 
                    color     = new SDL_Color{  r = r, g = g, b = b, a = a },
                    tex_coord = new SDL_FPoint{ x = 0 } 
                  };
          }
          return verts;
        }

        public static SDL_FPoint GetCenterOfVertex(SDL_Vertex[] verts)
        {
          float sumX = 0;
          float sumY = 0;
          foreach (var vert in verts)
          {
            sumX += vert.position.x;
            sumY += vert.position.y;
          }
          return new SDL_FPoint { x= sumX / verts.Length, y = sumY / verts.Length };
        }

        public static void RotatePointRelatively(SDL_FPoint center, ref SDL_FPoint point, int angle) 
        {
          CoordinatesRelativeToPoint(center, ref point);
          RotatePoint(ref point, angle);
          CoordinatesRelativeToPoint(new SDL_FPoint{ x = -center.x, y = center.y }, ref point);
        }

        public static void RotatePoint(ref SDL_FPoint point, int angle) 
        {
          var radAngel = (float)(angle * Math.PI) / 180;
          point = new SDL_FPoint {
            x = point.x * MathF.Cos(radAngel) - point.y * MathF.Sin(radAngel),
            y = point.x * MathF.Sin(radAngel) + point.y * MathF.Cos(radAngel)
          };
        }

        public static void CoordinatesRelativeToPoint(SDL_FPoint center, ref SDL_FPoint point) 
        {
          point.x =   point.x - center.x;
          point.y = -(point.y - center.y);
        }
      }
    }
  }
}