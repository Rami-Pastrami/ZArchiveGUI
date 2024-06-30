using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace ZArchiveGUINET
{
    public partial class gui_main : Form
    {

        string[] selectedWUAFiles = new string[0];

        public gui_main()
        {
            InitializeComponent();
            ZArchivePathTextBox.Text = AttemptyLoadZArchivePath();

        }

        string AttemptyLoadZArchivePath()
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
            //TODO
            ResetRunUISection();
            EnableStartButtonIfReady();
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

        #endregion

        private async void RunWUAExtractionOnSelected()
        {
            string[] WUAsToExtract = GetSelectedFilePaths();
            BatchTaskHandler processor = new BatchTaskHandler(ZArchivePathTextBox.Text, text_file_output.Text);
            processor.UpdatedWuaExtractProgress += UpdateProgress;
            await processor.ProcessWUAsToWUPs(WUAsToExtract);
            ResetRunUISection();
            EnableStartButtonIfReady();
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
