﻿namespace Project___Sudoku
{
    partial class SudokuGame
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
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.RandomNine = new System.Windows.Forms.Button();
            this.RandomSolution = new System.Windows.Forms.Button();
            this.CheckButton = new System.Windows.Forms.Button();
            this.newFileButton = new System.Windows.Forms.Button();
            this.puzzle = new System.Windows.Forms.DataGridView();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.RemoveNFixedSquares = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.puzzle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.RemoveNFixedSquares);
            this.splitContainer1.Panel1.Controls.Add(this.numericUpDown1);
            this.splitContainer1.Panel1.Controls.Add(this.RandomNine);
            this.splitContainer1.Panel1.Controls.Add(this.RandomSolution);
            this.splitContainer1.Panel1.Controls.Add(this.CheckButton);
            this.splitContainer1.Panel1.Controls.Add(this.newFileButton);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.puzzle);
            this.splitContainer1.Size = new System.Drawing.Size(969, 593);
            this.splitContainer1.SplitterDistance = 322;
            this.splitContainer1.TabIndex = 0;
            // 
            // RandomNine
            // 
            this.RandomNine.Location = new System.Drawing.Point(108, 307);
            this.RandomNine.Name = "RandomNine";
            this.RandomNine.Size = new System.Drawing.Size(99, 57);
            this.RandomNine.TabIndex = 3;
            this.RandomNine.Text = "Random Nine";
            this.RandomNine.UseVisualStyleBackColor = true;
            this.RandomNine.Click += new System.EventHandler(this.RandomNine_Click);
            // 
            // RandomSolution
            // 
            this.RandomSolution.Location = new System.Drawing.Point(108, 212);
            this.RandomSolution.Name = "RandomSolution";
            this.RandomSolution.Size = new System.Drawing.Size(99, 66);
            this.RandomSolution.TabIndex = 2;
            this.RandomSolution.Text = "Create Solution";
            this.RandomSolution.UseVisualStyleBackColor = true;
            this.RandomSolution.Click += new System.EventHandler(this.CreateSolution_Click);
            // 
            // CheckButton
            // 
            this.CheckButton.Location = new System.Drawing.Point(108, 124);
            this.CheckButton.Name = "CheckButton";
            this.CheckButton.Size = new System.Drawing.Size(99, 67);
            this.CheckButton.TabIndex = 1;
            this.CheckButton.Text = "Check Solution";
            this.CheckButton.UseVisualStyleBackColor = true;
            this.CheckButton.Click += new System.EventHandler(this.CheckButton_Click);
            // 
            // newFileButton
            // 
            this.newFileButton.Location = new System.Drawing.Point(108, 38);
            this.newFileButton.Name = "newFileButton";
            this.newFileButton.Size = new System.Drawing.Size(99, 64);
            this.newFileButton.TabIndex = 0;
            this.newFileButton.Text = "Import Board";
            this.newFileButton.UseVisualStyleBackColor = true;
            this.newFileButton.Click += new System.EventHandler(this.newFileButton_Click);
            // 
            // puzzle
            // 
            this.puzzle.AllowUserToAddRows = false;
            this.puzzle.AllowUserToDeleteRows = false;
            this.puzzle.AllowUserToResizeColumns = false;
            this.puzzle.AllowUserToResizeRows = false;
            this.puzzle.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.puzzle.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.puzzle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.puzzle.ColumnHeadersVisible = false;
            this.puzzle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.puzzle.Location = new System.Drawing.Point(0, 0);
            this.puzzle.MultiSelect = false;
            this.puzzle.Name = "puzzle";
            this.puzzle.RowHeadersVisible = false;
            this.puzzle.RowTemplate.Height = 28;
            this.puzzle.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.puzzle.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.puzzle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.puzzle.Size = new System.Drawing.Size(643, 593);
            this.puzzle.TabIndex = 0;
            this.puzzle.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.puzzle_CellValidating);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(150, 433);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 26);
            this.numericUpDown1.TabIndex = 4;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // RemoveNFixedSquares
            // 
            this.RemoveNFixedSquares.Location = new System.Drawing.Point(30, 410);
            this.RemoveNFixedSquares.Name = "RemoveNFixedSquares";
            this.RemoveNFixedSquares.Size = new System.Drawing.Size(99, 71);
            this.RemoveNFixedSquares.TabIndex = 5;
            this.RemoveNFixedSquares.Text = "Remove n Many";
            this.RemoveNFixedSquares.UseVisualStyleBackColor = true;
            this.RemoveNFixedSquares.Click += new System.EventHandler(this.RemoveNFixedSquares_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(969, 593);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.puzzle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView puzzle;
        private System.Windows.Forms.Button newFileButton;
        private System.Windows.Forms.Button CheckButton;
        private System.Windows.Forms.Button RandomSolution;
        private System.Windows.Forms.Button RandomNine;
        private System.Windows.Forms.Button RemoveNFixedSquares;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    }
}
