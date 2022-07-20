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
                PlaceRandomRope(cx, cy, rdm);

                y--;
            }
            return true;

            static void PlaceRandomRope(int cx, int cy, int rdm)
            {
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
            }
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

            ScanRightSide(ref rightFound, ref x, ref y, ref tilesAwayRight, worldMaxX, worldMaxY); // Scan the right side of the map for islands

            int scanRange = 0;
            scanRange = SetScanRange(tilex, rightFound, tilesAwayRight); // Scan range is set for scanning the left side of the map

            x = tilex;
            y = 0;

            ScanLeftSide(ref leftFound, ref x, ref y, ref tilesAwayLeft, worldMaxY, scanRange); // Scan the left side of the map for islands

            int numericalAwayRight = -1; // Defaults
            int numericalAwayLeft = -1;
            string xAway = "You are: NIL X pos away."; // Set the defaults
            string yAway = "You are: NIL Y pos away."; // Set the defaults

            numericalAwayRight = rightFound ? (int)tilesAwayRight.Distance(startingPos) : 99999999; // I feel like 99999999 is a big enough number
            numericalAwayLeft = leftFound ? (int)tilesAwayLeft.Distance(startingPos) : 99999999;


            SetChatMessage(tilex, tiley, rightFound, leftFound, tilesAwayRight, tilesAwayLeft, numericalAwayRight, numericalAwayLeft, ref xAway, ref yAway);

            if (CheckIfPlayerIsOnIsland(xAway))
            {
                return true;
            }

            // Tell the user how close they are
            TellPlayerHowFar(xAway, yAway);

            return true;

            static void ScanRightSide(ref bool rightFound, ref int x, ref int y, ref Vector2 tilesAwayRight, int worldMaxX, int worldMaxY)
            {
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
                        tilesAwayRight.X = x; // Set the vector x & y
                        tilesAwayRight.Y = y;
                        rightFound = true;
                    }
                    else
                    {
                        y += 2;
                    }
                }
            }
            static void ScanLeftSide(ref bool leftFound, ref int x, ref int y, ref Vector2 tilesAwayLeft, int worldMaxY, int scanRange)
            {
                while (!leftFound)
                {
                    if (x <= scanRange || x <= 0) // x<=0 just in case we hit the edge
                    {
                        break;
                    }
                    if (y >= worldMaxY)
                    {
                        x -= 2;
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
            }

            static void SetChatMessage(int tilex, int tiley, bool rightFound, bool leftFound, Vector2 tilesAwayRight, Vector2 tilesAwayLeft, int numericalAwayRight, int numericalAwayLeft, ref string xAway, ref string yAway)
            {            
                // I do extra checks just to be safe...
                if (numericalAwayLeft > numericalAwayRight || !leftFound) // If the left is further away or there was no left found...
                {
                    // Check to see if the island is on the left or right side of the player
                    xAway = ((tilex - tilesAwayRight.X) < 0) ? "Island located: " + Math.Abs((tilex - tilesAwayRight.X)) + " blocks to the right" : "Island located: " + (tilex - tilesAwayRight.X) + " blocks to the left";
                    yAway = "You are: " + (tiley - tilesAwayRight.Y) + " Y pos away."; // Distance up and down away
                }
                else if (numericalAwayRight > numericalAwayLeft || !rightFound) // If the right is further away or there was no right found...
                {
                    // Check to see if the island is on the left or right side of the player
                    xAway = ((tilex - tilesAwayLeft.X) < 0) ? "Island located: " + (tilex - tilesAwayLeft.X) + " blocks to the right" : "Island located: " + Math.Abs((tilex - tilesAwayLeft.X)) + " blocks to the left";
                    yAway = "You are: " + (tiley - tilesAwayLeft.Y) + " Y pos away."; // Distance up and down away
                }
            }

            static bool CheckIfPlayerIsOnIsland(string xAway)
            {
                if (xAway == "You are: NIL X pos away.") // If nothing was set, the user is hopefully at the island because left and right should be ==
                {
                    Main.NewText("You Found It!", 150, 250, 150);
                    return true;
                }
                return false;
            }

            static void TellPlayerHowFar(string xAway, string yAway)
            {
                Main.NewText(xAway, 150, 250, 150);
                Main.NewText(yAway, 150, 250, 150);
                Main.NewText("", 150, 250, 150);
            }

            static int SetScanRange(int tilex, bool rightFound, Vector2 tilesAwayRight)
            {
                int scanRange;
                if (!rightFound)
                {
                    scanRange = 0; // This sets the scan range to infinite given there was nothing on the right
                }
                else // If the right was found, no need to search the entire left side of the map since I'm looking for the closest
                {
                    scanRange = (int)Math.Abs(tilex - (tilesAwayRight.X - tilex)); // This is the range away from the player pos. Ex. Player pos: 2000 Found at: 2500, Range is 500. So it searches 1500->2000
                }

                return scanRange;
            }
        }


    }
}