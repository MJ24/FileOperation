using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileOperation
{
    class Program
    {
        static void Main(string[] args)
        {
            //CopyFileByFileStream(@"D:\Test\555.avi", @"D:\Test\666.avi");

            CopyFileByStreamReaderWriter(@"D:\Test\office365.txt", @"D:\Test\O365.txt");

            Console.WriteLine("复制成功");
            Console.ReadKey();
        }

        private static void CopyFileByFileStream(string sourceFilePath, string targetFilePath)
        {
            using (FileStream fsRead = new FileStream(sourceFilePath, FileMode.Open, FileAccess.Read))
            {
                using (FileStream fsWrite = new FileStream(targetFilePath, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    while (true)
                    {
                        byte[] buffer = new byte[1024 * 1024 * 5];
                        //注意FileStream.Read第2个参数是指把文件读出来的stream写入buffer数组时，从哪里开始写
                        //而不是说从文件读时从哪里开始读，第3个参数是指往buffer数组一次最多写入多少
                        //返回值index是指本次实际上读了多少，前几次都是能读的最大值buffer.Length，最后一次就不到此值了
                        int index = fsRead.Read(buffer, 0, buffer.Length);
                        if (index == 0)
                        {
                            break;
                        }
                        //FileStream.Write的第2个参数是指从buffer数组往stream里考数据时，从buffer数组的哪里开始考
                        //第3个参数是指从buffer数组一次最多考多少，因为本次只读了index，所以最多只写index而不是buffer.Length
                        fsWrite.Write(buffer, 0, index);
                    }
                }
            }
        }

        private static void CopyFileByStreamReaderWriter(string sourceFilePath, string targetFilePath)
        {
            using (StreamReader sr = new StreamReader(sourceFilePath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    string temp = sr.ReadLine();
                    //StreamWriter()第二个bool值的参数代表不覆盖写入而是apend写入
                    using (StreamWriter sw = new StreamWriter(targetFilePath, true))
                    {
                        sw.WriteLine(temp);
                    }
                }
            }
        }
    }
}