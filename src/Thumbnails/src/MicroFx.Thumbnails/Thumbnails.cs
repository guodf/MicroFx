using FFmpeg.AutoGen;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace MicroFx.Thumbnails
{
    public class Thumbnails
    {
        static Thumbnails()
        {
            FFmpegBinariesHelper.RegisterFFmpegBinaries();
        }

        public static bool FromIamge(string srcFile, string outFile,Size size)
        {
            using (var bitmap = SKBitmap.Decode(srcFile))
            {
                if (bitmap.Height < bitmap.Width)
                {
                    size.Width = bitmap.Width * size.Height / bitmap.Height;
                    
                }
                else
                {
                    size.Height = bitmap.Height * size.Width / bitmap.Width;
                }
                
                using (var newBitmap = new SKBitmap(new SKImageInfo(size.Width,size.Height)))
                {
                    bitmap.ScalePixels(newBitmap, SKFilterQuality.Medium);
                    using (var image = SKImage.FromBitmap(newBitmap))
                    {
                        using (var output = File.OpenWrite(outFile))
                        {
                            image.Encode(SKEncodedImageFormat.Jpeg, 100)
                                .SaveTo(output);
                            return true;
                        }
                    }
                }
            }
        }

        public static bool FromGif(string srcFile, string outFile, Size size)
        {
            using (var sKCodec = SKCodec.Create(srcFile))
            {
                using (var bitmap = new SKBitmap(sKCodec.Info))
                {
                    if (bitmap.Height > bitmap.Width)
                    {
                        size.Width = bitmap.Width * size.Height / bitmap.Height;
                    }
                    else
                    {
                        size.Height = bitmap.Height * size.Width / bitmap.Width;
                    }
                    sKCodec.GetPixels(sKCodec.Info, bitmap.GetPixels(), new SKCodecOptions(0));
                    using (var newBitmap = new SKBitmap(new SKImageInfo(size.Width, size.Height)))
                    {
                        bitmap.ScalePixels(newBitmap, SKFilterQuality.Medium);
                        using (var image = SKImage.FromBitmap(newBitmap))
                        {
                            using (var output = File.OpenWrite(outFile))
                            {
                                image.Encode(SKEncodedImageFormat.Jpeg, 100)
                                    .SaveTo(output);
                                return true;
                            }
                        }
                    }

                }
            }
        }

        public unsafe static bool FromVideo(string srcFile,string outFile)
        {
            ffmpeg.av_log_set_level(ffmpeg.AV_LOG_VERBOSE);

            // do not convert to local function
            av_log_set_callback_callback logCallback = (p0, level, format, vl) =>
            {
                if (level > ffmpeg.av_log_get_level()) return;

                var lineSize = 1024;
                var lineBuffer = stackalloc byte[lineSize];
                var printPrefix = 1;
                ffmpeg.av_log_format_line(p0, level, format, vl, lineBuffer, lineSize, &printPrefix);
                var line = Marshal.PtrToStringAnsi((IntPtr)lineBuffer);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(line);
                Console.ResetColor();
            };

            ffmpeg.av_log_set_callback(logCallback);

            using (var vsd = new VideoStreamDecoder(srcFile))
            {
                Console.WriteLine($"codec name: {vsd.CodecName}");

                var info = vsd.GetContextInfo();
                info.ToList().ForEach(x => Console.WriteLine($"{x.Key} = {x.Value}"));

                var sourceSize = vsd.FrameSize;
                var sourcePixelFormat = vsd.PixelFormat;
                var destinationSize = sourceSize;
                var destinationPixelFormat = AVPixelFormat.AV_PIX_FMT_BGR24;
                using (var vfc = new VideoFrameConverter(sourceSize, sourcePixelFormat, destinationSize, destinationPixelFormat))
                {
                    //获取第一帧
                    if (vsd.TryDecodeNextFrame(out var frame))
                    {
                        var convertedFrame = vfc.Convert(frame);                       
                        using (var bitmap = new Bitmap(convertedFrame.width, convertedFrame.height, convertedFrame.linesize[0], PixelFormat.Format24bppRgb, (IntPtr)convertedFrame.data[0]))
                        {                 
                            bitmap.Save(outFile, ImageFormat.Jpeg);
                            return true;
                        }      
                    }
                    //var frameNumber = 0;
                    //while (vsd.TryDecodeNextFrame(out var frame))
                    //{
                    //    var convertedFrame = vfc.Convert(frame);

                    //    using (var bitmap = new Bitmap(convertedFrame.width, convertedFrame.height, convertedFrame.linesize[0], PixelFormat.Format24bppRgb, (IntPtr)convertedFrame.data[0]))
                    //        bitmap.Save($"frame.{frameNumber:D8}.jpg", ImageFormat.Jpeg);

                    //    Console.WriteLine($"frame: {frameNumber}"); 
                    //    frameNumber++;
                    //}
                }
            }
            return false;
        }
    }
}
