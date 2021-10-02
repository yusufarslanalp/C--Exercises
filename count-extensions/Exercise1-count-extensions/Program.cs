using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Exercise1_count_extensions
{
    class Program
    {
        static List<FileInfo> allFiles = new List<FileInfo>();

        static void Main(string[] args)
        {
            string path = @"C:\Program Files\dotnet";
            getAllFiles( path );
            var filesStarthWithM = allFiles.Where( file => file.Name[0] == 'M' );

            var allExtensions = filesStarthWithM.Select(s => s.Extension).Distinct().OrderBy( ext => ext );

            int count;
            foreach ( string ext in allExtensions )
            {
                count = filesStarthWithM.Where( file => file.Extension == ext ).Count();
                Console.WriteLine( "{0}: {1}", ext, count );
            }
        }


        static void getAllFiles( string dirPath )
        {
            DirectoryInfo currentDir = new DirectoryInfo( dirPath );
            allFiles.AddRange(currentDir.GetFiles());

            string[] subDirPaths = Directory.GetDirectories( dirPath );

            foreach ( string subDirPath in subDirPaths )
            {
                getAllFiles( subDirPath );
            }
        }
    }
}
