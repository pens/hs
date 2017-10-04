using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Omicron
{
    public static class CollisionManager
    {
        public static bool Colliding(OBB a, OBB b)
        {
            float mina;
            float minb;
            float maxa;
            float maxb;
            float proj;
            //a axis[0]
            mina = maxa = Vector2.Dot(a.corners[0], a.axis[0]);
            for (int i = 1; i < 4; i++)
            {
                proj = Vector2.Dot(a.corners[i], a.axis[0]);
                if (proj < mina)
                    minb = proj;
                else if (proj > maxa)
                    maxb = proj;
            }
            minb = maxb = Vector2.Dot(b.corners[0], a.axis[0]);
            for (int i = 1; i < 4; i++)
            {
                proj = Vector2.Dot(b.corners[i], a.axis[0]);
                if (proj < minb)
                    minb = proj;
                else if (proj > maxb)
                    maxb = proj;
            }
            if (!(mina < maxb && maxa > minb))
            {
                return false;
            }
            //a axis[1]
            mina = maxa = Vector2.Dot(a.corners[0], a.axis[1]);
            for (int i = 1; i < 4; i++)
            {
                proj = Vector2.Dot(a.corners[i], a.axis[1]);
                if (proj < mina)
                    minb = proj;
                else if (proj > maxa)
                    maxb = proj;
            }
            minb = maxb = Vector2.Dot(b.corners[0], a.axis[1]);
            for (int i = 1; i < 4; i++)
            {
                proj = Vector2.Dot(b.corners[i], a.axis[1]);
                if (proj < minb)
                    minb = proj;
                else if (proj > maxb)
                    maxb = proj;
            }
            if (!(mina < maxb && maxa > minb))
            {
                return false;
            }
            //b axis[0]
            mina = maxa = Vector2.Dot(a.corners[0], b.axis[0]);
            for (int i = 1; i < 4; i++)
            {
                proj = Vector2.Dot(a.corners[i], b.axis[0]);
                if (proj < mina)
                    minb = proj;
                else if (proj > maxa)
                    maxb = proj;
            }
            minb = maxb = Vector2.Dot(b.corners[0], b.axis[0]);
            for (int i = 1; i < 4; i++)
            {
                proj = Vector2.Dot(b.corners[i], b.axis[0]);
                if (proj < minb)
                    minb = proj;
                else if (proj > maxb)
                    maxb = proj;
            }
            if (!(mina < maxb && maxa > minb))
            {
                return false;
            }
            //b axis[1]
            mina = maxa = Vector2.Dot(a.corners[0], b.axis[1]);
            for (int i = 1; i < 4; i++)
            {
                proj = Vector2.Dot(a.corners[i], b.axis[1]);
                if (proj < mina)
                    minb = proj;
                else if (proj > maxa)
                    maxb = proj;
            }
            minb = maxb = Vector2.Dot(b.corners[0], b.axis[1]);
            for (int i = 1; i < 4; i++)
            {
                proj = Vector2.Dot(b.corners[i], b.axis[1]);
                if (proj < minb)
                    minb = proj;
                else if (proj > maxb)
                    maxb = proj;
            }
            if (!(mina < maxb && maxa > minb))
            {
                return false;
            }
            return true;
        }
    }
}
