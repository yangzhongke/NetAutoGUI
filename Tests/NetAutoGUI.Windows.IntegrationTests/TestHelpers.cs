﻿using System;
using System.Collections.Generic;
namespace NetAutoGUI.Windows.UnitTests
{
    internal class TestHelpers
    {
        public static string GetSolutionRootDirectory()
        {
            string currentDir = Directory.GetCurrentDirectory();

            while (!string.IsNullOrEmpty(currentDir))
            {
                if (Directory.GetFiles(currentDir, "*.sln").Length > 0||
                    Directory.GetFiles(currentDir, "*.slnx").Length > 0)
                {
                    return currentDir;
                }
                currentDir = Directory.GetParent(currentDir)?.FullName;
            }

            throw new Exception("Solution root not found.");
        }

        public static string FindFile(string root, string fileName)
        {
            try
            {
                foreach (string file in Directory.EnumerateFiles(root, fileName, SearchOption.AllDirectories))
                {
                    return file; // Return first occurrence
                }
            }
            catch (UnauthorizedAccessException) { }
            catch (IOException) { }

            throw new Exception($"{fileName} not found.");
        }
    }
}
