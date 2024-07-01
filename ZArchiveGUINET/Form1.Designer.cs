namespace ZArchiveGUINET
{
    partial class gui_main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            file_list = new CheckedListBox();
            btn_start = new Button();
            btn_cancel = new Button();
            progressBar1 = new ProgressBar();
            lab_prog_cur = new Label();
            lab_prog_total = new Label();
            btn_file_input = new Button();
            btn_file_list_all = new Button();
            btn_file_list_none = new Button();
            text_file_input = new TextBox();
            text_file_output = new TextBox();
            btn_file_output = new Button();
            ZArchivePathTextBox = new TextBox();
            ZArchivePathBtn = new Button();
            SuspendLayout();
            // 
            // file_list
            // 
            file_list.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            file_list.FormattingEnabled = true;
            file_list.Location = new Point(12, 165);
            file_list.Name = "file_list";
            file_list.Size = new Size(372, 292);
            file_list.TabIndex = 2;
            // 
            // btn_start
            // 
            btn_start.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btn_start.Enabled = false;
            btn_start.Location = new Point(558, 428);
            btn_start.Name = "btn_start";
            btn_start.Size = new Size(128, 32);
            btn_start.TabIndex = 0;
            btn_start.Text = "Start";
            btn_start.UseVisualStyleBackColor = true;
            btn_start.Click += btn_start_Click;
            // 
            // btn_cancel
            // 
            btn_cancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btn_cancel.Enabled = false;
            btn_cancel.Location = new Point(390, 428);
            btn_cancel.Name = "btn_cancel";
            btn_cancel.Size = new Size(128, 32);
            btn_cancel.TabIndex = 1;
            btn_cancel.Text = "Cancel";
            btn_cancel.UseVisualStyleBackColor = true;
            btn_cancel.Click += btn_cancel_Click;
            // 
            // progressBar1
            // 
            progressBar1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            progressBar1.Location = new Point(390, 400);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(296, 23);
            progressBar1.TabIndex = 3;
            // 
            // lab_prog_cur
            // 
            lab_prog_cur.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            lab_prog_cur.AutoSize = true;
            lab_prog_cur.Location = new Point(391, 266);
            lab_prog_cur.MinimumSize = new Size(300, 0);
            lab_prog_cur.Name = "lab_prog_cur";
            lab_prog_cur.Size = new Size(300, 15);
            lab_prog_cur.TabIndex = 6;
            lab_prog_cur.Text = "Current File: None";
            lab_prog_cur.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_prog_total
            // 
            lab_prog_total.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            lab_prog_total.AutoSize = true;
            lab_prog_total.Location = new Point(493, 382);
            lab_prog_total.Name = "lab_prog_total";
            lab_prog_total.Size = new Size(80, 15);
            lab_prog_total.TabIndex = 7;
            lab_prog_total.Text = "Total Progress";
            lab_prog_total.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btn_file_input
            // 
            btn_file_input.Location = new Point(572, 12);
            btn_file_input.Name = "btn_file_input";
            btn_file_input.Size = new Size(119, 23);
            btn_file_input.TabIndex = 8;
            btn_file_input.Text = "Source";
            btn_file_input.UseVisualStyleBackColor = true;
            btn_file_input.Click += btn_file_input_Click;
            // 
            // btn_file_list_all
            // 
            btn_file_list_all.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btn_file_list_all.Location = new Point(284, 139);
            btn_file_list_all.Name = "btn_file_list_all";
            btn_file_list_all.Size = new Size(100, 23);
            btn_file_list_all.TabIndex = 10;
            btn_file_list_all.Text = "Select All";
            btn_file_list_all.UseVisualStyleBackColor = true;
            btn_file_list_all.Click += btn_file_list_all_Click;
            // 
            // btn_file_list_none
            // 
            btn_file_list_none.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btn_file_list_none.Location = new Point(12, 139);
            btn_file_list_none.Name = "btn_file_list_none";
            btn_file_list_none.Size = new Size(100, 23);
            btn_file_list_none.TabIndex = 11;
            btn_file_list_none.Text = "Select None";
            btn_file_list_none.UseVisualStyleBackColor = true;
            btn_file_list_none.Click += btn_file_list_none_Click;
            // 
            // text_file_input
            // 
            text_file_input.Location = new Point(12, 13);
            text_file_input.Name = "text_file_input";
            text_file_input.ReadOnly = true;
            text_file_input.Size = new Size(554, 23);
            text_file_input.TabIndex = 12;
            // 
            // text_file_output
            // 
            text_file_output.Location = new Point(12, 40);
            text_file_output.Name = "text_file_output";
            text_file_output.ReadOnly = true;
            text_file_output.Size = new Size(554, 23);
            text_file_output.TabIndex = 15;
            // 
            // btn_file_output
            // 
            btn_file_output.Location = new Point(572, 39);
            btn_file_output.Name = "btn_file_output";
            btn_file_output.Size = new Size(119, 23);
            btn_file_output.TabIndex = 14;
            btn_file_output.Text = "Output";
            btn_file_output.UseVisualStyleBackColor = true;
            btn_file_output.Click += btn_file_output_Click;
            // 
            // ZArchivePathTextBox
            // 
            ZArchivePathTextBox.Location = new Point(12, 69);
            ZArchivePathTextBox.Name = "ZArchivePathTextBox";
            ZArchivePathTextBox.ReadOnly = true;
            ZArchivePathTextBox.Size = new Size(554, 23);
            ZArchivePathTextBox.TabIndex = 17;
            // 
            // ZArchivePathBtn
            // 
            ZArchivePathBtn.Location = new Point(572, 68);
            ZArchivePathBtn.Name = "ZArchivePathBtn";
            ZArchivePathBtn.Size = new Size(119, 23);
            ZArchivePathBtn.TabIndex = 16;
            ZArchivePathBtn.Text = "ZArchive Path";
            ZArchivePathBtn.UseVisualStyleBackColor = true;
            ZArchivePathBtn.Click += ZArchivePathBtn_Click;
            // 
            // gui_main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(698, 469);
            Controls.Add(ZArchivePathTextBox);
            Controls.Add(ZArchivePathBtn);
            Controls.Add(text_file_output);
            Controls.Add(btn_file_output);
            Controls.Add(text_file_input);
            Controls.Add(btn_file_list_none);
            Controls.Add(btn_file_list_all);
            Controls.Add(btn_file_input);
            Controls.Add(lab_prog_total);
            Controls.Add(lab_prog_cur);
            Controls.Add(progressBar1);
            Controls.Add(file_list);
            Controls.Add(btn_cancel);
            Controls.Add(btn_start);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "gui_main";
            Text = "WUA to WUP";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btn_start;
        private Button btn_cancel;
        private CheckedListBox file_list;
        private ProgressBar progressBar1;
        private Label lab_prog_cur;
        private Label lab_prog_total;
        private Button btn_file_input;
        private Button btn_file_list_all;
        private Button btn_file_list_none;
        private TextBox text_file_input;
        private TextBox text_file_output;
        private Button btn_file_output;
        private TextBox ZArchivePathTextBox;
        private Button ZArchivePathBtn;
    }
}
