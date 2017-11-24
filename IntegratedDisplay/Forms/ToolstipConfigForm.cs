using DevComponents.DotNetBar;
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
    public partial class ToolstipConfigForm : Form
    {
        public ToolstipConfigForm()
        {
            InitializeComponent();
        }

        Dictionary<string, ToolStripButton> dicAll = new Dictionary<string, ToolStripButton>();
        Dictionary<string, ToolStripButton> dicCustom = new Dictionary<string, ToolStripButton>();
        FormlayoutConfig config = new FormlayoutConfig();

        private void ToolstipConfigForm_Load(object sender, EventArgs e)
        {
            LoadAllToolStripItem();
            LoadCustomToolStripItem();
            config.statusBarList = new List<string>();
        }

        private void LoadAllToolStripItem()
        {
            listBoxAll.DataSource = null;
            listBoxAll.Items.Clear();
            dicAll.Clear();
            for (int i = 0; i < MainForm.sMainform.tsAllKeys.Items.Count - 1; i++)
            {
                dicAll.Add(MainForm.sMainform.tsAllKeys.Items[i].Text, (ToolStripButton)MainForm.sMainform.tsAllKeys.Items[i]);
            }
            if (dicAll.Count > 0)
            {
                listBoxAll.DataSource = new BindingSource(dicAll, null);
                listBoxAll.DisplayMember = "Key";
                listBoxAll.ValueMember = "Value";
            }
            
        }
        private void LoadCustomToolStripItem()
        {
            listBoxCustom.DataSource = null;
            listBoxCustom.Items.Clear();
            dicCustom.Clear();
            if (MainForm.sMainform.tsCustom.Items.Count > 0)
            {
                int itemsCount = MainForm.sMainform.tsCustom.Items.Count - 1;
                for (int i = 0; i < itemsCount; i++)
                {
                    dicCustom.Add(MainForm.sMainform.tsCustom.Items[i].Text, (ToolStripButton)MainForm.sMainform.tsCustom.Items[i]);
                }
            }
            if (dicCustom.Count > 0)
            {
                listBoxCustom.DataSource = new BindingSource(dicCustom, null);
                listBoxCustom.DisplayMember = "Key";
                listBoxCustom.ValueMember = "Value";
                config.statusBarList = dicCustom.Values.Select(p => p.Name).ToList();
                listBoxCustom.SelectedItems.Clear();
            }
            ConfigManger.SaveLayoutConfig(config);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listBoxAll.SelectedItems.Count; i++)
            {
                ListBoxItem item = listBoxAll.SelectedItems[i];
                if(dicAll.ContainsKey( item.Text))
                {
                    MainForm.sMainform.tsCustom.Items.Insert(MainForm.sMainform.tsCustom.Items.Count - 1, dicAll[item.Text]);
                    //dicAll.Remove(item.Text);
                }
                //var dicTemp = (KeyValuePair<string, ToolStripButton>)( as ItemBindingData).DataItem;
                
            }
            LoadAllToolStripItem();
            LoadCustomToolStripItem();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listBoxCustom.SelectedItems.Count; i++)
            {
                ListBoxItem item = listBoxCustom.SelectedItems[i];
                if (dicCustom.ContainsKey(item.Text))
                {
                    MainForm.sMainform.tsAllKeys.Items.Insert(MainForm.sMainform.tsAllKeys.Items.Count - 1, dicCustom[item.Text]);
                }
            }
            LoadAllToolStripItem();
            LoadCustomToolStripItem();
        }
    }
}
