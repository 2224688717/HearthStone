﻿namespace 炉边传说
{
    partial class ctlFilePicker
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmdClearPath = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtPathName = new System.Windows.Forms.TextBox();
            this.cmdBrowse = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmdClearPath
            // 
            this.cmdClearPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdClearPath.BackColor = System.Drawing.Color.Transparent;
            this.cmdClearPath.Location = new System.Drawing.Point(375, 3);
            this.cmdClearPath.Name = "cmdClearPath";
            this.cmdClearPath.Size = new System.Drawing.Size(75, 25);
            this.cmdClearPath.TabIndex = 10;
            this.cmdClearPath.Text = "Clear";
            this.cmdClearPath.UseVisualStyleBackColor = false;
            this.cmdClearPath.Click += new System.EventHandler(this.cmdClearPath_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Location = new System.Drawing.Point(3, 13);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(27, 13);
            this.lblTitle.TabIndex = 9;
            this.lblTitle.Text = "Title";
            // 
            // txtPathName
            // 
            this.txtPathName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPathName.BackColor = System.Drawing.Color.White;
            this.txtPathName.Location = new System.Drawing.Point(50, 6);
            this.txtPathName.Name = "txtPathName";
            this.txtPathName.ReadOnly = true;
            this.txtPathName.Size = new System.Drawing.Size(238, 20);
            this.txtPathName.TabIndex = 8;
            // 
            // cmdBrowse
            // 
            this.cmdBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdBrowse.BackColor = System.Drawing.Color.Transparent;
            this.cmdBrowse.Location = new System.Drawing.Point(294, 3);
            this.cmdBrowse.Name = "cmdBrowse";
            this.cmdBrowse.Size = new System.Drawing.Size(75, 24);
            this.cmdBrowse.TabIndex = 7;
            this.cmdBrowse.Text = "Browse...";
            this.cmdBrowse.UseVisualStyleBackColor = false;
            this.cmdBrowse.Click += new System.EventHandler(this.cmdBrowse_Click);
            // 
            // ctlFilePicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.cmdClearPath);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.txtPathName);
            this.Controls.Add(this.cmdBrowse);
            this.Name = "ctlFilePicker";
            this.Size = new System.Drawing.Size(463, 31);
            this.Load += new System.EventHandler(this.ctlFilePicker_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdClearPath;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtPathName;
        private System.Windows.Forms.Button cmdBrowse;
    }
}
