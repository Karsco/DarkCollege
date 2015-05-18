using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Media;

namespace DarkCollege
{
    class Game
    {
        /*----------Constant values----------*/
        public const int CANVAS_HEIGHT = 600;
        public const int CANVAS_WIDTH = 800;
        public const int LEVEL_WIDTH = 32;
        public const int LEVEL_HEIGHT = 24;
        public const int TILE_SIZE = 50;
        public const int VP_X_LIMIT = CANVAS_WIDTH / (TILE_SIZE * 2);
        public const int VP_Y_LIMIT = CANVAS_HEIGHT / (TILE_SIZE * 2);


        /*----------Class types------------*/

        public enum gridObjects      //components in a level
        {
            empty,
            enemy,
            obstacle,
            obstacleOpen,
            food
        }


        /*----------Class variables----------*/

        private GameEngine gEngine;
        private DCActor mainCharacter;
        private static int health;
        private static int viewPortOffSetX = 0;
        private static int viewPortOffSetY = 0;

        private SoundPlayer foodSound = new SoundPlayer(DarkCollege.Properties.Resources.food1);
        private SoundPlayer obstacleSound = new SoundPlayer(DarkCollege.Properties.Resources.obstacle1);


        /*----------Class functions---------*/

        public void loadLevel() //set up level grid and main character
        {
            DClevel.initialiseLevel();  //add an empty grid
            DClevel.placeActors(gridObjects.food, 4);   //add 4 sacks of food to the game
            DClevel.placeActors(gridObjects.obstacle, 8);   //add 8 trapdoors to the game
            health = 100;  //starting health value;
        }

        //main character operates on a different layer so deal with it separately
        public void loadMainCharacter()
        {
            mainCharacter = new DCActor(0, 11, "knightSprite", 4); //add a new actor at position 0,0 in grid
        }

        public void startGraphics(Graphics g)   //start graphics engine
        {
            loadLevel();
            loadMainCharacter();
            gEngine = new GameEngine(g);
            gEngine.initialise();
        }

        public void changeGameState(Keys keyPressed)
        {
            int xDirection = 0;
            int yDirection = 0;
            switch (keyPressed)
            {
                case Keys.Right:
                    xDirection = 1;
                    break;
                case Keys.Left:
                    xDirection = -1;
                    break;
                case Keys.Up:
                    yDirection = -1;
                    break;
                case Keys.Down:
                    yDirection = 1;
                    break;
                case Keys.Space:
                    openTrapDoor();
                    break;
            }
            mainCharacter.move(xDirection, yDirection);   //move character using current values of xDirection and yDirection
            removeFood();
        }

        public void reDrawGraphics(Graphics g)   //start graphics engine
        {
            reCalculateViewPort();
            gEngine.render(mainCharacter, viewPortOffSetX, viewPortOffSetY);
        }

        private void reCalculateViewPort()
        {
            //move viewPort as long as it is still within limits of level
            if (mainCharacter.xPos + VP_X_LIMIT > LEVEL_WIDTH)
            {
                viewPortOffSetX = LEVEL_WIDTH - (CANVAS_WIDTH / TILE_SIZE);
            }
            else
            {
                if (mainCharacter.xPos - VP_X_LIMIT > 0)
                {
                    viewPortOffSetX = mainCharacter.xPos - VP_X_LIMIT;
                }
                else
                {
                    viewPortOffSetX = 0;
                }
            }

            //move viewPort as long as it is still within limits of level
            if (mainCharacter.yPos + VP_Y_LIMIT > LEVEL_HEIGHT)
            {
                viewPortOffSetY = LEVEL_HEIGHT - (CANVAS_HEIGHT / TILE_SIZE);
            }
            else
            {
                if (mainCharacter.yPos - VP_Y_LIMIT > 0)
                {
                    viewPortOffSetY = mainCharacter.yPos - VP_Y_LIMIT;
                }
                else
                {
                    viewPortOffSetY = 0;
                }
            }
        }

        private void removeFood()
        {
            //remove food if in same space
            Game.gridObjects[,] levelObjects = DClevel.levelMap;   //get a copy of the current grid
            if (levelObjects[mainCharacter.xPos, mainCharacter.yPos] == Game.gridObjects.food)
            {
                levelObjects[mainCharacter.xPos, mainCharacter.yPos] = Game.gridObjects.empty;
                foodSound.Play();
                updateHealth(+10);
            }
        }

        private void openTrapDoor()
        {
            Game.gridObjects[,] levelObjects = DClevel.levelMap;   //get a copy of the current grid
            int x = mainCharacter.xPos;
            int y = mainCharacter.yPos;
            if (mainCharacter.currentDirection == DCActor.direction.left)
            {
                    x--;
            }
            else if(mainCharacter.currentDirection == DCActor.direction.right)
            {
                    x++;
            }
            else if(mainCharacter.currentDirection == DCActor.direction.up)
            {
                    y--;
            }
            else if(mainCharacter.currentDirection == DCActor.direction.down)
            {
                    y++;
            }
            if (levelObjects[x, y] == Game.gridObjects.obstacle)
                levelObjects[x, y] = Game.gridObjects.obstacleOpen;
            updateHealth(-10);
        }

        public int updateHealth(int adjustment)
        {
            health = health + adjustment;
            return health;
        }
    }
}
