﻿namespace FinalPeoject
{
    partial class FORMHISTORY
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
            this.crystalReport11 = new FinalPeoject.CrystalReport1();
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.CrystalReport12 = new FinalPeoject.CrystalReport1();
            this.SuspendLayout();
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = 0;
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystalReportViewer1.Location = new System.Drawing.Point(0, 0);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.ReportSource = this.CrystalReport12;
            this.crystalReportViewer1.Size = new System.Drawing.Size(1027, 592);
            this.crystalReportViewer1.TabIndex = 0;
            // 
            // FORMHISTORY
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1027, 592);
            this.Controls.Add(this.crystalReportViewer1);
            this.Name = "FORMHISTORY";
            this.Text = "FORMHISTORY";
            this.Load += new System.EventHandler(this.FORMHISTORY_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalReport1 crystalReport11;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
        private CrystalReport1 CrystalReport12;
    }
}