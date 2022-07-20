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
        public static bool BlockInWay(int tilex, int tiley)
        {
            return !(Collision.EmptyTile(tilex, tiley, false));
        }
        public static bool IsOnGround(int tilex, int tiley)
        {
            return !(Collision.EmptyTile(tilex, tiley + 1, false));
        }

        public static bool build(int tilex, int tiley) // Build function made for creating the beanstalk. Can be used by other items if I add in some checks
        {
            if (!IsOnGround(tilex, tiley))
            {
                return true;
            }
            int y = tiley;
            while (y != 0)
            {
                if (BlockInWay(tilex, y))
                {
                    break;
                }
                int cx = tilex;
                int cy = y;
                int rdm = Main.rand.Next(1, 7);
                WorldGen.PlaceTile(cx, cy, TileID.VineRope);
                if (rdm == 1)
                {
                    if (!BlockInWay(cx + 1, cy + 1))
                    {
                        WorldGen.PlaceTile(cx + 1, cy, TileID.VineRope);
                    }
                }
                else if (rdm == 2)
                {
                    if (!BlockInWay(cx - 1, cy + 1))
                    {
                        WorldGen.PlaceTile(cx - 1, cy, TileID.VineRope);
                    }
                }

                y--;
            }
            return true;
        }
        public static bool Find(int tilex, int tiley)
        {
            bool rightFound = false;
            bool leftFound = false;
            int x = tilex;
            int y = 0;
            Vector2 startingPos = new Vector2(tilex, tiley);
            Vector2 tilesAwayRight = new Vector2();
            Vector2 tilesAwayLeft = new Vector2();
            int worldMaxX = Main.maxTilesX; // World Border
            int worldMaxY = 600; // set to 600 since that's the average of how low islands are
            while (!rightFound)
            {
                if (x >= worldMaxX)
                {
                    break;
                }
                if (y >= worldMaxY)
                {
                    x += 2;
                    y = 0;
                }
                int tyle = Main.tile[x, y].TileType;
                if (tyle == TileID.Sunplate || tyle == TileID.Cloud || tyle == TileID.RainCloud || tyle == TileID.SnowCloud) // Common island blocks
                {
                    tilesAwayRight.X = x; // Set the vector x & y. Also set the "Found" to true
                    tilesAwayRight.Y = y;
                    rightFound = true;
                }
                else
                {
                    y += 2;
                }
            }
            int scanRange = 0; // If the right was found, no need to search the entire left side of the map since I'm looking for the closest
            if (!rightFound)
            {
                scanRange = 0; // This sets the scan range to infinite given there was nothing on the right
            }
            else
            {
                scanRange = (int)Math.Abs(tilex - (tilesAwayRight.X - tilex)); // This is the range away from the player pos. Ex. Player pos: 2000 Found at: 2500, Range is 500. So it searches 1500->2000
            }
            x = tilex;
            y = 0;
            while (!leftFound)
            {
                if (x <= scanRange || x <= 0) // x<=0 just in case we hit the edge
                {
                    break;
                }
                if (y >= worldMaxY)
                {
                    x = x - 2;
                    y = 0;
                }
                int tyle = Main.tile[x, y].TileType;
                if (tyle == TileID.Sunplate || tyle == TileID.Cloud || tyle == TileID.RainCloud || tyle == TileID.SnowCloud)
                {
                    tilesAwayLeft.X = x;
                    tilesAwayLeft.Y = y;
                    leftFound = true;
                }
                else
                {
                    y += 2;
                }
            }
            int numericalAwayRight = -1; //Defaults
            int numericalAwayLeft = -1;
            numericalAwayRight = rightFound ? (int)tilesAwayRight.Distance(startingPos) : 99999999; // I feel like 99999999 is a big enough number
            numericalAwayLeft = leftFound ? (int)tilesAwayLeft.Distance(startingPos) : 99999999;

            string xAway = "You are: NIL X pos away."; // Set the defaults
            string yAway = "You are: NIL Y pos away."; // Set the defaults
                                                       // I do extra checks just to be safe...
            if (numericalAwayLeft > numericalAwayRight || !leftFound) // If the left is further away or there was no left found...
            {
                if ((tilex - tilesAwayRight.X) < 0)
                {
                    xAway = "Island located: " + Math.Abs((tilex - tilesAwayRight.X)) + " blocks to the right"; // Display how close player is to the RIGHT island
                }
                else
                {
                    xAway = "Island located: " + (tilex - tilesAwayRight.X) + " blocks to the left";
                }

                yAway = "You are: " + (tiley - tilesAwayRight.Y) + " Y pos away."; // Distance up and down away
            }
            else if (numericalAwayRight > numericalAwayLeft || !rightFound) // If the right is further away or there was no right found...
            {
                if ((tilex - tilesAwayLeft.X) < 0)
                {
                    xAway = "Island located: " + (tilex - tilesAwayLeft.X) + " blocks to the right";// Display how close player is to the LEFT island
                }
                else
                {
                    xAway = "Island located: " + Math.Abs((tilex - tilesAwayLeft.X)) + " blocks to the left";
                }
                yAway = "You are: " + (tiley - tilesAwayLeft.Y) + " Y pos away."; // Distance up and down away
            }
            if (xAway == "You are: NIL X pos away.") // If nothing was set, the user is hopefully at the island because left and right should be ==
            {
                Main.NewText("You Found It!", 150, 250, 150);
                return true;
            }
            // Tell the user how close they are
            Main.NewText(xAway, 150, 250, 150);
            Main.NewText(yAway, 150, 250, 150);
            Main.NewText("", 150, 250, 150);
            return true;
        }
    }
}