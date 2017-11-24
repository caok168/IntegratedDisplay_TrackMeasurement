using IntegratedDisplay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IntegratedDisplay.Forms
{
    public partial class ChartConfigForm : Form
    {
        private ChartConfig _chartConfigData = null;
        public ChartConfigForm(ChartConfig config)
        {
            InitializeComponent();
            _chartConfigData = config;
        }

        private void ChartConfigForm_Load(object sender, EventArgs e)
        {
            txtTitle.Text = _chartConfigData.ChartTitle;
            labFontTitle.Text = _chartConfigData.ChartTitleFont.Name + "-" + _chartConfigData.ChartTitleFont.Size;

            txtAxesY.Text = _chartConfigData.AxesYTitle;
            labFontAxesY.Text = _chartConfigData.AxesYFont.Name + "-" + _chartConfigData.AxesYFont.Size;
            nudAxesYMin.Value = _chartConfigData.AxesYMin;
            nudAxesYStep.Value = _chartConfigData.AxesYStep;

            txtAxesX.Text = _chartConfigData.AxesXTitle;
            labFontAxesX.Text = _chartConfigData.AxesXFont.Name + "-" + _chartConfigData.AxesXFont.Size;
            nudAxesXMin.Value = _chartConfigData.AxesXMin;
            nudAxesXStep.Value = _chartConfigData.AxesXStep;
        }

        private void btnTiltleFont_Click(object sender, EventArgs e)
        {
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                _chartConfigData.ChartTitleFont = fontDialog.Font;
                labFontTitle.Text = _chartConfigData.ChartTitleFont.Name + "-" + _chartConfigData.ChartTitleFont.Size;
            }
        }

        private void btnAxesYFont_Click(object sender, EventArgs e)
        {
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                _chartConfigData.AxesYFont = fontDialog.Font;
                labFontAxesY.Text = _chartConfigData.AxesYFont.Name + "-" + _chartConfigData.AxesYFont.Size;
            }
        }

        private void btnAxesXFont_Click(object sender, EventArgs e)
        {
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                _chartConfigData.AxesXFont = fontDialog.Font;
                labFontAxesX.Text = _chartConfigData.AxesXFont.Name + "-" + _chartConfigData.AxesXFont.Size;
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            _chartConfigData.ChartTitle = txtTitle.Text;

            _chartConfigData.AxesYTitle = txtAxesY.Text;
            _chartConfigData.AxesYMin = (int)nudAxesYMin.Value;
            _chartConfigData.AxesYStep = (int)nudAxesYStep.Value;

            _chartConfigData.AxesXTitle = txtAxesX.Text;
            _chartConfigData.AxesXMin = (int)nudAxesXMin.Value;
            _chartConfigData.AxesXStep = (int)nudAxesXStep.Value;
            this.Tag = _chartConfigData;
            this.Close();
        }
    }
}
