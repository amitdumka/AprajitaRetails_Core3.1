﻿namespace AprajitaRetailsWatcher
{
    partial class VoyWatcher
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
            this.fileWatcher = new System.IO.FileSystemWatcher();
            ((System.ComponentModel.ISupportInitialize)(this.fileWatcher)).BeginInit();
            // 
            // fileWatcher
            // 
            this.fileWatcher.EnableRaisingEvents = true;
            this.fileWatcher.Changed += new System.IO.FileSystemEventHandler(this.FileWatcher_Changed);
            this.fileWatcher.Created += new System.IO.FileSystemEventHandler(this.FileWatcher_Created);
            this.fileWatcher.Deleted += new System.IO.FileSystemEventHandler(this.FileWatcher_Deleted);
            // 
            // Service1
            // 
            this.ServiceName = "AprajitaRetailsWatcher";
            ((System.ComponentModel.ISupportInitialize)(this.fileWatcher)).EndInit();

        }

        #endregion

        private System.IO.FileSystemWatcher fileWatcher;
    }
}
