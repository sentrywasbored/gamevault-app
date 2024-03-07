﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace gamevault.Models
{
    public enum AppConfigKey
    {
        Username,
        Password,
        RootPath,
        Executable,
        BackgroundStart,
        ServerUrl,
        LibStartup,
        AutoExtract,
        ExtractionFinished,
        RunningInTrayMessage,
        DownloadLimit,
        InstalledGamesOpen,
        LaunchParameter,
        ShowRawgTitle,
        LastPlayed,
        Phalcode1,
        Phalcode2,
        Theme
    }
    public static class AppFilePath
    {
        internal static string ImageCache = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}/GameVault/cache/images";
        internal static string OfflineProgress = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}/GameVault/cache/prgs";
        internal static string OfflineCache = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}/GameVault/cache/local";
        internal static string ConfigDir = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}/GameVault/config";
        internal static string WebConfigDir = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}/GameVault/config/web";
        internal static string UserFile = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}/GameVault/config/user";
        internal static string IgnoreList = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}/GameVault/cache/ignorelist";
        internal static string ErrorLog = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}/GameVault/errorlog";


        internal static void InitDebugPaths()
        {
            ImageCache = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}/GameVault/debug/cache/images";
            OfflineProgress = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}/GameVault/debug/cache/prgs";
            OfflineCache = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}/GameVault/debug/cache/local";
            ConfigDir = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}/GameVault/debug/config";
            WebConfigDir = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}/GameVault/debug/config/web";
            UserFile = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}/GameVault/debug/config/user";
            IgnoreList = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}/GameVault/debug/cache/ignorelist";
            ErrorLog = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}/GameVault/debug/errorlog";
        }
    }
    public static class Globals
    {
        internal static string[] SupportedExecutables = new string[] { "EXE", "BAT", "COM", "CMD", "INF", "IPA", "OSX", "PIF", "RUN", "WSH", "LNK", "SH" };
    }
}
