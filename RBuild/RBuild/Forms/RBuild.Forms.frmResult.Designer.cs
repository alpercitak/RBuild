﻿namespace RBuild.Forms
{
    partial class frmResult
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmResult));
            this.tx_Result = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // tx_Result
            // 
            this.tx_Result.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tx_Result.Location = new System.Drawing.Point(0, 0);
            this.tx_Result.Name = "tx_Result";
            this.tx_Result.ReadOnly = true;
            this.tx_Result.Size = new System.Drawing.Size(292, 266);
            this.tx_Result.TabIndex = 5;
            this.tx_Result.Text = "";
            // 
            // frmResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.tx_Result);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmResult";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "RBuild";
            this.Load += new System.EventHandler(this.frmResult_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox tx_Result;
    }
}