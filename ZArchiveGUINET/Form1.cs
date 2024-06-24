using System.IO;

namespace ZArchiveGUINET
{
    public partial class gui_main : Form
    {
        String path_wua = "";
        String path_output = "";
        String path_cache = "";


        public gui_main()
        {
            InitializeComponent();
            path_cache = AppContext.BaseDirectory;
            text_file_working.Text = path_cache;
        }



        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btn_file_list_all_Click(object sender, EventArgs e)
        {

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

                path_wua = System.IO.Path.GetDirectoryName(openFileDialog.FileNames[0]);
                text_file_input.Text = path_wua;

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
        private void btn_file_working_Click(object sender, EventArgs e)
        {

        }
    }

}
