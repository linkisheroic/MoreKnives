﻿namespace MoreKnives.Common
{
    using System;

    public class RoseLogger
    {
        /*
         * General messages or generic errors
         * Thank you SeraphimRisen for the example code for the logger.
         */
        public static void Message(string modName, string message)
        {
            Console.WriteLine(modName + " " + message);
        }

        public static void GenericError(string modName, Exception ex)
        {
            Console.WriteLine(modName + " ERROR: " + ex);
        }

        /*
         * File I/O related errors
         */
        public static void ConfigNotFound(string modName)
        {
            Console.WriteLine(modName + " Config file not found. Creating default value.");
        }

        public static void ConfigReadSuccess(string modName)
        {
            Console.WriteLine(modName + " Config file found and read successfully");
        }

        public static void ConfigReadError(string modName, Exception ex)
        {
            Console.WriteLine(modName + " Error reading file. Setting defaults. Exception: " + ex);
        }

        public static void SeralizerFailed(string file, Exception ex)
        {
            Console.WriteLine("File I/O Error: " + file + " Exception: " + ex);
        }

        /*
         * Patch() errors
         */
        public static void PatchStart(string modName, string version)
        {
            Console.WriteLine(modName + " Start patching. Version: " + version);
        }

        public static void PatchComplete(string modName)
        {
            Console.WriteLine(modName + " Patching complete.");
        }

        public static void PatchFailed(string modName, Exception ex)
        {
            Console.WriteLine(modName + " Patching failed. Exception: " + ex);
        }
    }
}