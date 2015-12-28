//************************************************************
//* Application: Sample code dase for realtime/delayed data writing module
//* Created: December 27, 2015, 12:00
//* Author: R Sochor
//* Module: Data writer service
//*
//* Copyright 2015, all rights reserved
//************************************************************
using System.ServiceProcess;

namespace DataWriter.Service
{
    partial class DataWriterService : ServiceBase
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
            //this.DataWriterController = new System.ServiceProcess.ServiceController();
            this.DataWriter = new System.ComponentModel.BackgroundWorker();
            // 
            // SqlWriterController
            // 
            //this.DataWriterController.ServiceName = "Data Writer Service Controller Sample";
            // 
            // Service1
            // 
            this.ServiceName = "Data Writer Service Sample";
        }

        #endregion
        private System.ComponentModel.BackgroundWorker DataWriter;
    }
}
