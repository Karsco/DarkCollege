using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DarkCollege
{
    class DClevel
    {
        private static Random co_ords = new Random();
        private static Game.gridObjects[,] map = new Game.gridObjects[Game.LEVEL_WIDTH, Game.LEVEL_HEIGHT];

        public static Game.gridObjects[,] levelMap //this is an external property that can be accessed by the graphics engine
        {
            get { return map; }
            set { map = value; }
        }

        public static void initialiseLevel()
        {
            //make all blocks in grid empty at start as this is likely to be the majority
            for (int x = 0; x < Game.LEVEL_WIDTH; x++)
            {
                for (int y = 0; y < Game.LEVEL_HEIGHT; y++)
                {
                    map[x, y] = Game.gridObjects.empty;
                }
            }
        }

        //place a given number of the specified type of object in the grid - positions are randomly generated
        public static void placeActors(Game.gridObjects objectType, int numObjects)
        {
            for (int i = 0; i < numObjects; i++)
            {
                int x = co_ords.Next(0, Game.LEVEL_WIDTH);
                int y = co_ords.Next(0, Game.LEVEL_HEIGHT);
                map[x, y] = objectType;
            }
        }

        ~DClevel()
        {

        }

    }
}
