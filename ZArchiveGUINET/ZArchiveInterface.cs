using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.DirectoryServices;

namespace ZArchiveGUINET
{
    /// <summary>
    /// Handles using the ZArchive.exe 
    /// </summary>
    public static class ZArchiveInterface
    {
        public enum RESULT
        {
            OK,
            EXE_NOT_FOUND,
            WUP_NOT_FOUND,
            OUTPUT_DIR_NOT_FOUND,
            ZARCHIVE_FAILED,
            SKIPPED,
        }

        public static async Task<RESULT> WUAToWUP(String ZArchiveEXEPath, String WUPPath, String OutputFolderPath)
        {
            if (!Directory.Exists(ZArchiveEXEPath)) { return RESULT.EXE_NOT_FOUND; }
            if (!Directory.Exists(WUPPath)) { return RESULT.WUP_NOT_FOUND; }
            if (!Directory.Exists(OutputFolderPath)) { return RESULT.OUTPUT_DIR_NOT_FOUND; }


            using (Process proc = new() )
            {
                try
                {
                    proc.StartInfo.FileName = ZArchiveEXEPath;
                    proc.StartInfo.CreateNoWindow = true;
                    proc.StartInfo.Arguments = $"\"{WUPPath}\" \"{OutputFolderPath}\"";
                    proc.Start();
                    await proc.WaitForExitAsync();
                    return RESULT.OK;
                }
                catch
                {
                    return RESULT.ZARCHIVE_FAILED;
                }
            }


        }


    }
}
