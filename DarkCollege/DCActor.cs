using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DarkCollege
{
    class DCActor
    {
        //--------Class types-------------//

        public enum direction
        {
            right,
            down,
            left,
            up
        }

        //--------Class variables---------//
        private static int x = 0;
        private static int y = 0;
        private static int maxSprites;
        private static List<Bitmap> spriteSet = new List<Bitmap>();        //sprite set
        private static int currImage = 1;    //current sprite image in animation series
        private static direction currDirection = direction.right;

        //---------Class properties--------//

        public int xPos     //property holds current x position
        {
            get { return x; }
            set { x = value; }
        }

        public int yPos     //property holds current x position
        {
            get { return y; }
            set { y = value; }
        }

        public Bitmap currentImage  //property holds the current animation sprite for the character
        {
            get { return getCurrentImage(); }
        }

        public direction currentDirection   //property holds the current direction faced by the character
        {
            get { return currDirection; }
        }
       //--------Class functions--------//

       public DCActor(int startX, int startY, String gameSpriteName, int numSprites)        //constructor sets first image and starting position
       {
           x = startX;
           y = startY;
           loadSpriteSet(gameSpriteName, numSprites);
           maxSprites = numSprites;
           currImage = 1;
           currDirection = direction.right;
       }

       //load individual sprites from files into list
       private void loadSpriteSet(String spriteName, int numSprites)
       {
           //add right facing sprites
           for (int i = 0; i < numSprites; i++)
           {
               int currSpriteNum = i + 1;
               String resourcePath = "..\\..\\Resources\\knightSpriteR" + (i+1).ToString() + ".png";
               Bitmap thisSprite = new Bitmap(Image.FromFile(resourcePath));
               spriteSet.Add(thisSprite);
           }
           //add down facing sprites
           for (int i = 0; i < numSprites; i++)
           {
               int currSpriteNum = i + 1;
               String resourcePath = "..\\..\\Resources\\knightSpriteD" + (i+1).ToString() + ".png";
               Bitmap thisSprite = new Bitmap(Image.FromFile(resourcePath));
               spriteSet.Add(thisSprite);
           }
           //add left facing sprites
           for (int i = 0; i < numSprites; i++)
           {
               int currSpriteNum = i + 1;
               String resourcePath = "..\\..\\Resources\\knightSpriteL" + (i+1).ToString() + ".png";
               Bitmap thisSprite = new Bitmap(Image.FromFile(resourcePath));
               spriteSet.Add(thisSprite);
           }
           //add up facing sprites
           for (int i = 0; i < numSprites; i++)
           {
               int currSpriteNum = i + 1;
               String resourcePath = "..\\..\\Resources\\knightSpriteU" + (i+1).ToString() + ".png";
               Bitmap thisSprite = new Bitmap(Image.FromFile(resourcePath));
               spriteSet.Add(thisSprite);
           }
       }

       private Bitmap getCurrentImage()     //return the bitmap for the image that should be showing at the moment
       {
           Bitmap thisSprite = spriteSet.ElementAt(currImage - 1);
           return thisSprite;
       }

        /*----------Class functions---------*/

       public void move(int xMove, int yMove)   //move actor one place in given direction xDirection takes priority
       {
           //turn right if necessary and change to next image when actor is moving to right
           if (xMove > 0 && x < Game.LEVEL_WIDTH - 1)
           {
               x = x + xMove;
               if (currDirection != direction.right)
               {
                   currDirection = direction.right;
                   currImage = 1;
               }
               else
               {
                   if (currImage < maxSprites)
                       currImage++;
                   else
                       currImage = 1;
               }
           }
           else if (xMove < 0 && x > 0)   //turn left if necessary and change to next image when actor is moving to left
           {
               x = x + xMove;
               if (currDirection != direction.left)
               {
                   currDirection = direction.left;
                   currImage = 9;
               }
               else
               {
                   if (currImage > 9)
                       currImage--;
                   else
                       currImage = 9 + maxSprites - 1;
               }
           }          
           else if (yMove > 0 && y < Game.LEVEL_HEIGHT - 1) //turn down if necessary and change to next image when actor is moving to down
           {
               y = y + yMove;
               if (currDirection != direction.down)
               {
                   currDirection = direction.down;
                   currImage = 5;
               }
               else
               {
                   if (currImage < 5 + maxSprites - 1)
                       currImage++;
                   else
                       currImage = 5;
               }
           }
           else if (yMove < 0 && y > 0)   //turn up if necessary and change to next image when actor is moving to up
           {
               y = y + yMove;
               if (currDirection != direction.up)
               {
                   currDirection = direction.up;
                   currImage = 13;
               }
               else
               {
                   if (currImage > 13)
                       currImage--;
                   else
                       currImage = 13 + maxSprites - 1;
               }
           }
       }
    }
}
