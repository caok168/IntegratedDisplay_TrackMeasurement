/// -------------------------------------------------------------------------------------------
/// FileName：ConfigManger.cs
/// 说    明：配置文件管理类，用于系统设置的读取和保存
/// Version ：1.0
/// Date    ：2017/5/26
/// Author  ：jinxl
/// -------------------------------------------------------------------------------------------
using IntegratedDisplay.Models;
using System;
using System.IO;
using System.Xml.Serialization;

namespace IntegratedDisplay
{
    internal static class ConfigManger
    {
        /// <summary>
        /// 配置文件路径
        /// </summary>
        private static string _configPath = string.Empty;

        /// <summary>
        /// 窗体布局配置路径
        /// </summary>
        private static string _layoutConfigPath = string.Empty;      

        /// <summary>
        /// 配置文件，如果路径为空，会自动创建默认配置
        /// </summary>
        public static string ConfigPath
        {
            get { return _configPath; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    return;
                }
                _configPath = value;
                if (!File.Exists(value))
                {
                    SaveConfigData(new ConfigData());
                }
                

            }
        }

        /// <summary>
        /// 布局文件路径，找不到则创建默认
        /// </summary>
        public static string LayoutConfigPath
        {
            get
            {
                return _layoutConfigPath;
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    return;
                }
                _layoutConfigPath = value;
                if (!File.Exists(_layoutConfigPath))
                {
                    SaveLayoutConfig(new FormlayoutConfig());
                }
            }
        }

        /// <summary>
        /// 配置文件序列化器实例
        /// </summary>
        private static XmlSerializer _serializer = new XmlSerializer(typeof(ConfigData));

        /// <summary>
        /// 布局配置序列化实例
        /// </summary>
        private static XmlSerializer _layoutSer = new XmlSerializer(typeof(FormlayoutConfig));

        /// <summary>
        /// 通过反序列化得到配置文件数据
        /// </summary>
        /// <returns>返回配置文件数据</returns>
        public static ConfigData GetConfigData()
        {
            try
            {
                using (StreamReader reader = new StreamReader(ConfigPath))
                {
                    return (ConfigData)_serializer.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 通过序列化保存配置文件
        /// </summary>
        /// <param name="config">需要序列化的配置文件数据</param>
        public static void SaveConfigData(ConfigData config)
        {
            try
            {
                XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                namespaces.Add("", "");
                using (StreamWriter writer = new StreamWriter(ConfigPath))
                {
                    _serializer.Serialize(writer, config, namespaces);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static FormlayoutConfig GetLayoutConfig()
        {
            try
            {
                using (StreamReader reader = new StreamReader(LayoutConfigPath))
                {
                    return (FormlayoutConfig)_layoutSer.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void SaveLayoutConfig(FormlayoutConfig config)
        {
            try
            {
                XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                namespaces.Add("", "");
                using (StreamWriter writer = new StreamWriter(LayoutConfigPath))
                {
                    _layoutSer.Serialize(writer, config, namespaces);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
