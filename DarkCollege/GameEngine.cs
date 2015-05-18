using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DarkCollege
{
    class GameEngine
    {
           /*-------Class variables---------*/

        private Graphics gameGraphics;
        private Bitmap castleFloor;
        private Bitmap DCFood;
        private Bitmap DCEnemy;
        private Bitmap DCObstacle;
        private Bitmap DCOpenObstacle;

        //background rectangle and brush colour
        Rectangle canvasRect;
        SolidBrush blackBrush;

        /*-------Class functions------*/

        public GameEngine(Graphics g) //constructor
        {
            gameGraphics = g;
        }

        public void initialise()    //load graphics assets and start rendering engine
        {
            canvasRect = new Rectangle(0, 0, Game.CANVAS_WIDTH, Game.CANVAS_HEIGHT);
            blackBrush = new SolidBrush(Color.Black);
            loadAssets();
        }

        private void loadAssets()   //load assets for game - images and sounds
        {
            castleFloor = DarkCollege.Properties.Resources.castleFloor;
            DCFood = DarkCollege.Properties.Resources.food;
            DCEnemy = DarkCollege.Properties.Resources.obstacle;
            DCObstacle = DarkCollege.Properties.Resources.obstacle;
            DCOpenObstacle = DarkCollege.Properties.Resources.obstacleOpen;
        }
        
       public void render(DCActor mainCharacter, int offsetX, int offsetY)    //draws graphics frame by frame, background, enemies, main character
       {
            Bitmap frame = new Bitmap(Game.CANVAS_WIDTH, Game.CANVAS_HEIGHT);  //clear the buffer frame
            Graphics frameGraphics = Graphics.FromImage(frame);
            Bitmap mainActorImage = mainCharacter.currentImage;
    
           //colour in the background
           frameGraphics.FillRectangle(blackBrush, canvasRect);

           //draw grid objects into new frame
            Game.gridObjects[,] levelObjects = DClevel.levelMap;   //get a copy of the current grid

            for (int x = 0; x < Game.CANVAS_WIDTH / Game.TILE_SIZE; x++)
            {
                for (int y = 0; y < Game.CANVAS_HEIGHT /Game.TILE_SIZE; y++)
                {
                    switch (levelObjects[x + offsetX, y + offsetY])
                    {
                        case Game.gridObjects.empty:
                            frameGraphics.DrawImage(castleFloor, x * Game.TILE_SIZE, y * Game.TILE_SIZE);
                            break;
                        case Game.gridObjects.food:
                            frameGraphics.DrawImage(DCFood, x * Game.TILE_SIZE, y * Game.TILE_SIZE);
                            break;
                        case Game.gridObjects.obstacle:
                            frameGraphics.DrawImage(DCObstacle, x * Game.TILE_SIZE, y * Game.TILE_SIZE);
                            break;
                        case Game.gridObjects.obstacleOpen:
                            frameGraphics.DrawImage(DCOpenObstacle, x * Game.TILE_SIZE, y * Game.TILE_SIZE);
                            break;
                    }
                }
            }

            //draw main character in viewport
            frameGraphics.DrawImage(mainActorImage, (mainCharacter.xPos - offsetX) * Game.TILE_SIZE, (mainCharacter.yPos - offsetY) * Game.TILE_SIZE);


            //draw level
            gameGraphics.DrawImage(frame, 0, 0);
            frame.Dispose();
            frameGraphics.Dispose();
        }

       ~GameEngine()
       {
           blackBrush.Dispose();
       }

    }
}
