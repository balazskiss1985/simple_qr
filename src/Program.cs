using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;
using ZXing.QrCode;

namespace simple_qr
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length == 0)
            {
                Console.WriteLine("");
                Console.WriteLine("Welcome to a simple implementation of the zxing.net application.");
                Console.WriteLine("The application will help you generate or read qr codes.");
                Console.WriteLine("");
                Console.WriteLine("Usage: simple_qr [options] [path-to-image]");
                Console.WriteLine("");
                Console.WriteLine("Options:");
                Console.WriteLine(" -e |--example                          Display example.");
                Console.WriteLine(" -c |--create [text] [path-to-image]    Create QR Image.");
                Console.WriteLine(" -r |--read [path-to-image]             Read QR Image.");
            }
            else
            {
                if (args[0].ToString() == "-e" || args[0].ToString() == "--example")
                {
                    Console.WriteLine("");
                    Console.WriteLine("Create QR image example as follows:");
                    Console.WriteLine("simpel_qr --create \"Type your text here.\" \"c:\\image\\create_qr_code.png\"");
                    Console.WriteLine("");
                    Console.WriteLine("Read QR image example as follows:");
                    Console.WriteLine("simple_qr --read \"c:\\image\\read_qr_code.png\"");
                    Console.WriteLine("");
                }
                else if (args[0].ToString() == "-c" || args[0].ToString() == "--create")
                {
                    if(args[1].Length == 0 || args[0].Length == 0)
                    {
                        Console.WriteLine("No text and image path specified for creating the png file.");
                    }
                    else
                    {
                        try
                        {
                            var options = new QrCodeEncodingOptions
                            {
                                DisableECI = true,
                                CharacterSet = "UTF-8",
                                Width = 250,
                                Height = 250,
                            };
                            var writer = new BarcodeWriter();
                            writer.Format = BarcodeFormat.QR_CODE;
                            writer.Options = options;

                            var result = new Bitmap(writer.Write(args[1].ToString()));

                            result.Save(args[2], ImageFormat.Png);

                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Error creating the png file.");
                        }

                    }


                }
                else if (args[0].ToString() == "-r" || args[0].ToString() == "--read")
                {
                    if(args[1].Length == 0)
                    {
                        Console.WriteLine("No image path specified for reading the png file.");
                    }
                    else
                    {
                        try
                        {
                            // create a barcode reader instance
                            IBarcodeReader reader = new BarcodeReader();
                            // load a bitmap

                            var barcodeBitmap = (Bitmap)System.Drawing.Image.FromFile(args[1].ToString());

                            // detect and decode the barcode inside the bitmap
                            var result = reader.Decode(barcodeBitmap);
                            // do something with the result
                            if (result != null)
                            {
                                Console.WriteLine(result.Text);
                            }

                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Error reading the png file.");
                        }

                    }

                }
                else
                {
                    Console.WriteLine("Unknown Command, please try \"qr_reader --example\"");
                }
            }
        }
    }
}
