using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;
using System.Text.RegularExpressions;
using System.IO;

namespace RenameHistogramdiffs
{
    class Program
    {
         
        int new_width = 16; 
        int new_height = 16;

        double difference = 0;
        Double prevDoub = 0;
        double thisDoub = 0;
        string filenameStr = "";


        public double[] Histogram1(Bitmap sourceImage)
        {
            double[] RGBColor = new double[512];
            int width = sourceImage.Width, height = sourceImage.Height;
            byte Red, Green, Blue;
            Color pixelColor;

            for (int i = 0, j; i < width; ++i)
            {
                for (j = 0; j < height; ++j)
                {
                    pixelColor = sourceImage.GetPixel(i, j);
                    Red = pixelColor.R;
                    Green = pixelColor.G;
                    Blue = pixelColor.B;

                    int quantColor = ((Red / 32) * 64) + ((Green / 32) * 8) + (Blue / 32);

                    ++RGBColor[quantColor];
                }
            }

            double normalizationFactor = width * height;
            for (int i = 0; i < RGBColor.Length; i++)
            {
                RGBColor[i] = RGBColor[i] / normalizationFactor;
            }

            return RGBColor;
        }

        public Bitmap resizeImage(Bitmap image)
        {
            Bitmap new_image = new Bitmap(new_width, new_height);
            Graphics g = Graphics.FromImage(new_image);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            g.DrawImage(image, 0, 0, new_width, new_height);
            return new_image;
        }
        private Bitmap ResizeBitmap(Bitmap sourceBMP)
        {
            Bitmap result = new Bitmap(new_width, new_height);
            using (Graphics g = Graphics.FromImage(result))
                g.DrawImage(sourceBMP, 0, 0, new_width, new_height);
            return result;
        }
        
        public void SetThisDoub(double numb)
        {
            thisDoub = numb;

        }
        public void SetPrevDoub()
        {
            prevDoub = thisDoub;

        }
        public double GetDiffDoub()
        {
            double absDoub;

            absDoub = Math.Abs(prevDoub - thisDoub);
            return (absDoub);
        }
        public void SetFilename(double input)
        {

            filenameStr += input.ToString();

        }

        public string CleanFileName(string value)
        {
            // remove all "0," from the string.

            String cleanFilenamePass1 = Regex.Replace(filenameStr, @"\d[,]", "");
            String cleanFilenamePass2 = Regex.Replace(cleanFilenamePass1, @"E", "");
            String cleanFilenamePass3 = Regex.Replace(cleanFilenamePass1, @"-", "");

            return cleanFilenamePass3;
        }

        public string GetFilename()
        {
            return filenameStr;
        }

        public void RenameFiles(string inputStr)
        {
            Bitmap bm2 = new Bitmap(inputStr);

            Bitmap bm = new Bitmap(bm2, 16, 16);
            double[] doubleArr = Histogram1(bm);
            foreach (double d in doubleArr)
            {
                if (d == 0)
                {
                    // we don't care about the empty/0 histograms
                }

                else
                {
                    SetThisDoub(d);
                    difference = GetDiffDoub();
                    SetFilename(difference);
                }

                SetPrevDoub();
            }
            var newFileName = CleanFileName(GetFilename());
            int MaxLength = 100;

            if (newFileName.Length > MaxLength)
                newFileName = newFileName.Substring(0, MaxLength);
            string outputname = newFileName + ".jpg";
            // Console.WriteLine("ren " + args[0] + " " + newFileName + ".jpg");
            bm.Dispose();
            bm2.Dispose();

            try
            {
                File.Move(inputStr, outputname);
            }
            catch
            {
                // The Parallel.ForEach function seems to have an issue with my way of executing.
                // It gives Execption IO errors, the file is in use by another process. 
                // This way those exceptions are ignored and the process can continue through the set of .bat files. 
                // This assumes that the process completes in order, which it should because if the file is not deleted then it can be re-run.
                // If that is not the case, I'll figure something else out. 
            }

        }

        static void Main(string[] args)
        {
            Program p = new Program();
            var paths = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.bat");





            string inputString = args[0];
            p.RenameFiles(inputString);




        }
        
    }
}
