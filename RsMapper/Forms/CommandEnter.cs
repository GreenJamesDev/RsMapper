﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RsMapper.Forms
{
    public partial class CommandEnter : Form
    {

        public string Command;

        public CommandEnter()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Command = textBox1.Text;
            this.Close();
            this.Dispose();
        }
    }
}
