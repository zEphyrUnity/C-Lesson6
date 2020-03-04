using System;
using System.IO;
using System.Diagnostics;
using System.Text;

namespace CopySamples
{
    class Program
    {
        //Запись значений типа байт в двоичном виде
        static long FileStreamSample(string filename, long size)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write);

            for (int i = 0; i < size; i++)
            {
                fs.WriteByte((byte)i);
            }

            fs.Close();
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        //Чтение в массив значений типа байт из двоичного файла
        static byte[] FileStream(string filename, long size)
        {
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            byte[] fileArray = new byte[size];

            for (int i = 0; i < size; i++)
            {
                fileArray[i] = br.ReadByte();
            }

            fs.Close();
            br.Close();
            return fileArray;
        }
        //-------------------------------------------------------------------------------
        //Запись значений типа interger в двоичном виде
        static long BinaryStreamSample(string filename, long size)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs);

            for (int i = 0; i < size; i++)
            {
                bw.Write(i);
            }

            fs.Close();
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }
        //Чтение в массив значений типа integer из двоичного файла
        static int[] BinaryStream(string filename, long size)
        {
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            int[] fileInt = new int[size];

            for (int i = 0; i < size; i++)
            {
                fileInt[i] += br.ReadInt32();
            }

            fs.Close();
            br.Close();
            return fileInt;
        }
        //-------------------------------------------------------------------------------
        //Запись значений строкового типа
        static long StreamWriterSample(string filename, long size)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);

            for (int i = 0; i < size; i++)
            {
                sw.Write(0);
            }

            sw.Flush();
            sw.Close();
            fs.Close();
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }
        //Чтение значений строкового типа
        static string StreamReader(string filename, long size)
        {
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            string fileString = "";
            
            for(int i = 0; i < size; i++)
            {
                fileString += sr.ReadLine();
            }

            sr.Close();
            return fileString;
        }
        //-------------------------------------------------------------------------------
        //Буферизированная запись в файл значений типа байт
        static long BufferedStreamSample(string filename, long size)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write);

            int countPart = 4;//количество частей
            int bufsize = (int)(size / countPart);
            byte[] buffer = new byte[size];

            BufferedStream bs = new BufferedStream(fs, bufsize);
            //bs.Write(buffer, 0, (int)size);//Error!

            for (int i = 0; i < countPart; i++)
            {
                bs.Write(buffer, 0, (int)bufsize);
            }

            fs.Close();
            stopwatch.Stop();

            return stopwatch.ElapsedMilliseconds;
        }
        //Буферизированное чтение из файла значений типа байт
        static byte[] BufferedStream(string filename, long size)
        {
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);

            int countPart = 4;//количество частей
            int bufsize = (int)(size / countPart);
            byte[] buffer = new byte[size];

            BufferedStream bs = new BufferedStream(fs, bufsize);
            BinaryReader br = new BinaryReader(bs);

            byte[] fileArray = new byte[size];
            for(int i = 0; i < countPart; i++)
            {
                fileArray[i] = br.ReadByte();
            }

            fs.Close();
            bs.Close();
            br.Close();

            return fileArray;
        }

        static void Main(string[] args)
        {
            long kbyte = 1024;
            long mbyte = 1024 * kbyte;
            long gbyte = 1024 * mbyte;
            long size = mbyte;

            string path1 = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\bigdata0.bin";
            string path2 = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\bigdata3.bin";
            string path3 = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\bigdata2.bin";
            string path4 = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\bigdata1.bin";

            Console.WriteLine("FileStream. Milliseconds:{0}", FileStreamSample("..\\..\\bigdata0.bin", size));                          //Write FileStream
            Console.WriteLine("BinaryStream. Milliseconds:{0}", BinaryStreamSample("..\\..\\bigdata1.bin", size));                      //Write BinaryStream
            Console.WriteLine("StreamWriter. Milliseconds:{0}", StreamWriterSample("..\\..\\bigdata2.bin", size));                      //Write StreamReader/StreamWriter
            Console.WriteLine("BufferedStream. Milliseconds:{0}", BufferedStreamSample("..\\..\\bigdata3.bin", size));                  //Write BufferedStream

            var byteFileArray = FileStream(path1, size);                                                                                //Read FileStream
            var byteBufferedArray = BufferedStream(path2, size);                                                                        //Read BufferedStream
            var fileString = StreamReader(path3, size);                                                                                 //Read StreamReader
            var fileInt = BinaryStream(path4, size);                                                                                    //Read BinaryStream                   

            //foreach (byte num in byteBufferedArray)
            //{
            //    Console.Write($"{num} |");
            //}

            //for (int i = 0; i < size; i++)
            //{
            //    Console.Write($"{fileInt[i]} ");
            //}

            Console.WriteLine(fileString);

            Console.ReadKey();
        }
    }
}

