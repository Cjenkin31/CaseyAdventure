using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;

namespace CaseyAdventure
{
    class ActionFunctions
    {
		public static bool BlockInWay(int tilex, int tiley) {
			return !(Collision.EmptyTile(tilex, tiley, false));
		}
		public static bool IsOnGround(int tilex, int tiley) {
			return !(Collision.EmptyTile(tilex, tiley+1, false));
		}

        public static bool build(int tilex,int tiley) // Build function made for creating the beanstalk. Can be used by other items if I add in some checks
		{
			if(!IsOnGround(tilex,tiley)){
				return true;
			}
					int y=tiley;
					while(y != 0)
					{
						if (BlockInWay(tilex, y)){
							break;
						}
						int cx = tilex;
						int cy = y;
						int rdm = Main.rand.Next(1,7);
						WorldGen.PlaceTile(cx, cy, TileID.VineRope);
						if (rdm == 1)
						{
							if (!BlockInWay(cx+1, cy+1)){
								WorldGen.PlaceTile(cx+1, cy, TileID.VineRope);
							}
						} 
						else if (rdm ==2)
						{
							if (!BlockInWay(cx-1, cy+1)){
								WorldGen.PlaceTile(cx-1, cy, TileID.VineRope);
							}
						}

						y--;
					}
					return true;
		}
    }
}