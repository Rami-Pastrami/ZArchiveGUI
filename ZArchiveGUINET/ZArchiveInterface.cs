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

        public static async Task<BatchTaskHandler.RESULT> WUAToWUP(String ZArchiveEXEPath, String WUPPath, String OutputFolderPath)
        {
            if (!Path.Exists(ZArchiveEXEPath)) { return BatchTaskHandler.RESULT.EXE_NOT_FOUND; }
            if (!Path.Exists(WUPPath)) { return BatchTaskHandler.RESULT.WUP_NOT_FOUND; }
            if (!Directory.Exists(OutputFolderPath)) { return BatchTaskHandler.RESULT.OUTPUT_DIR_NOT_FOUND; }


            using (Process proc = new() )
            {
                try
                {
                    proc.StartInfo.FileName = ZArchiveEXEPath;
                    proc.StartInfo.CreateNoWindow = true;
                    proc.StartInfo.Arguments = $"\"{WUPPath}\" \"{OutputFolderPath}\"";
                    proc.Start();
                    await proc.WaitForExitAsync();
                    return BatchTaskHandler.RESULT.OK;
                }
                catch
                {
                    return BatchTaskHandler.RESULT.ZARCHIVE_FAILED;
                }
            }


        }


    }
}
