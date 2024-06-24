using System.IO;

namespace ZArchiveGUINET
{
    public partial class gui_main : Form
    {
        String pathWua = "";
        String pathOutput = "";
        String pathCache = "";


        public gui_main()
        {
            InitializeComponent();
            update_paths("", "", AppContext.BaseDirectory);
        }

        private void update_paths(String wuaPath, String outputPath, String cachePath)
        {
            pathWua = wuaPath;
            pathOutput = outputPath;
            pathCache = cachePath;

            text_file_input.Text = wuaPath;
            text_file_output.Text = outputPath;
            text_file_working.Text = cachePath;

        }

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
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Multiselect = true;
                openFileDialog.Filter = "wua files (*.wua)|*.wua";
                openFileDialog.FilterIndex = 0;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() != DialogResult.OK) { return; }
                if (openFileDialog.SafeFileNames.Length == 0) { return; }
                if (Path.GetDirectoryName(openFileDialog.FileNames[0]) is null) { return; }

                update_paths(Path.GetDirectoryName(openFileDialog.FileNames[0]), pathOutput, pathCache);

                String[] files = openFileDialog.SafeFileNames;
                file_list.Items.Clear();
                foreach (String file in files)
                {
                    file_list.Items.Add(file);
                }

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
                if (folderBrowserDialog.ShowDialog() != DialogResult.OK) { return; }

                String selectedFolder = folderBrowserDialog.SelectedPath;
                update_paths(pathWua, selectedFolder, pathCache);
            }
        }

        /// <summary>
        /// For setting cache / working folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_file_working_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                if (folderBrowserDialog.ShowDialog() != DialogResult.OK) { return; }

                String selectedFolder = folderBrowserDialog.SelectedPath;
                update_paths(pathWua, pathOutput, selectedFolder);
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

        // TODO
        // check for exe existing
        // chain exe usage
        // gui cleanup



    }

}
