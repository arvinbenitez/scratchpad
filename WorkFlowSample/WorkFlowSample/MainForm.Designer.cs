namespace WorkFlowSample
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonEnd = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.textBoxResult = new System.Windows.Forms.TextBox();
            this.textWorkflowId = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxXaml = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelB = new System.Windows.Forms.Label();
            this.textBoxBookmark = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(12, 12);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(116, 23);
            this.buttonStart.TabIndex = 0;
            this.buttonStart.Text = "Start Transcode";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonEnd
            // 
            this.buttonEnd.Location = new System.Drawing.Point(134, 12);
            this.buttonEnd.Name = "buttonEnd";
            this.buttonEnd.Size = new System.Drawing.Size(130, 23);
            this.buttonEnd.TabIndex = 2;
            this.buttonEnd.Text = "Resume Transcode";
            this.buttonEnd.UseVisualStyleBackColor = true;
            this.buttonEnd.Click += new System.EventHandler(this.buttonResumeTranscode_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(337, 81);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Resume from DB";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.buttonResumeFromDB_Click);
            // 
            // textBoxResult
            // 
            this.textBoxResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxResult.Location = new System.Drawing.Point(520, 76);
            this.textBoxResult.Multiline = true;
            this.textBoxResult.Name = "textBoxResult";
            this.textBoxResult.Size = new System.Drawing.Size(353, 323);
            this.textBoxResult.TabIndex = 4;
            // 
            // textWorkflowId
            // 
            this.textWorkflowId.Location = new System.Drawing.Point(112, 28);
            this.textWorkflowId.Name = "textWorkflowId";
            this.textWorkflowId.Size = new System.Drawing.Size(332, 20);
            this.textWorkflowId.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.labelB);
            this.groupBox1.Controls.Add(this.textBoxBookmark);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textWorkflowId);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Location = new System.Drawing.Point(21, 280);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(467, 119);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Workflow To Load from DB";
            // 
            // textBoxXaml
            // 
            this.textBoxXaml.Location = new System.Drawing.Point(21, 76);
            this.textBoxXaml.Multiline = true;
            this.textBoxXaml.Name = "textBoxXaml";
            this.textBoxXaml.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxXaml.Size = new System.Drawing.Size(467, 198);
            this.textBoxXaml.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Workflow XAML";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Workflow Id";
            // 
            // labelB
            // 
            this.labelB.AutoSize = true;
            this.labelB.Location = new System.Drawing.Point(7, 54);
            this.labelB.Name = "labelB";
            this.labelB.Size = new System.Drawing.Size(55, 13);
            this.labelB.TabIndex = 10;
            this.labelB.Text = "Bookmark";
            // 
            // textBoxBookmark
            // 
            this.textBoxBookmark.Location = new System.Drawing.Point(112, 54);
            this.textBoxBookmark.Name = "textBoxBookmark";
            this.textBoxBookmark.Size = new System.Drawing.Size(332, 20);
            this.textBoxBookmark.TabIndex = 9;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(885, 427);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textBoxResult);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxXaml);
            this.Controls.Add(this.buttonEnd);
            this.Controls.Add(this.buttonStart);
            this.Name = "MainForm";
            this.Text = "Workflow Form";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonEnd;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBoxResult;
        private System.Windows.Forms.TextBox textWorkflowId;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxXaml;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelB;
        private System.Windows.Forms.TextBox textBoxBookmark;
    }
}

