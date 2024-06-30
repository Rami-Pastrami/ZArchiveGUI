using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ZArchiveGUINET
{
    internal class BatchTaskHandler
    {
        public event Action? StartedExtractWUA;
        public event Action? FinishedExtractWUA;
        public event UpdatedWUAExtractProgress? UpdatedWuaExtractProgress;

        public delegate void UpdatedWUAExtractProgress(string currentWUAFile, float completedPercentage);
        
        string ZArchivePath;
        string outputFolderPath;

        public BatchTaskHandler(string zArchivePath, string outputFolderPath)
        {
            this.ZArchivePath = zArchivePath;
            this.outputFolderPath = outputFolderPath;
        }

        public async Task<ZArchiveInterface.RESULT[]> ProcessWUAsToWUPs(string[] WUAFilePaths)
        {
            StartedExtractWUA?.Invoke();
            int numberWUAs = WUAFilePaths.Length;
            ZArchiveInterface.RESULT[] results = new ZArchiveInterface.RESULT[numberWUAs];
            for (int i = 0; i < numberWUAs; i++)
            {
                string fileName = Path.GetFileName(WUAFilePaths[i]);
                UpdatedWuaExtractProgress?.Invoke(fileName, (float)i / (float)numberWUAs);
                results[i] = await ZArchiveInterface.WUAToWUP(ZArchivePath, WUAFilePaths[i], outputFolderPath);
            }
            FinishedExtractWUA?.Invoke();
            return results;

        }


    }
}
