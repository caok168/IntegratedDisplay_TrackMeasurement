
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Collections.ObjectModel;

namespace IntegratedDisplay.Models
{
    /// <summary>
    /// 服务器配置信息
    /// </summary>
    public class ServerConfigDesc
    {
        /// <summary>
        /// 服务器IP
        /// </summary>
        [XmlAttribute(AttributeName = "Server")]
        public string IP { get; set; }

        /// <summary>
        /// 服务器端口
        /// </summary>
        [XmlAttribute(AttributeName = "Port")]
        public int Port { get; set; }
    }
    /// <summary>
    /// 台帐信息结构描述
    /// 添加XmlAttribute的属性,在XML中的名称将会显示为AttributeName后的名称
    /// 同时也将变XML的属性,不再是值
    /// </summary>
    public class AccountDesc
    {
        /// <summary>
        /// 表名称--台账信息在Inner.idf数据库中的表名称
        /// </summary>
        [XmlAttribute(AttributeName="Name")]
        public string TableName { get; set; }

        /// <summary>
        /// 起始公里标--台账信息在Inner.idf数据库中的列名称
        /// </summary>
        [XmlAttribute(AttributeName="StartMileage")]
        public string StartMileage { get; set; }
        
        /// <summary>
        /// 结束公里标--台账信息在Inner.idf数据库中的列名称
        /// </summary>
        [XmlAttribute(AttributeName = "EndMileage")]
        public string EndMileage { get; set; }

        /// <summary>
        /// 该类台帐信息是否选中
        /// </summary>
        [XmlAttribute(AttributeName = "IsCheck")]
        public bool IsCheck { get; set; }

        /// <summary>
        /// 显示类型
        /// </summary>
        [XmlAttribute(AttributeName = "KeyText")]
        public string KeyText { get; set; }

        /// <summary>
        /// 需要显示的列
        /// </summary>
        [XmlAttribute(AttributeName = "DisplayText")]
        public string DisplayText { get; set; }

        /// <summary>
        /// 台账信息中文名称
        /// </summary>
        [XmlElementAttribute("Name", IsNullable = false)]
        public string Name { get; set; }

        /// <summary>
        /// 初始化为空
        /// </summary>
        public AccountDesc()
        {
            this.Name = string.Empty;
            this.TableName = string.Empty;
            this.StartMileage = string.Empty;
            this.EndMileage = string.Empty;
            this.IsCheck = false;
            this.KeyText = string.Empty;
            this.DisplayText = string.Empty;
        }

        /// <summary>
        /// 根据参数进行初始化
        /// </summary>
        /// <param name="name">中文名称</param>
        /// <param name="tableName">在表中的名字</param>
        /// <param name="startMileage">参数1的值,默认为里程起点</param>
        /// <param name="endMileage">参数2的值,默认为里程终点</param>
        /// <param name="isCheck">是否选中</param>
        /// <param name="typeText">类型名称</param>
        /// <param name="displayText">显示的值</param>
        public AccountDesc(string name, string tableName, string startMileage, string endMileage, bool isCheck, string typeText, string displayText)
        {
            this.Name = name;
            this.TableName = tableName;
            this.StartMileage = startMileage;
            this.EndMileage = endMileage;
            this.IsCheck = isCheck;
            this.KeyText = typeText;
            this.DisplayText = displayText;
        }
    }

    /// <summary>
    /// 台帐总配置信息
    /// </summary>
    public class AccountsInfoDesc
    {
        /// <summary>
        /// 台帐信息是否显示
        /// </summary>
        [XmlAttribute(AttributeName = "IsCheck")]
        public bool IsCheck { get; set; }

        /// <summary>
        /// 台帐配置信息列表
        /// </summary>
        public List<AccountDesc> AccountDescList { get; set; }
    }

    /// <summary>
    /// 已添加的目录信息
    /// </summary>
    public class DirectoryPathInfo
    {
        /// <summary>
        /// 目录列表
        /// </summary>
        [XmlElementAttribute("DirPath", IsNullable = false)]
        public List<string> PathList { get; set; }

    }

    /// <summary>
    /// 最近访问的文件信息
    /// </summary>
    public class RecentFileInfo
    {
        /// <summary>
        /// 最近访问的文件
        /// </summary>
        [XmlElementAttribute("FilePath", IsNullable = false)]
        public ObservableCollection<string> Files;
      

        /// <summary>
        /// 最近访问的文件个数
        /// </summary>
        [XmlIgnore]
        public int FilesCount { get; set; }

        public RecentFileInfo()
        {
            FilesCount = 10;
            Files = new ObservableCollection<string>();
            Files.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(Files_CollectionChanged);
        }

        /// <summary>
        /// 添加项到集合中时触发，确保个数不超过FilesCount的值
        /// </summary>
        /// <param name="sender">集合</param>
        /// <param name="e">事件</param>
        void Files_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                if (Files != null && Files.Count > FilesCount)
                {
                    Files.RemoveAt(FilesCount - 1);
                }
            }
        }
    }

    /// <summary>
    /// 单个波形配置文件信息
    /// </summary>
    public class WaveConfigDesc
    {
        /// <summary>
        /// 波形显示配置文件列表
        /// </summary>
        [XmlElementAttribute("WaveConfig", IsNullable = false)]
        public string WaveConfigFile { get; set; }

        /// <summary>
        /// 波形配置索引
        /// </summary>
        [XmlElementAttribute("WaveConfigIndex", IsNullable = false)]
        public int WaveConfigIndex { get; set; }
    }

    /// <summary>
    /// 波形配置总体信息
    /// </summary>
    public class WaveConfigsDesc
    {
        /// <summary>
        /// 波形配置列表
        /// </summary>
        [XmlElementAttribute("WaveConfigs", IsNullable = false)]
        public List<WaveConfigDesc> WaveConfigList { get; set; }

        /// <summary>
        /// 波形配置文件个数
        /// </summary>
        [XmlIgnore]
        public int WaveConfigCount { get; set; }

        public WaveConfigsDesc()
        {
            WaveConfigCount = 10;
            WaveConfigList = new List<WaveConfigDesc>();
        }
    }

    /// <summary>
    /// 配置文件信息
    /// </summary>
    public class ConfigData
    {
        /// <summary>
        /// 版本信息
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 是否显示无效数据
        /// </summary>
        public bool IsInvalidDataShow { get; set; }

        /// <summary>
        /// 测量半径
        /// </summary>
        public int MeterageRadius { get; set; }

        /// <summary>
        /// 标注半斤
        /// </summary>
        public int SignRadius { get; set; }

        /// <summary>
        ///自动保存通道配置
        /// </summary>
        public bool IsAutoSaveConfig { get; set; }

        /// <summary>
        /// 自动滚动速度
        /// </summary>
        public int AutoScrollVelocity { get; set; }

        /// <summary>
        /// 服务ip及端口
        /// </summary>
        public ServerConfigDesc ServerConfig { get; set; }

        /// <summary>
        /// 媒体库路径
        /// </summary>
        public string MediaPath { get; set; }

        /// <summary>
        /// 台帐信息
        /// </summary>
        public AccountsInfoDesc Accouts { get; set; }

        /// <summary>
        /// 访问目录信息
        /// </summary>
        public DirectoryPathInfo DirPaths { get; set; }

        /// <summary>
        /// 最近访问的文件
        /// </summary>
        public RecentFileInfo RecentFiles { get; set; }

        /// <summary>
        /// 导出路径
        /// </summary>
        //public string ExportPath { get; set; }

        /// <summary>
        /// 波形配置文件
        /// </summary>
        public WaveConfigsDesc WaveConfigs { get; set; }

        /// <summary>
        /// 默认通道配置文件
        /// </summary>
        public string DefaultChannelConfig { get; set; }

        public ConfigData()
        {
            Version = "1.0.0";
            IsInvalidDataShow = true;
            IsAutoSaveConfig = false;
            MeterageRadius = 10;
            AutoScrollVelocity = 20;
            SignRadius = 20;
            ServerConfig = new ServerConfigDesc() { IP = "172.16.80.20", Port = 1235 };
            Accouts = new AccountsInfoDesc() { IsCheck = true, };
            Accouts.AccountDescList = new List<AccountDesc>();
            Accouts.AccountDescList.Add(new AccountDesc("曲线", "qx", "起点里程", "终点里程", true, "曲线半径", "起点里程,终点里程,曲线方向,曲线半径,超高,曲线全长"));
            Accouts.AccountDescList.Add(new AccountDesc("坡度", "pd", "起点里程", "终点里程", false, "坡度", "*"));
            Accouts.AccountDescList.Add(new AccountDesc("道岔", "dch", "尖轨尖里程", "", false, "尖轨尖里程", "*"));
            Accouts.AccountDescList.Add(new AccountDesc("长短链", "长短链", "公里", "", false, "类型", "*"));
            Accouts.AccountDescList.Add(new AccountDesc("速度区段", "Suduji", "起点里程", "终点里程", false, "LINESPEEDLEVEL", "*"));
            DirPaths = new DirectoryPathInfo();
            RecentFiles = new RecentFileInfo();
            MediaPath = string.Empty;
            //ExportPath = "D:/";
            WaveConfigs = new WaveConfigsDesc();
            DefaultChannelConfig = string.Empty;
        }

    }
}
