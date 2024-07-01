using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace ZArchiveGUINET
{
    public partial class gui_main : Form
    {
        bool isJavaInstalled = false;
        string[] selectedWUAFiles = new string[0];
        BatchTaskHandler? processor = null;

        public gui_main()
        {
            InitializeComponent();
            ZArchivePathTextBox.Text = AttemptLoadZArchivePath();
            isJavaInstalled = IsJavaInstalled();
        }

        string AttemptLoadZArchivePath()
        {
            string currentFolder = "";
            currentFolder = Path.GetDirectoryName(Application.ExecutablePath);
            string possiblePath = currentFolder + "zarchive.exe";
            if (File.Exists(possiblePath))
            {
                return possiblePath;
            }
            return "";
        }


        #region Button Events

        /// <summary>
        /// For setting directory containing WUA files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_file_input_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Select all WUAs you would like to dump as WUP files";
                openFileDialog.Multiselect = true;
                openFileDialog.Filter = "wua files (*.wua)|*.wua";
                openFileDialog.FilterIndex = 0;
                openFileDialog.RestoreDirectory = true;

                // Verify
                if (openFileDialog.ShowDialog() != DialogResult.OK) { return; }
                if (openFileDialog.SafeFileNames.Length == 0) { return; }
                if (Path.GetDirectoryName(openFileDialog.FileNames[0]) is null) { return; }

                // Set folder path
                text_file_input.Text = Path.GetDirectoryName(openFileDialog.FileNames[0]);
                selectedWUAFiles = openFileDialog.FileNames; // save full paths

                // Set selectable items
                String[] files = openFileDialog.SafeFileNames;
                file_list.Items.Clear();
                foreach (String file in files)
                {
                    file_list.Items.Add(file);
                }
                EnableStartButtonIfReady();
            }
        }

        /// <summary>
        /// For setting Output folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_file_output_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                //Verify
                if (folderBrowserDialog.ShowDialog() != DialogResult.OK) { return; }
                if (!Directory.Exists(folderBrowserDialog.SelectedPath)) {  return; }

                // Set folder path
                text_file_output.Text = folderBrowserDialog.SelectedPath;
                EnableStartButtonIfReady();
            }
        }

        /// <summary>
        /// For setting ZArchive EXE path
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZArchivePathBtn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Select the ZArchive exe";
                openFileDialog.Multiselect = false;
                openFileDialog.Filter = "exe files (*.exe)|*.exe";
                openFileDialog.FilterIndex = 0;
                openFileDialog.RestoreDirectory = true;

                // Verify
                if (openFileDialog.ShowDialog() != DialogResult.OK) { return; }

                // Set exe path
                ZArchivePathTextBox.Text = openFileDialog.FileName;
                EnableStartButtonIfReady();
            }
        }


        /// <summary>
        /// For selecting all items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_file_list_all_Click(object sender, EventArgs e)
        {
            if (file_list.Items.Count == 0) { return; }
            for (int itemIndex = 0; itemIndex < file_list.Items.Count; itemIndex++)
            {
                file_list.SetItemChecked(itemIndex, true);
            }
        }

        /// <summary>
        /// For deslecting all items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_file_list_none_Click(object sender, EventArgs e)
        {
            if (file_list.Items.Count == 0) { return; }
            for (int itemIndex = 0; itemIndex < file_list.Items.Count; itemIndex++)
            {
                file_list.SetItemChecked(itemIndex, false);
            }
        }


        private void btn_start_Click(object sender, EventArgs e)
        {
            RunWUAExtractionOnSelected();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            if(processor == null) {  return; }

            processor.ForceStop();
            btn_cancel.Text = "Canceling...";

        }

        #endregion

        #region UI Logic
        /// <summary>
        /// Checks state of program, will enable start button if prequesites are met
        /// </summary>
        private void EnableStartButtonIfReady()
        {
            bool isReady = selectedWUAFiles.Length > 0;
            isReady = isReady && Directory.Exists(text_file_output.Text);
            isReady = isReady && Path.Exists(ZArchivePathTextBox.Text);
            btn_start.Enabled = isReady;
        }

        private void ResetRunUISection()
        {
            btn_start.Enabled = false;
            btn_cancel.Enabled = false;
            btn_cancel.Text = "Cancel";
            lab_prog_cur.Text = "Current File: None";
            progressBar1.Value = 0;
        }

        #endregion

        #region Queries

        private string[] GetSelectedFilePaths()
        {
            string[] file_names = file_list.CheckedItems.Cast<string>().ToArray();
            string[] paths = new string[file_names.Length];
            for (int i = 0; i < file_names.Length; i++)
            {
                paths[i] = text_file_input.Text + "\\" +  file_names[i];
            }
            return paths;

        }

        private bool IsJavaInstalled()
        {
            try
            {
                // Totally not taken from https://stackoverflow.com/questions/1855937/how-to-detect-whether-java-runtime-is-installed-or-not
                using (Process javaTester = new Process())
                {
                    List<String> output = new List<string>();
                    javaTester.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    javaTester.StartInfo.CreateNoWindow = true;
                    javaTester.StartInfo.FileName = "cmd.exe";
                    javaTester.StartInfo.UseShellExecute = false;
                    javaTester.StartInfo.RedirectStandardOutput = true;
                    javaTester.StartInfo.RedirectStandardError = true;
                    javaTester.StartInfo.Arguments = "/c \"" + "java -version " + "\"";

                    javaTester.OutputDataReceived += new DataReceivedEventHandler((s, e) =>
                    {
                        if (e.Data != null)
                        {
                            output.Add((string)e.Data);
                        }
                    });
                    javaTester.ErrorDataReceived += new DataReceivedEventHandler((s, e) =>
                    {
                        if (e.Data != null)
                        {
                            output.Add((String)e.Data);
                        }
                    });

                    javaTester.Start();
                    javaTester.BeginOutputReadLine();
                    javaTester.BeginErrorReadLine();

                    return javaTester.ExitCode == 0;
                }
            }
            catch 
            {
                return false;
            }

        }

        #endregion

        private async void RunWUAExtractionOnSelected()
        {
            btn_start.Enabled = false;
            btn_cancel.Enabled = true;
            string[] WUAsToExtract = GetSelectedFilePaths();
            processor = new BatchTaskHandler(ZArchivePathTextBox.Text, text_file_output.Text);
            processor.UpdatedWuaExtractProgress += UpdateProgress;
            BatchTaskHandler.RESULT[] results = await processor.ProcessWUAs(WUAsToExtract);
            ResetRunUISection();
            EnableStartButtonIfReady();
            processor = null;
        }

        private void UpdateProgress(string currentFileName, float percentageComplete)
        {
            lab_prog_cur.Text = "Current File: " + currentFileName;
            progressBar1.Value = (int)(percentageComplete * 100f);
        }


        // TODO
        // gui cleanup



    }

}
