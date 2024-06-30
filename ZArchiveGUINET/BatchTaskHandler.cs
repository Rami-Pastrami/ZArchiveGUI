using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ZArchiveGUINET
{
    public class BatchTaskHandler
    {
        public enum RESULT
        {
            OK,
            EXE_NOT_FOUND,
            WUP_NOT_FOUND,
            OUTPUT_DIR_NOT_FOUND,
            ZARCHIVE_FAILED,
            NUSPACKER_NOT_FOUND,
            NUS_PACKER_FAILED,
            SKIPPED,
        }

        public event Action? StartedExtractWUA;
        public event Action? FinishedExtractWUA;
        public event Action? Cancled;
        public event UpdatedWUAExtractProgress? UpdatedWuaExtractProgress;

        public delegate void UpdatedWUAExtractProgress(string currentWUAFile, float completedPercentage);
        
        string ZArchivePath;
        string outputFolderPath;
        bool forceStop = false;

        public BatchTaskHandler(string zArchivePath, string outputFolderPath)
        {
            this.ZArchivePath = zArchivePath;
            this.outputFolderPath = outputFolderPath;
        }

        public void ForceStop()
        {
            forceStop = true;
        }

        public async Task<RESULT[]> ProcessWUAsToWUPs(string[] WUAFilePaths)
        {
            StartedExtractWUA?.Invoke();
            int numberWUAs = WUAFilePaths.Length;
            RESULT[] results = new RESULT[numberWUAs];
            Array.Fill<RESULT>(results, RESULT.SKIPPED);
            for (int i = 0; i < numberWUAs; i++)
            {
                if (forceStop)
                {
                    forceStop = false;
                    break;
                }
                string fileName = Path.GetFileName(WUAFilePaths[i]);
                UpdatedWuaExtractProgress?.Invoke(fileName, (float)i / (float)numberWUAs);
                results[i] = await ZArchiveInterface.WUAToWUP(ZArchivePath, WUAFilePaths[i], outputFolderPath);
            }
            FinishedExtractWUA?.Invoke();
            return results;

        }


    }
}
