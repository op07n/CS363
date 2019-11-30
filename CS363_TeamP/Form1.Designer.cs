namespace CS363_TeamP
{
    partial class Form1
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
        public void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtCollisionImminent = new System.Windows.Forms.TextBox();
            this.dgvTakeoffQueue = new System.Windows.Forms.DataGridView();
            this.flightID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dest = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.waitTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnTakeoff = new System.Windows.Forms.DataGridViewButtonColumn();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTakeoffQueue)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::CS363_TeamP.Properties.Resources.pngkit_dirt_explosion_png_1787968;
            this.pictureBox1.Location = new System.Drawing.Point(631, 239);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(25, 25);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // txtCollisionImminent
            // 
            this.txtCollisionImminent.BackColor = System.Drawing.Color.Black;
            this.txtCollisionImminent.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCollisionImminent.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCollisionImminent.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCollisionImminent.ForeColor = System.Drawing.Color.Red;
            this.txtCollisionImminent.Location = new System.Drawing.Point(8, 20);
            this.txtCollisionImminent.Margin = new System.Windows.Forms.Padding(2);
            this.txtCollisionImminent.Name = "txtCollisionImminent";
            this.txtCollisionImminent.Size = new System.Drawing.Size(316, 28);
            this.txtCollisionImminent.TabIndex = 1;
            this.txtCollisionImminent.Text = "** COLLISION IMMINENT**";
            this.txtCollisionImminent.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCollisionImminent.Visible = false;
            // 
            // dgvTakeoffQueue
            // 
            this.dgvTakeoffQueue.AllowUserToAddRows = false;
            this.dgvTakeoffQueue.AllowUserToDeleteRows = false;
            this.dgvTakeoffQueue.AllowUserToResizeColumns = false;
            this.dgvTakeoffQueue.AllowUserToResizeRows = false;
            this.dgvTakeoffQueue.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTakeoffQueue.BackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ControlDarkDark;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTakeoffQueue.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTakeoffQueue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTakeoffQueue.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.flightID,
            this.Dest,
            this.waitTime,
            this.btnTakeoff});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ControlDarkDark;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTakeoffQueue.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvTakeoffQueue.EnableHeadersVisualStyles = false;
            this.dgvTakeoffQueue.GridColor = System.Drawing.SystemColors.HotTrack;
            this.dgvTakeoffQueue.Location = new System.Drawing.Point(12, 87);
            this.dgvTakeoffQueue.MultiSelect = false;
            this.dgvTakeoffQueue.Name = "dgvTakeoffQueue";
            this.dgvTakeoffQueue.ReadOnly = true;
            this.dgvTakeoffQueue.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvTakeoffQueue.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvTakeoffQueue.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTakeoffQueue.Size = new System.Drawing.Size(312, 449);
            this.dgvTakeoffQueue.TabIndex = 2;
            this.dgvTakeoffQueue.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTakeoffQueue_CellClick);
            // 
            // flightID
            // 
            this.flightID.HeaderText = "Flight ID";
            this.flightID.Name = "flightID";
            this.flightID.ReadOnly = true;
            this.flightID.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.flightID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Dest
            // 
            this.Dest.HeaderText = "Dest. AP";
            this.Dest.Name = "Dest";
            this.Dest.ReadOnly = true;
            this.Dest.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Dest.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // waitTime
            // 
            this.waitTime.HeaderText = "Wait Time";
            this.waitTime.Name = "waitTime";
            this.waitTime.ReadOnly = true;
            this.waitTime.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.waitTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // btnTakeoff
            // 
            this.btnTakeoff.HeaderText = "Action";
            this.btnTakeoff.Name = "btnTakeoff";
            this.btnTakeoff.ReadOnly = true;
            this.btnTakeoff.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.btnTakeoff.Text = "Takeoff";
            this.btnTakeoff.UseColumnTextForButtonValue = true;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.Black;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.textBox1.Location = new System.Drawing.Point(12, 64);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(312, 19);
            this.textBox1.TabIndex = 3;
            this.textBox1.Text = "Takeoff Queue";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackgroundImage = global::CS363_TeamP.Properties.Resources.BackgroundWithMenuSpace;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1424, 741);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.dgvTakeoffQueue);
            this.Controls.Add(this.txtCollisionImminent);
            this.Controls.Add(this.pictureBox1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "NextGen ATC Simulator";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Click += new System.EventHandler(this.Form1_Click);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTakeoffQueue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtCollisionImminent;
        private System.Windows.Forms.DataGridView dgvTakeoffQueue;
        private System.Windows.Forms.DataGridViewTextBoxColumn flightID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dest;
        private System.Windows.Forms.DataGridViewTextBoxColumn waitTime;
        private System.Windows.Forms.DataGridViewButtonColumn btnTakeoff;
        private System.Windows.Forms.TextBox textBox1;
    }
}

