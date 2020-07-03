namespace AchronPatcher
{
    partial class Patcher
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
            this.btnPatch = new System.Windows.Forms.Button();
            this.txtTarget = new System.Windows.Forms.TextBox();
            this.lblTarget = new System.Windows.Forms.Label();
            this.ofdClient = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // btnPatch
            // 
            this.btnPatch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPatch.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnPatch.Location = new System.Drawing.Point(13, 71);
            this.btnPatch.Name = "btnPatch";
            this.btnPatch.Size = new System.Drawing.Size(212, 62);
            this.btnPatch.TabIndex = 0;
            this.btnPatch.Text = "Patch Client";
            this.btnPatch.UseVisualStyleBackColor = true;
            this.btnPatch.Click += new System.EventHandler(this.btnPatch_Click);
            // 
            // txtTarget
            // 
            this.txtTarget.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.txtTarget.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.txtTarget.Location = new System.Drawing.Point(13, 34);
            this.txtTarget.Name = "txtTarget";
            this.txtTarget.Size = new System.Drawing.Size(212, 22);
            this.txtTarget.TabIndex = 1;
            this.txtTarget.Text = "127.0.0.1";
            // 
            // lblTarget
            // 
            this.lblTarget.AutoSize = true;
            this.lblTarget.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblTarget.Location = new System.Drawing.Point(12, 7);
            this.lblTarget.Name = "lblTarget";
            this.lblTarget.Size = new System.Drawing.Size(106, 17);
            this.lblTarget.TabIndex = 2;
            this.lblTarget.Text = "Target Address";
            // 
            // Patcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(239, 151);
            this.Controls.Add(this.lblTarget);
            this.Controls.Add(this.txtTarget);
            this.Controls.Add(this.btnPatch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "Patcher";
            this.Text = "Achron Patcher";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPatch;
        private System.Windows.Forms.TextBox txtTarget;
        private System.Windows.Forms.Label lblTarget;
        private System.Windows.Forms.OpenFileDialog ofdClient;
    }
}

