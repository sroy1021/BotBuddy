using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;


namespace BotBuddy.SanjayProsadRoy
{
    public class ImgAction
    {
        private static string ImageFile { get; set; }

        public ImgAction(string imageFile)
        {

            ImageFile = imageFile;

        }

        public Point GetImageLocation()
        {
            //CompareBigAndSmallBitmaps();
            var bmpSmall = (Bitmap)Image.FromFile(ImageFile);
            var bmpBig = CaptureScreen();
            Point location;
            FindBitmap(bmpSmall, bmpBig, out location);

            return location;


        }

        private static Bitmap CaptureScreen()
        {
            var image = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);
            var gfx = Graphics.FromImage(image);
            gfx.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
            return image;
        }


        /// <summary>
        /// Find the location of a bitmap within another bitmap and return if it was successfully found
        /// </summary>
        /// <param name="bmpNeedle">The image we want to find</param>
        /// <param name="bmpHaystack">Where we want to search for the image</param>
        /// <param name="location">Where we found the image</param>
        /// <returns>If the bmpNeedle was found successfully</returns>
        private bool FindBitmap(Bitmap bmpNeedle, Bitmap bmpHaystack, out Point location)
        {
            if (bmpNeedle == null || bmpHaystack == null)
            {
                location = new Point();
                return false;
            }
            for (int outerX = 0; outerX < bmpHaystack.Width - bmpNeedle.Width; outerX++)
            {
                for (int outerY = 0; outerY < bmpHaystack.Height - bmpNeedle.Height; outerY++)
                {
                    for (int innerX = 0; innerX < bmpNeedle.Width; innerX++)
                    {
                        for (int innerY = 0; innerY < bmpNeedle.Height; innerY++)
                        {
                            Color cNeedle = bmpNeedle.GetPixel(innerX, innerY);
                            Color cHaystack = bmpHaystack.GetPixel(innerX + outerX, innerY + outerY);

                            if (cNeedle.R != cHaystack.R || cNeedle.G != cHaystack.G || cNeedle.B != cHaystack.B)
                            {
                                goto notFound;
                            }
                        }
                    }
                    location = new Point(outerX, outerY);
                    return true;
                    notFound:
                    continue;
                }
            }
            location = Point.Empty;
            return false;
        }


        //private static void CompareBigAndSmallBitmaps()
        //{
        //    var bmpBig = CaptureScreen();              //(Bitmap) Image.FromFile();
        //  var bmpSmall = (Bitmap) Image.FromFile(ImageFile);
        //  for (var offX = 0; offX < bmpBig.Width - bmpSmall.Width; offX++)
        //  {
        //    for (var offY = 0; offY < bmpBig.Height - bmpSmall.Height; offY++)
        //    {
        //      var percentage = CompareSmallBitmaps(bmpBig, bmpSmall, offX, offY);
        //      if (percentage > 98.0)  // define percentage of equality
        //      {
        //        // Aha... found something here....and exit here if you want
        //          MessageBox.Show("Location : " + offX.ToString() + offY.ToString());
        //      }
        //    }
        //  }
        //}



        //private static double CompareSmallBitmaps(Bitmap bmpBig, Bitmap bmpSmall, int offX, int offY)
        //{
        //  var equals = 0;
        //  for (var x = 0; x < bmpSmall.Width; x++)
        //  {
        //    for (var y = 0; y < bmpSmall.Height; y++)
        //    {
        //      var color1 = bmpBig.GetPixel(x + offX, y + offY).ToArgb();
        //      var color2 = bmpSmall.GetPixel(x, y).ToArgb();
        //      if (color1 == color2)
        //      {
        //        equals++;
        //      }
        //    }
        //  }
        //  return (Convert.ToDouble(equals)/Convert.ToDouble(bmpSmall.Width*bmpSmall.Height))*100.0;
        //}




    }
}
