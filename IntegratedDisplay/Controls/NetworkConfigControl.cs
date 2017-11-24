using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IntegratedDisplay.Models;

namespace IntegratedDisplay
{
    public partial class NetworkConfigControl : UserControl
    {
        public ServerConfigDesc ServerConfig { get; set; }

        public NetworkConfigControl()
        {
            InitializeComponent();
        }

        private void NetworkConfigControl_Load(object sender, EventArgs e)
        {
            if (ServerConfig != null)
            {
                txtServerIP.Text = ServerConfig.IP;
                txtServerPort.Text = ServerConfig.Port.ToString();
            }
        }

        private void txtServerPort_Leave(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (!string.IsNullOrEmpty(textBox.Text.Trim()))
            {
                int port = 0;
                if (int.TryParse(textBox.Text.Trim(), out port))
                {
                    if (port > 0)
                    {
                        ServerConfig.Port = port;
                    }
                    else
                    {
                        MessageBox.Show("端口号必须大于0！");
                        textBox.Text = string.Empty;
                        textBox.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("请输入一个大于0整数！");
                    textBox.Text = string.Empty;
                    textBox.Focus();
                }
            }
            else
            {
                MessageBox.Show("数据不能为空，请重新输入！");
                textBox.Text = string.Empty;
                textBox.Focus();
            }
        }

        private void txtServerIP_Leave(object sender, EventArgs e)
        {
            ServerConfig.IP = txtServerIP.Text;
        }
    }
}
