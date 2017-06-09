using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _4thof4th.BulletStructure
{
    class PlayerBullet : BulletGeneric
    {
        public PlayerBullet(Texture2D e, Vector2 f) : base(e, f){
            pos = f;
        }
        public void Update() {
            base.pos.Y -= 9;
        }
    }
}
