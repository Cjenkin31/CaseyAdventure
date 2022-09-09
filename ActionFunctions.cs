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

        public static bool BuildSkyRope(int tilex, int tiley)
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
        public static bool BuildBridge(int tilex, int tiley)
        {
            if (!IsOnGround(tilex, tiley))
            {
                return true;
            }
            int x = tilex;
            int blocksLeft = 500;
            while (blocksLeft > 0)
            {
                Console.WriteLine("Help");
                if (BlockInWay(x, tiley))
                {
                    for(int i =0; i < 3; i++)
                    {
                        WorldGen.PlaceTile(x, tiley+i, TileID.Stone);
                    }
                }
                int cx = x;
                int cy = tiley ;
                WorldGen.PlaceTile(cx, cy, TileID.Stone);

                blocksLeft--;
                x++;
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
            int worldMaxX = Main.maxTilesX; 
            int worldMaxY = 600;

            ScanRightSide(ref rightFound, ref x, ref y, ref tilesAwayRight, worldMaxX, worldMaxY);

            int scanRange = 0;
            scanRange = SetScanRange(tilex, rightFound, tilesAwayRight);

            x = tilex;
            y = 0;

            ScanLeftSide(ref leftFound, ref x, ref y, ref tilesAwayLeft, worldMaxY, scanRange);

            int numericalAwayRight = -1;
            int numericalAwayLeft = -1;
            string xAway = "You are: NIL X pos away.";
            string yAway = "You are: NIL Y pos away.";

            numericalAwayRight = rightFound ? (int)tilesAwayRight.Distance(startingPos) : 99999999;
            numericalAwayLeft = leftFound ? (int)tilesAwayLeft.Distance(startingPos) : 99999999;


            SetChatMessage(tilex, tiley, rightFound, leftFound, tilesAwayRight, tilesAwayLeft, numericalAwayRight, numericalAwayLeft, ref xAway, ref yAway);

            if (CheckIfPlayerIsOnIsland(xAway))
            {
                return true;
            }

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
                    if (tyle == TileID.Sunplate || tyle == TileID.Cloud || tyle == TileID.RainCloud || tyle == TileID.SnowCloud)
                    {
                        tilesAwayRight.X = x;
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
                    if (x <= scanRange || x <= 0)
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
                if (numericalAwayLeft > numericalAwayRight || !leftFound)
                {

                    xAway = ((tilex - tilesAwayRight.X) < 0) ? "Island located: " + Math.Abs((tilex - tilesAwayRight.X)) + " blocks to the right" : "Island located: " + (tilex - tilesAwayRight.X) + " blocks to the left";
                    yAway = "You are: " + (tiley - tilesAwayRight.Y) + " Y pos away."; 
                }
                else if (numericalAwayRight > numericalAwayLeft || !rightFound)
                {
                    xAway = ((tilex - tilesAwayLeft.X) < 0) ? "Island located: " + (tilex - tilesAwayLeft.X) + " blocks to the right" : "Island located: " + Math.Abs((tilex - tilesAwayLeft.X)) + " blocks to the left";
                    yAway = "You are: " + (tiley - tilesAwayLeft.Y) + " Y pos away.";
                }
            }

            static bool CheckIfPlayerIsOnIsland(string xAway)
            {
                if (xAway == "You are: NIL X pos away.")
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
                    scanRange = 0;
                }
                else
                {
                    scanRange = (int)Math.Abs(tilex - (tilesAwayRight.X - tilex));
                }

                return scanRange;
            }
        }

        enum SlotMachineSymbols
        {
            Heart = 0,
            Diamond = 1,
            Spade = 2,
            Club = 3,
            Seven = 4,
            Cherry = 5,
            Clover = 6,
            Crown = 7,
            Bar = 8,
            Win = 9

        }
        public static bool SpinSlotMachine(int currency)
        {
            if (currency < 1)
            {
                Main.NewText("You did not have enough money", 150, 250, 150);
            }
            SlotMachineSymbols[] slotMachineCol1= new SlotMachineSymbols[] {SlotMachineSymbols.Heart,SlotMachineSymbols.Diamond,
                                                                                    SlotMachineSymbols.Spade,SlotMachineSymbols.Club,
                                                                                    SlotMachineSymbols.Seven,SlotMachineSymbols.Cherry,
                                                                                    SlotMachineSymbols.Clover,SlotMachineSymbols.Bar,
                                                                                    SlotMachineSymbols.Crown,SlotMachineSymbols.Win};
            SlotMachineSymbols[] slotMachineCol2 = slotMachineCol1;
            SlotMachineSymbols[] slotMachineCol3 = slotMachineCol1;                                                                       
            int rdm = Main.rand.Next(0, 9);
            SlotMachineSymbols middleSymbolCol1 = slotMachineCol1[rdm];
            rdm = Main.rand.Next(0, 9);
            SlotMachineSymbols middleSymbolCol2 = slotMachineCol2[rdm];
            rdm = Main.rand.Next(0, 9);
            SlotMachineSymbols middleSymbolCol3 = slotMachineCol3[rdm];
            return true;
        }
    }
}