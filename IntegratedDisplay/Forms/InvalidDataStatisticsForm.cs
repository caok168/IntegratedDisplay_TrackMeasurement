/// -------------------------------------------------------------------------------------------
/// FileName：InvalidDataStatisticsForm
/// 说    明：无效区段统计窗体
/// Version ：1.0
/// Date    ：2017/6/1
/// Author  ：CaoKai
/// -------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Windows.Forms;
using IntegratedDisplay.Models;

namespace IntegratedDisplay.Forms
{
    /// <summary>
    /// 无效区段统计窗体
    /// </summary>
    public partial class InvalidDataStatisticsForm : Form
    {
        public InvalidDataStatisticsForm()
        {
            InitializeComponent();
        }

        public InvalidDataStatisticsForm(List<StatisticsData> dataList)
        {
            InitializeComponent();

            InitListview(dataList);
        }

        private void InitListview(List<StatisticsData> dataList)
        {
            if (dataList == null || dataList.Count == 0)
            {
                return;
            }
            foreach (StatisticsData cls in dataList)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = cls.reasonType;
                //lvi.SubItems.Add(cls.reasonType);
                lvi.SubItems.Add(cls.sumcount.ToString());
                lvi.SubItems.Add(cls.countPercent);
                lvi.SubItems.Add(cls.gongliPercent);

                listView1.Items.Add(lvi);
            }
        }
    }
}
