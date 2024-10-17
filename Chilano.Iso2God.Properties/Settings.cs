using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Chilano.Iso2God.Properties
{
    [CompilerGenerated]
    [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "9.0.0.0")]
    internal sealed class Settings : ApplicationSettingsBase
    {
        private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());

        public static Settings Default => defaultInstance;

        [DefaultSettingValue("")]
        [DebuggerNonUserCode]
        [UserScopedSetting]
        public string OutputPath
        {
            get
            {
                return (string)this["OutputPath"];
            }
            set
            {
                this["OutputPath"] = value;
            }
        }

        [DefaultSettingValue("")]
        [UserScopedSetting]
        [DebuggerNonUserCode]
        public string RebuildPath
        {
            get
            {
                return (string)this["RebuildPath"];
            }
            set
            {
                this["RebuildPath"] = value;
            }
        }

        [UserScopedSetting]
        [DefaultSettingValue("True")]
        [DebuggerNonUserCode]
        public bool RebuiltCheck
        {
            get
            {
                return (bool)this["RebuiltCheck"];
            }
            set
            {
                this["RebuiltCheck"] = value;
            }
        }

        [DefaultSettingValue("True")]
        [DebuggerNonUserCode]
        [UserScopedSetting]
        public bool AlwaysSave
        {
            get
            {
                return (bool)this["AlwaysSave"];
            }
            set
            {
                this["AlwaysSave"] = value;
            }
        }

        [DefaultSettingValue("")]
        [UserScopedSetting]
        [DebuggerNonUserCode]
        public string FtpIP
        {
            get
            {
                return (string)this["FtpIP"];
            }
            set
            {
                this["FtpIP"] = value;
            }
        }

        [DefaultSettingValue("xbox")]
        [DebuggerNonUserCode]
        [UserScopedSetting]
        public string FtpUser
        {
            get
            {
                return (string)this["FtpUser"];
            }
            set
            {
                this["FtpUser"] = value;
            }
        }

        [DefaultSettingValue("xbox")]
        [UserScopedSetting]
        [DebuggerNonUserCode]
        public string FtpPass
        {
            get
            {
                return (string)this["FtpPass"];
            }
            set
            {
                this["FtpPass"] = value;
            }
        }

        [DebuggerNonUserCode]
        [DefaultSettingValue("21")]
        [UserScopedSetting]
        public string FtpPort
        {
            get
            {
                return (string)this["FtpPort"];
            }
            set
            {
                this["FtpPort"] = value;
            }
        }

        [DefaultSettingValue("True")]
        [UserScopedSetting]
        [DebuggerNonUserCode]
        public bool AutoRenameMultiDisc
        {
            get
            {
                return (bool)this["AutoRenameMultiDisc"];
            }
            set
            {
                this["AutoRenameMultiDisc"] = value;
            }
        }

        [DefaultSettingValue("False")]
        [UserScopedSetting]
        [DebuggerNonUserCode]
        public bool FtpUpload
        {
            get
            {
                return (bool)this["FtpUpload"];
            }
            set
            {
                this["FtpUpload"] = value;
            }
        }

        [DefaultSettingValue("2")]
        [DebuggerNonUserCode]
        [UserScopedSetting]
        public int DefaultPadding
        {
            get
            {
                return (int)this["DefaultPadding"];
            }
            set
            {
                this["DefaultPadding"] = value;
            }
        }

        [DefaultSettingValue("False")]
        [DebuggerNonUserCode]
        [UserScopedSetting]
        public bool AutoBrowse
        {
            get
            {
                return (bool)this["AutoBrowse"];
            }
            set
            {
                this["AutoBrowse"] = value;
            }
        }

        [DefaultSettingValue("False")]
        [UserScopedSetting]
        [DebuggerNonUserCode]
        public bool TitleDirectory
        {
            get
            {
                return (bool)this["TitleDirectory"];
            }
            set
            {
                this["TitleDirectory"] = value;
            }
        }

        [DefaultSettingValue("800")]
        [DebuggerNonUserCode]
        [UserScopedSetting]
        public int Width
        {
            get
            {
                return (int)this["Width"];
            }
            set
            {
                this["Width"] = value;
            }
        }

        [DefaultSettingValue("400")]
        [DebuggerNonUserCode]
        [UserScopedSetting]
        public int Height
        {
            get
            {
                return (int)this["Height"];
            }
            set
            {
                this["Height"] = value;
            }
        }

        [DefaultSettingValue("False")]
        [DebuggerNonUserCode]
        [UserScopedSetting]
        public bool Maximized
        {
            get
            {
                return (bool)this["Maximized"];
            }
            set
            {
                this["Maximized"] = value;
            }
        }

        [DefaultSettingValue("190,65,35,50,55,100,270")]
        [DebuggerNonUserCode]
        [UserScopedSetting]
        public string ColumnsWidth
        {
            get
            {
                return (string)this["ColumnsWidth"];
            }
            set
            {
                this["ColumnsWidth"] = value;
            }
        }

        [DefaultSettingValue("False")]
        [DebuggerNonUserCode]
        [UserScopedSetting]
        public bool DeleteIsoAfterCompletion
        {
            get
            {
                try
                {
                    return (bool)this["DeleteIsoAfterCompletion"];
                }
                catch (Exception ex)
                {
                    // Log or handle the exception as needed
                    // Example: Log to a file or show a message box
                    // File.AppendAllText("settings_error.log", ex.ToString());
                    MessageBox.Show($"Error retrieving setting: {ex.Message}", "Settings Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Return a default value in case of an error
                    return false;
                }
            }
            set
            {
                try
                {
                    this["DeleteIsoAfterCompletion"] = value;
                }
                catch (Exception ex)
                {
                    // Log or handle the exception as needed
                    // Example: Log to a file or show a message box
                    // File.AppendAllText("settings_error.log", ex.ToString());
                    MessageBox.Show($"Error saving setting: {ex.Message}", "Settings Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
